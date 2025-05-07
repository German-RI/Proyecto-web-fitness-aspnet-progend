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
using Microsoft.AspNetCore.Authorization;
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
        if (!User.Identity.IsAuthenticated)
        {
            return RedirectToPage("/Account/Login", new { area = "Identity" });
        }
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            return RedirectToPage("/Account/Login", new { area = "Identity" });
        }

        var userDatos = await _context.DatosUsers
            .OrderByDescending(d => d.RecordDate)
            .FirstOrDefaultAsync(d => d.UserId == user.Id);

        var recomendaciones = new List<Recetas>();
        if (userDatos == null || userDatos.CaloriasConsumidas == 0)
        { // Recomendaciones básicas si no hay consumo registrado 
            recomendaciones = await _context.Recetas
            .Where(r => r.Calorias <= 500
            && r.Proteinas >= 10
            && r.Carbohidratos <= 70)
            .ToListAsync();
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
            recomendaciones = await _context.Recetas
                .Where(r => r.Calorias <= (bmr * 0.75)
                    && r.Calorias >= (bmr * 0.25)
                    && r.Proteinas >= 0.2 * r.Calorias / 4
                    && r.Carbohidratos <= 0.65 * r.Calorias / 4)
                .ToListAsync();
        }

        var viewModel = new PlanesYrecetasViewModel
        {
            Recetas = await _context.Recetas.ToListAsync(),
            RecomendacionRecetas = recomendaciones
        };

        return View(viewModel);
    }

    /*create */
    [Authorize(Roles = "ADMIN")]
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
    [Authorize(Roles = "ADMIN")]
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
    [Authorize(Roles = "ADMIN")]
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

        // Verifica si el favorito ya existe 
        var existingFavorite = await _context.UserRecetas
            .FirstOrDefaultAsync(f => f.UserId == user.Id && f.RecetaId == Id);
        if (existingFavorite != null)
        {
            TempData["Message"] = "Esta receta ya está en tus favoritos.";
            return RedirectToAction("Index");
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