namespace ProyectoPROGEND.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
public class PlanEntranamiento{
    public int Id {get;set;}
    public string? Nombre {get;set;}
    public string? Image_Portada {get;set;}
    public string? TipoEntrenamiento {get;set;} // cardio, fuerza, HIIT, yoga, etc...
    public string? Equipamiento {get;set;} // Requiere pesas, ligas, máquinas o es sin equipo
    public string? Frecuencia {get;set;} // días por semana debe realizarse el plan
    public string? Objetivo {get;set;} // pérdida de peso, ganancia muscular, mejora de resistencia, etc.
    
    public string? Descripcion { get; set; }
    public string? Duracion{get;set;}
    public int Dificultad{get;set;}
    public int Nivel {get;set;}
    public int EdadMinRecom {get; set;}
    public int EdadMaxRecom {get; set;}
    public int PesoMaxRecom {get; set;}
    public int PesoMinRecom {get; set;}
    public int AlturaMinRecom {get; set;}
    public int AlturaMaxRecom {get; set;}

}