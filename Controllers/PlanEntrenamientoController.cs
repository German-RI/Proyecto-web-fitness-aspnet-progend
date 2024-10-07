using ProyectoPROGEND.Models;
using System.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

using Microsoft.AspNetCore.Authorization;

namespace ProyectoPROGEND.Controllers;
public class PlanEntrenamientoController : Controller
{

    private readonly AppDbContext _context;

    public PlanEntrenamientoController(AppDbContext context)
    {
        _context = context;
    }
     public async Task<IActionResult> Index()
    {
        List<PlanEntranamiento> tdea = await  _context.PlanEntranamiento.ToListAsync();
        return View(tdea);

    }
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Nombre,Duracion,Dificultad,Nivel,Descripcion")] PlanEntranamiento tdea)
    {
        if (ModelState.IsValid)
        {
           _context.Add(tdea);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(tdea);
    }
    
}