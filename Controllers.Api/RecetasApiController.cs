using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoPROGEND.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
[Authorize(AuthenticationSchemes = "ApiScheme")]
public class RecetasApiController : ControllerBase
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly AppDbContext _context;
    private readonly IConfiguration _configuration;

    public RecetasApiController(UserManager<ApplicationUser> userManager, AppDbContext context, IConfiguration configuration)
    {
        _userManager = userManager;
        _context = context;
        _configuration = configuration;
    }

    [HttpGet("recomendadas")]
    public async Task<IActionResult> GetRecomendadas(int page = 1, int pageSize = 3)
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null) return Unauthorized();

        var userDatos = await _context.DatosUsers
            .OrderByDescending(d => d.RecordDate)
            .FirstOrDefaultAsync(d => d.UserId == user.Id);

        var recomendadasQuery = Enumerable.Empty<Recetas>().AsQueryable();
        if (userDatos == null || userDatos.CaloriasConsumidas == 0)
        { // Recomendaciones básicas si no hay consumo registrado
            recomendadasQuery = _context.Recetas
            .Where(r => r.Calorias <= 500
            && r.Proteinas >= 10
            && r.Carbohidratos <= 70);
        }
        else
        {
            // Fórmula de Mifflin-St Jeor considerando el sexo del usuario 
            double bmr;
            if (user.Sexo == "M")
            {
                bmr = 10 * userDatos.Peso + 6.25 * userDatos.Altura - 5 * (DateTime.Today.Year - user.FechaNacimiento.Year) + 5;
            }
            else
            {
                bmr = 10 * userDatos.Peso + 6.25 * userDatos.Altura - 5 * (DateTime.Today.Year - user.FechaNacimiento.Year) - 161;
            }
            recomendadasQuery = _context.Recetas
                .Where(r => r.Calorias <= (bmr * 0.75)
                    && r.Calorias >= (bmr * 0.25)
                    && r.Proteinas >= 0.2 * r.Calorias / 4
                    && r.Carbohidratos <= 0.65 * r.Calorias / 4);
        }

        var total = await recomendadasQuery.CountAsync();
        var recetas = await recomendadasQuery
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        var baseUrl = _configuration.GetSection("AppSettings")["PublicBaseUrl"] + "imagenes/";

        return Ok(new
        {
            baseUrl = baseUrl,
            paginacion = new PaginatedResult<Recetas> { Total = total, Items = recetas }
        });
    }

    [HttpGet("todas")]
    public async Task<IActionResult> GetTodas(int page = 1, int pageSize = 9)
    {
        var query = _context.Recetas.AsQueryable();
        var total = await query.CountAsync();
        var recetas = await query
            .OrderBy(r => r.Nombre)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        var baseUrl = _configuration.GetSection("AppSettings")["PublicBaseUrl"] + "imagenes/";

        return Ok(new
        {
            baseUrl = baseUrl,
            paginacion = new PaginatedResult<Recetas> { Total = total, Items = recetas }
        });
    }

    [HttpPost("favorita/{id}")]
    public async Task<IActionResult> AddFavorita(int id)
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null) return Unauthorized();

        var exists = await _context.UserRecetas
            .AnyAsync(f => f.UserId == user.Id && f.RecetaId == id);
        if (exists) return BadRequest("Ya está en favoritos.");

        _context.UserRecetas.Add(new UserRecetas
        {
            UserId = user.Id,
            RecetaId = id
        });
        await _context.SaveChangesAsync();
        return Ok();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetDetalleReceta(int id)
    {
        var receta = await _context.Recetas.FindAsync(id);
        if (receta == null)
            return NotFound();

        var baseUrl = _configuration.GetSection("AppSettings")["PublicBaseUrl"] + "imagenes/";

        return Ok(new
        {
            baseUrl = baseUrl,
            receta
        });
    }

    [HttpDelete("favorita/{id}")]
    public async Task<IActionResult> RemoveFavorita(int id)
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null) return Unauthorized();

        var fav = await _context.UserRecetas
            .FirstOrDefaultAsync(f => f.UserId == user.Id && f.RecetaId == id);
        if (fav == null) return NotFound();

        _context.UserRecetas.Remove(fav);
        await _context.SaveChangesAsync();
        return Ok();
    }
}