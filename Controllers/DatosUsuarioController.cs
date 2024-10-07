using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ProyectoPROGEND.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;

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
        //List<Tiposdeautos> tdea = await contexto.Tiposdeautos.ToListAsync();
        return View();
    }
}