namespace ProyectoPROGEND.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
public class DatosUser{
        
        public int Id {get;set;}
        public double CaloriasConsumidas {get;set;}
        public double CaloriasQuemadas {get;set;}
        public float Peso {get;set;}
        public float Altura {get;set;}
        public DateTime RecordDate {get;set;}
        public string UserId { get; set; } 
        public ApplicationUser User { get; set; }
}