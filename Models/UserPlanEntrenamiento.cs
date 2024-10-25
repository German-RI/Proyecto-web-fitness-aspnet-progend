namespace ProyectoPROGEND.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

public class UserPlanesEntrenamiento
{
    public int Id { get; set; }
    public string UserId { get; set; }
    public ApplicationUser User { get; set; }
    public int PlanEntrenamientoId { get; set; }
    public PlanEntranamiento PlanEntrenamiento { get; set;}
}