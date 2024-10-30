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

using Microsoft.AspNetCore.Authorization;

namespace ProyectoPROGEND.Controllers;
public class PlanEntrenamientoController : Controller
{

    private readonly UserManager<ApplicationUser> _userManager;
    private readonly AppDbContext _context;

    public PlanEntrenamientoController(UserManager<ApplicationUser> userManager, AppDbContext context)
    {
        _userManager = userManager;
        _context = context;
    }
    public async Task<IActionResult> Index()
    {
        List<PlanEntranamiento> tdea = await _context.PlanEntranamiento.ToListAsync();
        return View(tdea);

    }
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Nombre,Duracion,Dificultad,Nivel,Descripcion,EdadMinRecom,EdadMaxRecom,PesoMaxRecom,PesoMinRecom,AlturaMinRecom,AlturaMaxRecom")] PlanEntranamiento tdea)
    {
        if (ModelState.IsValid)
        {
            _context.Add(tdea);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(tdea);
    }
    /*Detaalles*/
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null || _context.PlanEntranamiento == null)
        {
            return NotFound();
        }

        PlanEntranamiento? planEntrena = await _context.PlanEntranamiento.FirstOrDefaultAsync(n => n.Id == id);
        if (planEntrena == null)
        {
            return NotFound();
        }

        return View(planEntrena);

    }
    /*Editar*/
    public async Task<IActionResult> Edit(int? Id)
    {
        if (Id == null || _context.PlanEntranamiento == null)
        {
            return NotFound();
        }

        var planEntrena = await _context.PlanEntranamiento.FindAsync(Id);
        if (planEntrena == null)
        {
            return NotFound();
        }

        return View(planEntrena);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? Id, [Bind("Id,Nombre,Duracion,Dificultad,Nivel,Descripcion,EdadMinRecom,EdadMaxRecom,PesoMaxRecom,PesoMinRecom,AlturaMinRecom,AlturaMaxRecom")] PlanEntranamiento planEntrena)
    {
        if (Id != planEntrena.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(planEntrena);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!planasExists(planEntrena.Id))
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
        return View(planEntrena);
    }
    private bool planasExists(int id)
    {
        return (_context.PlanEntranamiento?.Any(e => e.Id == id)).GetValueOrDefault();
    }
    //Delete/
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null || _context.PlanEntranamiento == null)
        {
            return NotFound();
        }

        var planEntrena = await _context.PlanEntranamiento
            .FirstOrDefaultAsync(m => m.Id == id);
        if (planEntrena == null)
        {
            return NotFound();
        }

        return View(planEntrena);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        var planEntrena = await _context.PlanEntranamiento.FindAsync(id);
        if (planEntrena != null)
        {
            _context.PlanEntranamiento.Remove(planEntrena);
            await _context.SaveChangesAsync();
        }
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> AddPlanToFavorites(int Id)
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

        // Verifica si el favorito ya existe 
        var existingFavorite = await _context.UserPlanesEntrenamientos
            .FirstOrDefaultAsync(f => f.UserId == user.Id && f.PlanEntrenamientoId == Id);
        if (existingFavorite != null)
        {
            TempData["Message"] = "Este plan ya está en tus favoritos.";
            return RedirectToAction("Index");
        }

        var favorite = new UserPlanesEntrenamiento
        {
            UserId = user.Id,
            PlanEntrenamientoId = Id
        };

        _context.UserPlanesEntrenamientos.Add(favorite);
        await _context.SaveChangesAsync();

        return RedirectToAction("Index");
    }

    


}