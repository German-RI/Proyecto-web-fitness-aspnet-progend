using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProyectoPROGEND.Migrations
{
    /// <inheritdoc />
    public partial class AddUserRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Edad",
                table: "DatosUsers");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Recetas",
                type: "varchar(255)",
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "Recetas",
                type: "varchar(255)",
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "PlanEntranamiento",
                type: "varchar(255)",
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "PlanEntranamiento",
                type: "varchar(255)",
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<double>(
                name: "CaloriasConsumidas",
                table: "DatosUsers",
                type: "double",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "CaloriasQuemadas",
                table: "DatosUsers",
                type: "double",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<DateTime>(
                name: "RecordDate",
                table: "DatosUsers",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                type: "varchar(21)",
                maxLength: 21,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Recetas_UserId",
                table: "Recetas",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Recetas_UserId1",
                table: "Recetas",
                column: "UserId1");

            migrationBuilder.CreateIndex(
                name: "IX_PlanEntranamiento_UserId",
                table: "PlanEntranamiento",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_PlanEntranamiento_UserId1",
                table: "PlanEntranamiento",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_PlanEntranamiento_AspNetUsers_UserId",
                table: "PlanEntranamiento",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PlanEntranamiento_AspNetUsers_UserId1",
                table: "PlanEntranamiento",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Recetas_AspNetUsers_UserId",
                table: "Recetas",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Recetas_AspNetUsers_UserId1",
                table: "Recetas",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlanEntranamiento_AspNetUsers_UserId",
                table: "PlanEntranamiento");

            migrationBuilder.DropForeignKey(
                name: "FK_PlanEntranamiento_AspNetUsers_UserId1",
                table: "PlanEntranamiento");

            migrationBuilder.DropForeignKey(
                name: "FK_Recetas_AspNetUsers_UserId",
                table: "Recetas");

            migrationBuilder.DropForeignKey(
                name: "FK_Recetas_AspNetUsers_UserId1",
                table: "Recetas");

            migrationBuilder.DropIndex(
                name: "IX_Recetas_UserId",
                table: "Recetas");

            migrationBuilder.DropIndex(
                name: "IX_Recetas_UserId1",
                table: "Recetas");

            migrationBuilder.DropIndex(
                name: "IX_PlanEntranamiento_UserId",
                table: "PlanEntranamiento");

            migrationBuilder.DropIndex(
                name: "IX_PlanEntranamiento_UserId1",
                table: "PlanEntranamiento");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Recetas");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "Recetas");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "PlanEntranamiento");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "PlanEntranamiento");

            migrationBuilder.DropColumn(
                name: "CaloriasConsumidas",
                table: "DatosUsers");

            migrationBuilder.DropColumn(
                name: "CaloriasQuemadas",
                table: "DatosUsers");

            migrationBuilder.DropColumn(
                name: "RecordDate",
                table: "DatosUsers");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<int>(
                name: "Edad",
                table: "DatosUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
