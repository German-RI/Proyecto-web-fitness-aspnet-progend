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
using Microsoft.EntityFrameworkCore.Metadata.Internal;

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
        var recomendaciones = new List<PlanEntranamiento>();
        if (userDatos != null)
        {
            recomendaciones = await _context.PlanEntranamiento
                .Where(p => userDatos.Peso >= p.PesoMinRecom
                    && userDatos.Peso <= p.PesoMaxRecom
                    && userDatos.Altura >= p.AlturaMinRecom
                    && userDatos.Altura <= p.AlturaMaxRecom
                    && userDatos.RecordDate.Year - user.FechaNacimiento.Year >= p.EdadMinRecom
                    && userDatos.RecordDate.Year - user.FechaNacimiento.Year <= p.EdadMaxRecom)
                .ToListAsync();
        }
        var viewModel = new PlanesYrecetasViewModel
        {
            Planes = await _context.PlanEntranamiento.ToListAsync(),
            RecomendacionPlanes = recomendaciones
        };

        return View(viewModel);

    }

    /*Crear plan de entrenamiento*/
    [Authorize(Roles = "ADMIN,EDITPLANES")]//Se autoriza solo al Administrador (por ahora)
    public IActionResult Create()
    {
        return View();//retorna la vista para introducir datos
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Nombre,Image_Portada,TipoEntrenamiento,Equipamiento,Frecuencia,Objetivo,Duracion,Dificultad,Nivel,Descripcion,EdadMinRecom,EdadMaxRecom,PesoMaxRecom,PesoMinRecom,AlturaMinRecom,AlturaMaxRecom")] PlanEntranamiento tdea, IFormFile? imagen)
    {
        if (imagen == null || imagen.Length == 0)
        {
            ModelState.AddModelError("imagen", "La imagen es obligatoria.");
        }
        if (ModelState.IsValid)
        {
            if (imagen != null && imagen.Length > 0)
            {
                // Crear carpeta si no existe
                var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "imagenes");
                if (!Directory.Exists(folderPath))
                    Directory.CreateDirectory(folderPath);

                /*if (!string.IsNullOrEmpty(tdea.Image_Portada))
                {
                    var rutaAntigua = Path.Combine(folderPath, tdea.Image_Portada);
                    if (System.IO.File.Exists(rutaAntigua))
                    {
                        System.IO.File.Delete(rutaAntigua);
                    }
                }*/

                var nombreArchivo = Guid.NewGuid().ToString() + Path.GetExtension(imagen.FileName); // nombre único
                var ruta = Path.Combine(folderPath, nombreArchivo);

                using (var stream = new FileStream(ruta, FileMode.Create))
                {
                    await imagen.CopyToAsync(stream);
                }

                tdea.Image_Portada = nombreArchivo;
            }
            else
            {
                // Mantén el valor actual de Image_Portada si no se sube una nueva imagen
                _context.Entry(tdea).Property(r => r.Image_Portada).IsModified = false;
            }
            _context.Add(tdea);
            TempData["Message"] = "Plan de entrenamiento creado correctamente.";
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

        // Cargar recetas recomendadas
        var recetasRecomendadas = await _context.RelacionEntrenamientoRecetas
            .Where(r => r.PlanEntrenamientoId == id)
            .Select(r => r.Recetas)
            .ToListAsync();

        ViewBag.RecetasRecomendadas = recetasRecomendadas;

        return View(planEntrena);

    }
    /*Editar*/
    [Authorize(Roles = "ADMIN,EDITPLANES")]
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
    public async Task<IActionResult> Edit(int? Id, [Bind("Id,Nombre,Image_Portada,TipoEntrenamiento,Equipamiento,Frecuencia,Objetivo,Duracion,Dificultad,Nivel,Descripcion,EdadMinRecom,EdadMaxRecom,PesoMaxRecom,PesoMinRecom,AlturaMinRecom,AlturaMaxRecom")] PlanEntranamiento planEntrena, IFormFile? imagen)
    {
        if (Id != planEntrena.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                if (imagen != null && imagen.Length > 0)
                {
                    // Crear carpeta si no existe
                    var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "imagenes");
                    if (!Directory.Exists(folderPath))
                        Directory.CreateDirectory(folderPath);

                    if (!string.IsNullOrEmpty(planEntrena.Image_Portada))
                    {
                        var rutaAntigua = Path.Combine(folderPath, planEntrena.Image_Portada);
                        if (System.IO.File.Exists(rutaAntigua))
                        {
                            System.IO.File.Delete(rutaAntigua);
                        }
                    }

                    var nombreArchivo = Guid.NewGuid().ToString() + Path.GetExtension(imagen.FileName); // nombre único
                    var ruta = Path.Combine(folderPath, nombreArchivo);

                    using (var stream = new FileStream(ruta, FileMode.Create))
                    {
                        await imagen.CopyToAsync(stream);
                    }

                    planEntrena.Image_Portada = nombreArchivo;
                }
                else
                {
                    // Mantén el valor actual de Image_Portada si no se sube una nueva imagen
                    _context.Entry(planEntrena).Property(r => r.Image_Portada).IsModified = false;
                }

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
            TempData["Message"] = "Actualización exitosa.";
            return RedirectToAction(nameof(Index));
        }
        return View(planEntrena);
    }
    private bool planasExists(int id)
    {
        return (_context.PlanEntranamiento?.Any(e => e.Id == id)).GetValueOrDefault();
    }

    // Mostrar y editar recetas recomendadas para un plan
    [Authorize(Roles = "ADMIN,EDITPLANES")]
    public async Task<IActionResult> EditRecetasToPlan(int? id, string search = null)
    {
        if (id == null) return NotFound();

        var plan = await _context.PlanEntranamiento.FindAsync(id);
        if (plan == null) return NotFound();

        // Recetas ya recomendadas
        var recetasRecomendadas = await _context.RelacionEntrenamientoRecetas
            .Where(r => r.PlanEntrenamientoId == id)
            .Select(r => r.Recetas)
            .ToListAsync();

        // Todas las recetas (con filtro de búsqueda si aplica)
        var recetasQuery = _context.Recetas.AsQueryable();
        if (!string.IsNullOrEmpty(search))
            recetasQuery = recetasQuery.Where(r => r.Nombre.Contains(search));
        var todasRecetas = await recetasQuery.ToListAsync();

        // IDs de recetas recomendadas para marcar en la vista
        var recomendadasIds = recetasRecomendadas.Select(r => r.Id).ToHashSet();

        ViewBag.PlanId = id;
        ViewBag.RecetasRecomendadas = recetasRecomendadas;
        ViewBag.RecomendadasIds = recomendadasIds;
        ViewBag.Search = search;
        return View(todasRecetas);
    }

    // Acción para añadir o quitar receta recomendada (toggle)
    [Authorize(Roles = "ADMIN,EDITPLANES")]
    [HttpPost]
    public async Task<IActionResult> ToggleRecetaRecomendada(int planId, int recetaId)
    {
        var relacion = await _context.RelacionEntrenamientoRecetas
            .FirstOrDefaultAsync(r => r.PlanEntrenamientoId == planId && r.RecetasId == recetaId);

        if (relacion == null)
        {
            // Añadir
            _context.RelacionEntrenamientoRecetas.Add(new RelacionEntrenamientoRecetas
            {
                PlanEntrenamientoId = planId,
                RecetasId = recetaId
            });
            TempData["Message"] = "Receta añadida como recomendada.";
        }
        else
        {
            // Quitar
            _context.RelacionEntrenamientoRecetas.Remove(relacion);
            TempData["Message"] = "Receta eliminada de las recomendadas.";
        }
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(EditRecetasToPlan), new { id = planId });
    }

    //Delete/
    [Authorize(Roles = "ADMIN,EDITPLANES")]
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
        TempData["Message"] = "Plan de entrenamiento eliminado correctamente.";
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

        TempData["Message"] = "Plan de entrenamiento añadido a tus selecciones.";
        return RedirectToAction("Index");
    }




}