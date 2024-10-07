namespace ProyectoPROGEND.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
public class Progreso{
    public int Id {get;set;}
    public float Cal_consumida{get;set;}
    public float Cal_quemadas{get;set;}
    public float Agua_ingerida{get;set;}
}