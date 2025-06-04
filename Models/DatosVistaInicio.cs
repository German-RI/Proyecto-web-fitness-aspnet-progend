namespace ProyectoPROGEND.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

public class DatosVistaInicio
{
    public int Id { get; set; }
    public string? LinkVideo { get; set; }
    public string? Acercadenosotros { get; set; }
}