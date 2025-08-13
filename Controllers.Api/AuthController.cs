using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProyectoPROGEND.Models;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

namespace ProyectoPROGEND.Controllers.Api
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = "ApiScheme")]
    public class AuthController : ControllerBase
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailSender _emailSender;
        private readonly IConfiguration _config;

        public AuthController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, IEmailSender emailSender, IConfiguration config)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _emailSender = emailSender;
            _config = config;

        }

        public class LoginRequest
        {
            public string Email { get; set; }
            public string Password { get; set; }
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
                return Unauthorized("Usuario no encontrado");

            if (!await _userManager.IsEmailConfirmedAsync(user))
                return Unauthorized("El correo no ha sido confirmado");

            var result = await _signInManager.PasswordSignInAsync(user, request.Password, isPersistent: false, lockoutOnFailure: false);
            if (!result.Succeeded)
                return Unauthorized("Credenciales inválidas");

            // Opcional: Obtener roles o token
            var roles = await _userManager.GetRolesAsync(user);

            var claims = new[]
            {
                /*new Claim(JwtRegisteredClaimNames.Sub, user.Email),*/
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim("roles", string.Join(",", roles))
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expire = DateTime.UtcNow.AddHours(Convert.ToDouble(_config["Jwt:ExpireHours"]));

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: expire,
                signingCredentials: creds
            );

            return Ok(new
            {
                message = "Inicio de sesión exitoso",
                token = new JwtSecurityTokenHandler().WriteToken(token),
                expiration = expire,
                userId = user.Id,
                email = user.Email,
                roles = roles,
                sexo = user.Sexo,
                fechaNacimiento = user.FechaNacimiento

            });
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            if (request.Password != request.ConfirmPassword)
                return BadRequest("Las contraseñas no coinciden.");

            var user = new ApplicationUser
            {
                UserName = request.Email,
                Email = request.Email,
                FechaNacimiento = request.FechaNacimiento,
                Sexo = request.Sexo
            };

            var result = await _userManager.CreateAsync(user, request.Password);
            if (!result.Succeeded)
                return BadRequest(result.Errors);

            // Generar token de confirmación
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

            // Usar una URL pública real (puedes extraerla de config si gustas)
            var baseUrl = _config["AppSettings:PublicBaseUrl"];
            var callbackUrl = $"{baseUrl}/Identity/Account/ConfirmEmail?userId={Uri.EscapeDataString(user.Id)}&code={Uri.EscapeDataString(code)}";

            //configurado IEmailSender:
            await _emailSender.SendEmailAsync(
            user.Email,
            "Confirma tu cuenta",
            $"Por favor confirma tu cuenta haciendo clic <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>aquí</a>.");

            return Ok("Cuenta creada. Revisa tu correo para confirmarla.");
        }
    }
}
