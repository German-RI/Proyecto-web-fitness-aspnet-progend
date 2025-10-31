using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoPROGEND.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class DatosVistaInicioApiController : ControllerBase
{
    private readonly AppDbContext _context;

    public DatosVistaInicioApiController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet("home")]
    [AllowAnonymous]
    public async Task<IActionResult> GetDatosHome()
    {
        try
        {
            var datos = await _context.DatosVistaInicio.FindAsync(1);

            if (datos == null)
            {
                // Si no existe, crea una fila con valores vacíos
                datos = new DatosVistaInicio
                {
                    Id = 1,
                    LinkVideo = string.Empty,
                    Acercadenosotros = string.Empty
                };
                _context.DatosVistaInicio.Add(datos);
                await _context.SaveChangesAsync();
            }

            return Ok(new
            {
                success = true,
                datos
            });
        }
        catch (DbUpdateException)
        {
            return StatusCode(503, new
            {
                success = false,
                mensaje = "No hay conexión con la base de datos."
            });
        }
        catch (Exception)
        {
            return StatusCode(500, new
            {
                success = false,
                mensaje = "Ocurrió un error inesperado al obtener los datos."
            });
        }
    }
}