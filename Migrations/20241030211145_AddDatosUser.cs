using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProyectoPROGEND.Migrations
{
    /// <inheritdoc />
    public partial class AddDatosUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "DatosUsers",
                type: "varchar(255)",
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_DatosUsers_UserId",
                table: "DatosUsers",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_DatosUsers_AspNetUsers_UserId",
                table: "DatosUsers",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DatosUsers_AspNetUsers_UserId",
                table: "DatosUsers");

            migrationBuilder.DropIndex(
                name: "IX_DatosUsers_UserId",
                table: "DatosUsers");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "DatosUsers");
        }
    }
}
