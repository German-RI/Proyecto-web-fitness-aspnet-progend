namespace ProyectoPROGEND.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

public class RelacionEntrenamientoRecetas
{
    public int Id { get; set; }
    public int RecetasId { get; set; }
    public Recetas Recetas { get; set; }
    public int PlanEntrenamientoId { get; set; }
    public PlanEntranamiento PlanEntrenamiento { get; set; }
}