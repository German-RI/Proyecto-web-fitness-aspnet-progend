using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProyectoPROGEND.Migrations
{
    /// <inheritdoc />
    public partial class RemoveUserColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                name: "Discriminator",
                table: "AspNetUsers");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
    }
}
