using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProyectoPROGEND.Migrations
{
    /// <inheritdoc />
    public partial class newcampos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "Carbohidratos",
                table: "Recetas",
                type: "float",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "Proteinas",
                table: "Recetas",
                type: "float",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<int>(
                name: "AlturaMaxRecom",
                table: "PlanEntranamiento",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AlturaMinRecom",
                table: "PlanEntranamiento",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "EdadMaxRecom",
                table: "PlanEntranamiento",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "EdadMinRecom",
                table: "PlanEntranamiento",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PesoMaxRecom",
                table: "PlanEntranamiento",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PesoMinRecom",
                table: "PlanEntranamiento",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Carbohidratos",
                table: "Recetas");

            migrationBuilder.DropColumn(
                name: "Proteinas",
                table: "Recetas");

            migrationBuilder.DropColumn(
                name: "AlturaMaxRecom",
                table: "PlanEntranamiento");

            migrationBuilder.DropColumn(
                name: "AlturaMinRecom",
                table: "PlanEntranamiento");

            migrationBuilder.DropColumn(
                name: "EdadMaxRecom",
                table: "PlanEntranamiento");

            migrationBuilder.DropColumn(
                name: "EdadMinRecom",
                table: "PlanEntranamiento");

            migrationBuilder.DropColumn(
                name: "PesoMaxRecom",
                table: "PlanEntranamiento");

            migrationBuilder.DropColumn(
                name: "PesoMinRecom",
                table: "PlanEntranamiento");
        }
    }
}
