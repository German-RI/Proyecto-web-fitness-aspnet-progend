using ProyectoPROGEND.Models;
using System.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
namespace ProyectoPROGEND.Controllers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
public class SeleccionRecetas : Controller
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly AppDbContext _context;

    public SeleccionRecetas(UserManager<ApplicationUser> userManager, AppDbContext context)
    {
        _userManager = userManager;
        _context = context;
    }
    public async Task<IActionResult> Index()
    {
        List<Recetas> tdea = await _context.Recetas.ToListAsync();
        return View(tdea);
    }

    /*create */
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Nombre,Ingredientes,Instrucciones,Calorias,Proteinas,Carbohidratos")] Recetas tdea)
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
        if (id == null || _context.Recetas == null)
        {
            return NotFound();
        }

        Recetas? recetas = await _context.Recetas.FirstOrDefaultAsync(n => n.Id == id);
        if (recetas == null)
        {
            return NotFound();
        }

        return View(recetas);

    }
    /*Editar*/
    public async Task<IActionResult> Edit(int? Id)
    {
        if (Id == null || _context.Recetas == null)
        {
            return NotFound();
        }

        var recetas = await _context.Recetas.FindAsync(Id);
        if (recetas == null)
        {
            return NotFound();
        }

        return View(recetas);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? Id, [Bind("Id,Nombre,Ingredientes,Instrucciones,Calorias,Proteinas,Carbohidratos")] Recetas recetas)
    {
        if (Id != recetas.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(recetas);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RecetasExists(recetas.Id))
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
        return View(recetas);
    }
    private bool RecetasExists(int id)
    {
        return (_context.Recetas?.Any(e => e.Id == id)).GetValueOrDefault();
    }
    //Delete/
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null || _context.Recetas == null)
        {
            return NotFound();
        }

        var selecreceta = await _context.Recetas
            .FirstOrDefaultAsync(m => m.Id == id);
        if (selecreceta == null)
        {
            return NotFound();
        }

        return View(selecreceta);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        var selecreceta = await _context.Recetas.FindAsync(id);
        if (selecreceta != null)
        {
            _context.Recetas.Remove(selecreceta);
            await _context.SaveChangesAsync();
        }
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> AddRecetaToFavorites(int Id)
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
        var favorite = new UserRecetas
        {
            UserId = user.Id,
            RecetaId = Id
        };

        _context.UserRecetas.Add(favorite);
        await _context.SaveChangesAsync();

        return RedirectToAction("Index");
    }

}