namespace ProyectoPROGEND.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

public class PlanesYrecetasViewModel
{
    public List<PlanEntranamiento> Planes { get; set; }
    public List<Recetas> Recetas { get; set; }
    public List<DatosUser> DatosUsuario { get; set; }
    
    public List<PlanEntranamiento> RecomendacionPlanes { get; set; }
    public List<Recetas> RecomendacionRecetas { get; set; }
}