namespace ProyectoPROGEND.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
public class PlanEntranamiento{
    public int Id {get;set;}
    public string? Nombre {get;set;}
    public string? Descripcion{get;set;}
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