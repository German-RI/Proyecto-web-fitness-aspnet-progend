namespace ProyectoPROGEND.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Collections.Generic;

public class ApplicationUser : IdentityUser
{
    // Propiedades personalizadas
    public ICollection<UserRecetas> RecetasUsuarios { get; set; }
    public ICollection<UserPlanesEntrenamiento> PlanesUsuarios { get; set; }
}
