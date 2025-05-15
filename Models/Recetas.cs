namespace ProyectoPROGEND.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
public class Recetas{
    public int Id {get;set;}
    public string? Nombre{get;set;}
    public string? Image_Portada {get;set;}
    public string? Ingredientes {get;set;}
    public string? Instrucciones {get;set;}
    public float Calorias {get;set;}
    public float Proteinas {get;set;}
    public float Carbohidratos {get;set;}

    
}