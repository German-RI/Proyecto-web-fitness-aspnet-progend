using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProyectoPROGEND.Migrations
{
    /// <inheritdoc />
    public partial class Added_Model_Relacionentreplanesdeentrenamientoyrecetas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Recetas",
                keyColumn: "TipoComida",
                keyValue: null,
                column: "TipoComida",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "TipoComida",
                table: "Recetas",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Recetas",
                keyColumn: "TiempoPreparacion",
                keyValue: null,
                column: "TiempoPreparacion",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "TiempoPreparacion",
                table: "Recetas",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Recetas",
                keyColumn: "Porciones",
                keyValue: null,
                column: "Porciones",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "Porciones",
                table: "Recetas",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Recetas",
                keyColumn: "Nombre",
                keyValue: null,
                column: "Nombre",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "Nombre",
                table: "Recetas",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Recetas",
                keyColumn: "Instrucciones",
                keyValue: null,
                column: "Instrucciones",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "Instrucciones",
                table: "Recetas",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Recetas",
                keyColumn: "Ingredientes",
                keyValue: null,
                column: "Ingredientes",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "Ingredientes",
                table: "Recetas",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Recetas",
                keyColumn: "Dificultad",
                keyValue: null,
                column: "Dificultad",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "Dificultad",
                table: "Recetas",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Recetas",
                keyColumn: "Beneficios",
                keyValue: null,
                column: "Beneficios",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "Beneficios",
                table: "Recetas",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "PlanEntranamiento",
                keyColumn: "TipoEntrenamiento",
                keyValue: null,
                column: "TipoEntrenamiento",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "TipoEntrenamiento",
                table: "PlanEntranamiento",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "PlanEntranamiento",
                keyColumn: "Objetivo",
                keyValue: null,
                column: "Objetivo",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "Objetivo",
                table: "PlanEntranamiento",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "PlanEntranamiento",
                keyColumn: "Nombre",
                keyValue: null,
                column: "Nombre",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "Nombre",
                table: "PlanEntranamiento",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "PlanEntranamiento",
                keyColumn: "Frecuencia",
                keyValue: null,
                column: "Frecuencia",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "Frecuencia",
                table: "PlanEntranamiento",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "PlanEntranamiento",
                keyColumn: "Equipamiento",
                keyValue: null,
                column: "Equipamiento",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "Equipamiento",
                table: "PlanEntranamiento",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "PlanEntranamiento",
                keyColumn: "Duracion",
                keyValue: null,
                column: "Duracion",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "Duracion",
                table: "PlanEntranamiento",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "PlanEntranamiento",
                keyColumn: "Descripcion",
                keyValue: null,
                column: "Descripcion",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "Descripcion",
                table: "PlanEntranamiento",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "RelacionEntrenamientoRecetas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    RecetasId = table.Column<int>(type: "int", nullable: false),
                    PlanEntrenamientoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RelacionEntrenamientoRecetas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RelacionEntrenamientoRecetas_PlanEntranamiento_PlanEntrenami~",
                        column: x => x.PlanEntrenamientoId,
                        principalTable: "PlanEntranamiento",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RelacionEntrenamientoRecetas_Recetas_RecetasId",
                        column: x => x.RecetasId,
                        principalTable: "Recetas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_RelacionEntrenamientoRecetas_PlanEntrenamientoId",
                table: "RelacionEntrenamientoRecetas",
                column: "PlanEntrenamientoId");

            migrationBuilder.CreateIndex(
                name: "IX_RelacionEntrenamientoRecetas_RecetasId",
                table: "RelacionEntrenamientoRecetas",
                column: "RecetasId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RelacionEntrenamientoRecetas");

            migrationBuilder.AlterColumn<string>(
                name: "TipoComida",
                table: "Recetas",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "TiempoPreparacion",
                table: "Recetas",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Porciones",
                table: "Recetas",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Nombre",
                table: "Recetas",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Instrucciones",
                table: "Recetas",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Ingredientes",
                table: "Recetas",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Dificultad",
                table: "Recetas",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Beneficios",
                table: "Recetas",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "TipoEntrenamiento",
                table: "PlanEntranamiento",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Objetivo",
                table: "PlanEntranamiento",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Nombre",
                table: "PlanEntranamiento",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Frecuencia",
                table: "PlanEntranamiento",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Equipamiento",
                table: "PlanEntranamiento",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Duracion",
                table: "PlanEntranamiento",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Descripcion",
                table: "PlanEntranamiento",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");
        }
    }
}
