using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProyectoPROGEND.Migrations
{
    /// <inheritdoc />
    public partial class Updated_Models_PlanEntrenamiento_y_Recetas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Beneficios",
                table: "Recetas",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Dificultad",
                table: "Recetas",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Porciones",
                table: "Recetas",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "TiempoPreparacion",
                table: "Recetas",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "TipoComida",
                table: "Recetas",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Equipamiento",
                table: "PlanEntranamiento",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Frecuencia",
                table: "PlanEntranamiento",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Objetivo",
                table: "PlanEntranamiento",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "TipoEntrenamiento",
                table: "PlanEntranamiento",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Beneficios",
                table: "Recetas");

            migrationBuilder.DropColumn(
                name: "Dificultad",
                table: "Recetas");

            migrationBuilder.DropColumn(
                name: "Porciones",
                table: "Recetas");

            migrationBuilder.DropColumn(
                name: "TiempoPreparacion",
                table: "Recetas");

            migrationBuilder.DropColumn(
                name: "TipoComida",
                table: "Recetas");

            migrationBuilder.DropColumn(
                name: "Equipamiento",
                table: "PlanEntranamiento");

            migrationBuilder.DropColumn(
                name: "Frecuencia",
                table: "PlanEntranamiento");

            migrationBuilder.DropColumn(
                name: "Objetivo",
                table: "PlanEntranamiento");

            migrationBuilder.DropColumn(
                name: "TipoEntrenamiento",
                table: "PlanEntranamiento");
        }
    }
}
