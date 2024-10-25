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
        var viewModel = new PlanesYrecetasViewModel
        {
            Planes = planesFavorites,
            Recetas = recetasFavorites
        };
        //List<Tiposdeautos> tdea = await contexto.Tiposdeautos.ToListAsync();
        return View(viewModel);
    }

}