namespace ProyectoPROGEND.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
public class DatosUser{
        public int Id {get;set;}
        public int Edad {get;set;}
        public float Peso {get;set;}
        public float Altura {get;set;}
        
        /* ref a su usuario
                pubic int IdUser {get;set;}
        */
}