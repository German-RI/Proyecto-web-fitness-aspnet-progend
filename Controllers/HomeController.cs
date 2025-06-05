using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ProyectoPROGEND.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace ProyectoPROGEND.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly AppDbContext _context;

    public HomeController(ILogger<HomeController> logger, UserManager<ApplicationUser> userManager, AppDbContext context)
    {
        _logger = logger;
        _userManager = userManager;
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        /*if (_context.DatosVistaInicio == null)
        {

        }*/

        var datvistinicio = await _context.DatosVistaInicio.FindAsync(1);
        if (datvistinicio == null)
        {
            // Si no existe, crea una fila con valores vacíos
            datvistinicio = new DatosVistaInicio();
            datvistinicio.Id = 1; // Asegúrate de que el Id sea único
            datvistinicio.LinkVideo = string.Empty; // O cualquier valor por defecto que desees         
            datvistinicio.Acercadenosotros = string.Empty; // O cualquier valor por defecto que desees
            _context.DatosVistaInicio.Add(datvistinicio);
            await _context.SaveChangesAsync();
        }

        return View(datvistinicio);
    }

    [Authorize(Roles = "ADMIN")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EditVistaIndex(int? Id, [Bind("Id,LinkVideo,Acercadenosotros")] DatosVistaInicio datvistinicio)
    {

        if (_context.DatosVistaInicio == null)
        {
            return Problem("La tabla DatosVistaInicio no está disponible.");
        }

        if (Id != datvistinicio.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                // Asegúrate de que el enlace del video sea un embed de YouTube
                if (!string.IsNullOrWhiteSpace(datvistinicio.LinkVideo))
                {
                    datvistinicio.LinkVideo = GetEmbedUrl(datvistinicio.LinkVideo);
                }
                _context.Update(datvistinicio);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DatExists(datvistinicio.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }

        TempData["Error"] = "No se pudo guardar los cambios. Verifica los datos.";
        return View("Index", datvistinicio);

    }
    private bool DatExists(int id)
    {
        return (_context.DatosVistaInicio?.Any(e => e.Id == id)).GetValueOrDefault();
    }

    public string GetEmbedUrl(string url)
    {
        if (string.IsNullOrWhiteSpace(url))
            return "";

        // Si ya es un embed, lo dejamos igual
        if (url.Contains("youtube.com/embed/"))
            return url;

        // Si es un link normal de YouTube
        var videoId = "";
        if (url.Contains("youtube.com/watch"))
        {
            var uri = new Uri(url);
            var query = System.Web.HttpUtility.ParseQueryString(uri.Query);
            videoId = query["v"];
        }
        else if (url.Contains("youtu.be/"))
        {
            var parts = url.Split('/');
            videoId = parts.Last();
        }

        if (!string.IsNullOrEmpty(videoId))
            return $"https://www.youtube.com/embed/{videoId}";

        // Si no es YouTube, regresa el original
        return url;
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}


