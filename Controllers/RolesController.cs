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
public class RolesController : Controller
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly AppDbContext _context;

    public RolesController(UserManager<ApplicationUser> userManager, AppDbContext context)
    {
        _userManager = userManager;
        _context = context;
    }


    [Authorize(Roles = "ADMIN")]
    public async Task<IActionResult> Index(string searchTerm)
    {
        var users = await _userManager.Users.ToListAsync();

        if (!string.IsNullOrWhiteSpace(searchTerm))
        {
            users = users.Where(u => u.Email.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        return View(users);
    }

    [Authorize(Roles = "ADMIN")]
    //acción de ver los roles de un usuario
    public async Task<IActionResult> ManageRoles(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
        {
            return NotFound();
        }

        var roles = await _context.Roles.ToListAsync(); // Todos los roles disponibles
        var userRoles = await _userManager.GetRolesAsync(user); // Roles asignados al usuario

        var model = new ManageRolesViewModel
        {
            UserId = user.Id,
            UserEmail = user.Email,
            AvailableRoles = roles.Select(r => r.Name).ToList(),
            AssignedRoles = userRoles.ToList()
        };

        return View(model);
    }

    [Authorize(Roles = "ADMIN")]
    [HttpPost]//Accion de asignación de roles
    public async Task<IActionResult> AssignRole(string userId, string roleName)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
        {
            return NotFound();
        }

        var roleExists = await _context.Roles.AnyAsync(r => r.Name == roleName);
        if (!roleExists)
        {
            ModelState.AddModelError("", "El rol no existe.");
            return RedirectToAction(nameof(ManageRoles), new { userId });
        }

        var result = await _userManager.AddToRoleAsync(user, roleName);
        if (!result.Succeeded)
        {
            ModelState.AddModelError("", "No se pudo asignar el rol.");
        }

        return RedirectToAction(nameof(ManageRoles), new { userId });
    }

    [Authorize(Roles = "ADMIN")]
    [HttpPost]//acción para desasignar roles de un usuario
    public async Task<IActionResult> RemoveRole(string userId, string roleName)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
        {
            return NotFound();
        }

        // Verificar si el rol que se intenta eliminar es ADMIN
        if (roleName == "ADMIN")
        {
            // Contar cuántos usuarios tienen el rol ADMIN
            var adminUsers = await _userManager.GetUsersInRoleAsync("ADMIN");

            // Si solo hay un usuario con el rol ADMIN y es el que intenta eliminarlo, mostrar alerta
            if (adminUsers.Count == 1 && adminUsers.Any(u => u.Id == userId))
            {
                TempData["Error"] = "No se puede eliminar el rol ADMIN porque debe haber al menos un usuario con este rol.";
                return RedirectToAction(nameof(ManageRoles), new { userId });
            }
        }

        var result = await _userManager.RemoveFromRoleAsync(user, roleName);
        if (!result.Succeeded)
        {
            TempData["Error"] = "No se pudo desasignar el rol.";
        }

        return RedirectToAction(nameof(ManageRoles), new { userId });
    }

}