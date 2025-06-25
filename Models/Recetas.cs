namespace ProyectoPROGEND.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
public class Recetas{
    public int Id {get;set;}
    [Required(ErrorMessage = "El nombre de la receta es obligatorio.")]
    public string? Nombre { get; set; }
    public string? Image_Portada { get; set; }
    [Required(ErrorMessage = "Los Ingredientes de la receta es obligatoria.")]
    public string? Ingredientes { get; set; }
    [Required(ErrorMessage = "El tiempo de preparación de la receta es obligatorio.")]
    public string? TiempoPreparacion { get; set; } // Tiempo que se dilata la receta en prepararse
    [Required(ErrorMessage = "El número de porciones de la receta es obligatorio.")]
    public string? Porciones { get; set; } // Porciones por persona
    [Required(ErrorMessage = "El tipo de comida de la receta es obligatorio.")]
    public string? TipoComida { get; set; } // Si es desayuno, halmuerzo, cena o merienda...
    [Required(ErrorMessage = "La dificultad de la receta es obligatoria.")]
    public string? Dificultad { get; set; } // Dificultad a la hora de preparar
    [Required(ErrorMessage = "Los Beneficios de la receta es obligatorio.")]
    public string? Beneficios { get; set; } // Beneficios nutricionales
    [Required(ErrorMessage = "Las Instrucciones de la receta son obligatorias.")]
    public string? Instrucciones { get; set; }
    [Required(ErrorMessage = "Las calorías de la receta son obligatorias.")]
    [Range(0, float.MaxValue, ErrorMessage = "Las calorías deben ser un número positivo.")]
    public float Calorias { get; set; }
    [Required(ErrorMessage = "Las Proteinas de la receta son obligatorias.")]
    public float Proteinas { get; set; }
    [Required(ErrorMessage = "Los carbohidratos de la receta son obligatorios.")]
    public float Carbohidratos { get; set; }

    
}