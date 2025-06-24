namespace ProyectoPROGEND.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;


public class PlanEntranamiento
{
    public int Id { get; set; }

    [Required(ErrorMessage = "El nombre del plan es obligatorio.")]
    public string? Nombre { get; set; }
    public string? Image_Portada { get; set; }

    [Required(ErrorMessage = "El tipo de entrenamiento es obligatorio.")]
    public string? TipoEntrenamiento { get; set; }

    [Required(ErrorMessage = "El equipamiento necesario es obligatorio.")]
    public string? Equipamiento { get; set; }

    [Required(ErrorMessage = "La frecuencia de entrenamiento es obligatoria.")]
    public string? Frecuencia { get; set; }

    [Required(ErrorMessage = "El objetivo del plan es obligatorio.")]
    public string? Objetivo { get; set; }

    [Required(ErrorMessage = "La duración del plan es obligatoria.")]
    public string? Duracion { get; set; }
    [Required(ErrorMessage = "El nivel de intensidad es obligatorio.")]
    [Range(1, 5, ErrorMessage = "La dificultad debe estar entre 1 y 5.")]
    public int Dificultad { get; set; }
    [Required(ErrorMessage = "El nivel de entrenamiento es obligatorio.")]
    [Range(1, 5, ErrorMessage = "El nivel debe estar entre 1 y 5.")]
    public int Nivel { get; set; }

    [Required(ErrorMessage = "La descripción es obligatoria.")]
    public string? Descripcion { get; set; }
    [Required(ErrorMessage = "La edad Mínima es obligatoria.")]
    public int EdadMinRecom { get; set; }

    [Required(ErrorMessage = "La edad Máxima es obligatoria.")]
    public int EdadMaxRecom { get; set; }
    [Required(ErrorMessage = "El peso mínimo recomendado es obligatorio.")]
    public int PesoMinRecom { get; set; }

    [Required(ErrorMessage = "El peso máximo recomendado es obligatorio.")]
    public int PesoMaxRecom { get; set; }

    [Required(ErrorMessage = "La altura mínima recomendada es obligatoria.")]
    public int AlturaMinRecom { get; set; }

    [Required(ErrorMessage = "La altura máxima recomendada es obligatoria.")]
    public int AlturaMaxRecom { get; set; }
}