using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoPROGEND.Models;
using System;
using System.Linq;
using System.Threading.Tasks;


namespace ProyectoPROGEND.Controllers.Api
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = "ApiScheme")]
    public class DatosUsuarioApiController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;

        public DatosUsuarioApiController(UserManager<ApplicationUser> userManager, AppDbContext context, IConfiguration configuration)
        {
            _userManager = userManager;
            _context = context;
            _configuration = configuration;
        }

        // GET: api/DatosUsuarioApi
        [HttpGet("resumen")]
        public async Task<IActionResult> GetResumenUsuario()
        {
            // Obtener el usuario autenticado
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
                return Unauthorized("No se pudo identificar el usuario.");


            /*var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return Unauthorized("No se encontró el usuario autenticado.");

            var userId = user.Id; // Este sí coincide con la base y lo que generás en el token*/
            Console.WriteLine($"UserId: {userId}");
            foreach (var claim in User.Claims)
            {
                Console.WriteLine($"Claim Type: {claim.Type}, Value: {claim.Value}");
            }
            // Recetas favoritas
            var recetasFavorites = await _context.UserRecetas
                .Where(urf => urf.UserId == userId)
                .Select(urf => urf.Receta)
                .ToListAsync();

            // Planes favoritos
            var planesFavorites = await _context.UserPlanesEntrenamientos
                .Where(upf => upf.UserId == userId)
                .Select(upf => upf.PlanEntrenamiento)
                .ToListAsync();

            // Datos diarios recientes (últimos 7 días)
            var today = DateTime.Today;
            var datosUsuario = await _context.DatosUsers
                .Where(d => d.UserId == userId && d.RecordDate >= today.AddDays(-6))
                .OrderByDescending(d => d.RecordDate)
                .Take(7)
                .ToListAsync();

            var baseUrl = _configuration.GetSection("AppSettings")["PublicBaseUrl"] + "/imagenes/";


            return Ok(new
            {
                baseUrl = baseUrl,
                planes = planesFavorites,
                recetas = recetasFavorites,
                datosUsuario = datosUsuario
            });
        }

        [HttpPost("actualizar")]
        public async Task<IActionResult> ActualizarDatos([FromBody] DatosUserDto datos)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return Unauthorized();

            var today = DateTime.Today;
            var existing = await _context.DatosUsers
                .FirstOrDefaultAsync(d => d.UserId == user.Id && d.RecordDate == today);

            if (existing == null)
            {
                var nuevo = new DatosUser
                {
                    UserId = user.Id,
                    RecordDate = today,
                    CaloriasConsumidas = datos.CaloriasConsumidas,
                    CaloriasQuemadas = datos.CaloriasQuemadas,
                    Peso = datos.Peso,
                    Altura = datos.Altura
                };
                _context.DatosUsers.Add(nuevo);
            }
            else
            {
                existing.CaloriasConsumidas += datos.CaloriasConsumidas;
                existing.CaloriasQuemadas += datos.CaloriasQuemadas;
                existing.Peso = datos.Peso;
                existing.Altura = datos.Altura;
                _context.DatosUsers.Update(existing);
            }

            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpGet("hoy")]
        public async Task<IActionResult> DatosDeHoy()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return Unauthorized();

            var today = DateTime.Today;
            var datos = await _context.DatosUsers
                .FirstOrDefaultAsync(d => d.UserId == user.Id && d.RecordDate == today);

            if (datos == null) return NotFound();
            return Ok(new
            {
                datos.Peso,
                datos.Altura
            });
        }

        [HttpGet("historial")]
        public async Task<IActionResult> HistorialDatos()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return Unauthorized();

            var historial = await _context.DatosUsers
                .Where(d => d.UserId == user.Id)
                .OrderByDescending(d => d.RecordDate)
                .Select(d => new
                {
                    d.Id,
                    d.CaloriasConsumidas,
                    d.CaloriasQuemadas,
                    d.Peso,
                    d.Altura,
                    d.RecordDate,
                    d.UserId
                })
                .ToListAsync();

            return Ok(historial);
        }

        [HttpDelete("receta-favorita/{id}")]
        public async Task<IActionResult> EliminarRecetaFavorita(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return Unauthorized();

            var fav = await _context.UserRecetas
                .FirstOrDefaultAsync(f => f.UserId == user.Id && f.RecetaId == id);

            if (fav != null)
            {
                _context.UserRecetas.Remove(fav);
                await _context.SaveChangesAsync();
            }
            return Ok();
        }

        [HttpDelete("plan-favorito/{id}")]
        public async Task<IActionResult> EliminarPlanFavorito(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return Unauthorized();

            var fav = await _context.UserPlanesEntrenamientos
                .FirstOrDefaultAsync(f => f.UserId == user.Id && f.PlanEntrenamientoId == id);

            if (fav != null)
            {
                _context.UserPlanesEntrenamientos.Remove(fav);
                await _context.SaveChangesAsync();
            }
            return Ok();
        }
    }
}