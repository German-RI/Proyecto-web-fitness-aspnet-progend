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
public class DatosUsuariocontroller : Controller
{
    private readonly AppDbContext _context;

    public DatosUsuariocontroller(AppDbContext context)
    {
        _context = context;

    }
    public async Task<IActionResult> Index()

    {
        var planes = await _context.PlanEntranamiento.ToListAsync();
        var Recetas = await _context.Recetas.ToListAsync();
        var viewModel = new PlanesYrecetasViewModel
        {
            Planes = planes,
            Recetas = Recetas
        };
        //List<Tiposdeautos> tdea = await contexto.Tiposdeautos.ToListAsync();
        return View(viewModel);
    }

}