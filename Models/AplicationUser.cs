namespace ProyectoPROGEND.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Collections.Generic;

public class ApplicationUser : IdentityUser
{
    // Propiedades personalizadas
    public DateTime FechaNacimiento { get; set; } 
    public string Sexo { get; set; }
    public ICollection<UserRecetas> RecetasUsuarios { get; set; }
    public ICollection<UserPlanesEntrenamiento> PlanesUsuarios { get; set; }
    public ICollection<DatosUser> DatosUsuarios { get; set; }
}
