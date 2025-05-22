using ProyectoPROGEND.Models;
using System.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;


namespace ProyectoPROGEND.Controllers;

public class DatosUsuariocontroller : Controller
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly AppDbContext _context;

    public DatosUsuariocontroller(UserManager<ApplicationUser> userManager, AppDbContext context)
    {
        _userManager = userManager;
        _context = context;

    }
    public async Task<IActionResult> Index()

    {
        if (!User.Identity.IsAuthenticated)
        {
            return RedirectToPage("/Account/Login", new { area = "Identity" }); // Redirige a la página de login sin modificar nada en los archivos generados
        }

        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            return RedirectToPage("/Account/Login", new { area = "Identity" });
        }

        var recetasFavorites = await _context.UserRecetas
            .Where(urf => urf.UserId == user.Id)
            .Select(urf => urf.Receta)
            .ToListAsync();

        var planesFavorites = await _context.UserPlanesEntrenamientos
            .Where(upf => upf.UserId == user.Id)
            .Select(upf => upf.PlanEntrenamiento)
            .ToListAsync();

        var today = DateTime.Today;
        var datosUsuario = await _context.DatosUsers
            .Where(d => d.UserId == user.Id && d.RecordDate >= today
            .AddDays(-6))
            .OrderByDescending(d => d.RecordDate)
            .Take(7)
            .ToListAsync();

        var viewModel = new PlanesYrecetasViewModel
        {
            Planes = planesFavorites,
            Recetas = recetasFavorites,
            DatosUsuario = datosUsuario
        };
        //List<Tiposdeautos> tdea = await contexto.Tiposdeautos.ToListAsync();
        return View(viewModel);
    }

    public async Task<IActionResult> HistorialDatos()
    {
        if (!User.Identity.IsAuthenticated)
        {
            return RedirectToPage("/Account/Login", new { area = "Identity" });
        }

        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            return RedirectToPage("/Account/Login", new { area = "Identity" });
        }

        // Obtener todos los registros del usuario ordenados por fecha descendente
        var datosUsuarioHistorial = await _context.DatosUsers
            .Where(d => d.UserId == user.Id)
            .OrderByDescending(d => d.RecordDate)
            .ToListAsync();

        return View(datosUsuarioHistorial);
    }

    [HttpGet]
    public async Task<IActionResult> UpdateDatos()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            return RedirectToPage("/Account/Login", new { area = "Identity" });
        }

        var today = DateTime.Today;
        var datos = await _context.DatosUsers
            .Where(d => d.UserId == user.Id && d.RecordDate == today).FirstOrDefaultAsync();
        if (datos == null)
        {
            datos = new DatosUser { UserId = user.Id, RecordDate = today };
        }
        return View(datos);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> UpdateDatos(DatosUser datos)
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            return RedirectToPage("/Account/Login", new { area = "Identity" });
        }
        var today = DateTime.Today;
        var existingDatos = await _context.DatosUsers
            .Where(d => d.UserId == user.Id && d.RecordDate == today)
            .FirstOrDefaultAsync();

        if (existingDatos == null)
        {
            datos.UserId = user.Id;
            datos.RecordDate = today;
            _context.DatosUsers.Add(datos);
        }
        else
        {
            existingDatos.CaloriasConsumidas += datos.CaloriasConsumidas;
            existingDatos.CaloriasQuemadas += datos.CaloriasQuemadas;
            existingDatos.Peso = datos.Peso;
            existingDatos.Altura = datos.Altura;
            _context.DatosUsers.Update(existingDatos);
        }
        await _context.SaveChangesAsync();
        return RedirectToAction("Index"); // Redirige a una vista adecuada }
    }

    public async Task<IActionResult> RemoveRecetasFromFavorites(int Id)
    {
        if (!User.Identity.IsAuthenticated)
        {
            return RedirectToPage("/Account/Login", new { area = "Identity" });
        }

        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            return RedirectToPage("/Account/Login", new { area = "Identity" });
        }

        var favorite = await _context.UserRecetas
            .FirstOrDefaultAsync(f => f.UserId == user.Id && f.RecetaId == Id);

        if (favorite != null)
        {
            _context.UserRecetas.Remove(favorite);
            await _context.SaveChangesAsync();
        }

        return RedirectToAction("Index");
    }

    public async Task<IActionResult> RemovePlanFromFavorites(int Id)
    {
        if (!User.Identity.IsAuthenticated)
        {
            return RedirectToPage("/Account/Login", new { area = "Identity" });
        }

        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            return RedirectToPage("/Account/Login", new { area = "Identity" });
        }

        var favorite = await _context.UserPlanesEntrenamientos
            .FirstOrDefaultAsync(f => f.UserId == user.Id && f.PlanEntrenamientoId == Id);

        if (favorite != null)
        {
            _context.UserPlanesEntrenamientos.Remove(favorite);
            await _context.SaveChangesAsync();
        }

        return RedirectToAction("Index");
    }

}