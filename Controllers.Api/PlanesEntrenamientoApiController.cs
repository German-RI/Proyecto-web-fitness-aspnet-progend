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
public class PlanesEntrenamientoApiController : ControllerBase
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly AppDbContext _context;
    private readonly IConfiguration _configuration;

    public PlanesEntrenamientoApiController(UserManager<ApplicationUser> userManager, AppDbContext context, IConfiguration configuration)
    {
        _userManager = userManager;
        _context = context;
        _configuration = configuration;
    }

    [HttpGet("recomendados")]
    public async Task<IActionResult> GetRecomendados(int page = 1, int pageSize = 3)
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null) return Unauthorized();

        var userDatos = await _context.DatosUsers
            .OrderByDescending(d => d.RecordDate)
            .FirstOrDefaultAsync(d => d.UserId == user.Id);

        var recomendadosQuery = Enumerable.Empty<PlanEntranamiento>().AsQueryable();
        if (userDatos != null)
        {
            var edad = userDatos.RecordDate.Year - user.FechaNacimiento.Year;
            recomendadosQuery = _context.PlanEntranamiento
                .Where(p => userDatos.Peso >= p.PesoMinRecom
                    && userDatos.Peso <= p.PesoMaxRecom
                    && userDatos.Altura >= p.AlturaMinRecom
                    && userDatos.Altura <= p.AlturaMaxRecom
                    && edad >= p.EdadMinRecom
                    && edad <= p.EdadMaxRecom);
        }

        var total = await recomendadosQuery.CountAsync();
        var planes = await recomendadosQuery
            .OrderBy(p => p.Nombre)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return Ok(new PaginatedResult<PlanEntranamiento> { Total = total, Items = planes });
    }

    [HttpGet("todos")]
    public async Task<IActionResult> GetTodos(int page = 1, int pageSize = 9)
    {
        var query = _context.PlanEntranamiento.AsQueryable();
        var total = await query.CountAsync();
        var planes = await query
            .OrderBy(p => p.Nombre)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return Ok(new PaginatedResult<PlanEntranamiento> { Total = total, Items = planes });
    }

    [HttpPost("favorito/{id}")]
    public async Task<IActionResult> AddFavorito(int id)
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null) return Unauthorized();

        var exists = await _context.UserPlanesEntrenamientos
            .AnyAsync(f => f.UserId == user.Id && f.PlanEntrenamientoId == id);
        if (exists) return BadRequest("Ya está en favoritos.");

        _context.UserPlanesEntrenamientos.Add(new UserPlanesEntrenamiento
        {
            UserId = user.Id,
            PlanEntrenamientoId = id
        });
        await _context.SaveChangesAsync();
        return Ok();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetDetallePlan(int id)
    {
        var plan = await _context.PlanEntranamiento.FindAsync(id);
        if (plan == null)
            return NotFound();


        var recetas = await _context.RelacionEntrenamientoRecetas
            .Where(pr => pr.PlanEntrenamientoId == id)
            .Select(pr => pr.Recetas)
            .ToListAsync();

        var baseUrl = _configuration.GetSection("AppSettings")["PublicBaseUrl"] + "/imagenes/";

        return Ok(new
        {
            baseUrl = baseUrl,
            plan,
            recetasRecomendadas = recetas
        });
    }

    [HttpDelete("favorito/{id}")]
    public async Task<IActionResult> RemoveFavorito(int id)
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null) return Unauthorized();

        var fav = await _context.UserPlanesEntrenamientos
            .FirstOrDefaultAsync(f => f.UserId == user.Id && f.PlanEntrenamientoId == id);
        if (fav == null) return NotFound();

        _context.UserPlanesEntrenamientos.Remove(fav);
        await _context.SaveChangesAsync();
        return Ok();
    }
}