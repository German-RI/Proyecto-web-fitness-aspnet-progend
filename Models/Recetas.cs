namespace ProyectoPROGEND.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
public class Recetas{
    public int Id {get;set;}
    public string? Nombre{get;set;}
    public string? Image_Portada {get;set;}
    public string? Ingredientes {get;set;}
    public string? TiempoPreparacion { get; set; } // Tiempo que se dilata la receta en prepararse
    public string? Porciones { get; set; } // Porciones por persona
    public string? TipoComida { get; set; } // Si es desayuno, halmuerzo, cena o merienda...
    public string? Dificultad { get; set; } // Dificultad a la hora de preparar
    public string? Beneficios { get; set; } // Beneficios nutricionales
    
    public string? Instrucciones { get; set; }
    public float Calorias {get;set;}
    public float Proteinas {get;set;}
    public float Carbohidratos {get;set;}

    
}