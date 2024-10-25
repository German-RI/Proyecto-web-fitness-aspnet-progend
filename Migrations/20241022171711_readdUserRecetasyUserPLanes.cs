using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProyectoPROGEND.Migrations
{
    /// <inheritdoc />
    public partial class readdUserRecetasyUserPLanes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserPlanesFavorites_AspNetUsers_ApplicationUserId",
                table: "UserPlanesFavorites");

            migrationBuilder.DropForeignKey(
                name: "FK_UserPlanesFavorites_AspNetUsers_UserId",
                table: "UserPlanesFavorites");

            migrationBuilder.DropForeignKey(
                name: "FK_UserPlanesFavorites_PlanEntranamiento_PlanEntrenamientoId",
                table: "UserPlanesFavorites");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRecetasFavorites_AspNetUsers_UserId",
                table: "UserRecetasFavorites");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRecetasFavorites_Recetas_RecetaId",
                table: "UserRecetasFavorites");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserRecetasFavorites",
                table: "UserRecetasFavorites");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserPlanesFavorites",
                table: "UserPlanesFavorites");

            migrationBuilder.RenameTable(
                name: "UserRecetasFavorites",
                newName: "UserRecetas");

            migrationBuilder.RenameTable(
                name: "UserPlanesFavorites",
                newName: "UserPlanesEntrenamientos");

            migrationBuilder.RenameIndex(
                name: "IX_UserRecetasFavorites_UserId",
                table: "UserRecetas",
                newName: "IX_UserRecetas_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_UserRecetasFavorites_RecetaId",
                table: "UserRecetas",
                newName: "IX_UserRecetas_RecetaId");

            migrationBuilder.RenameIndex(
                name: "IX_UserPlanesFavorites_UserId",
                table: "UserPlanesEntrenamientos",
                newName: "IX_UserPlanesEntrenamientos_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_UserPlanesFavorites_PlanEntrenamientoId",
                table: "UserPlanesEntrenamientos",
                newName: "IX_UserPlanesEntrenamientos_PlanEntrenamientoId");

            migrationBuilder.RenameIndex(
                name: "IX_UserPlanesFavorites_ApplicationUserId",
                table: "UserPlanesEntrenamientos",
                newName: "IX_UserPlanesEntrenamientos_ApplicationUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserRecetas",
                table: "UserRecetas",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserPlanesEntrenamientos",
                table: "UserPlanesEntrenamientos",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserPlanesEntrenamientos_AspNetUsers_ApplicationUserId",
                table: "UserPlanesEntrenamientos",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserPlanesEntrenamientos_AspNetUsers_UserId",
                table: "UserPlanesEntrenamientos",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserPlanesEntrenamientos_PlanEntranamiento_PlanEntrenamiento~",
                table: "UserPlanesEntrenamientos",
                column: "PlanEntrenamientoId",
                principalTable: "PlanEntranamiento",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRecetas_AspNetUsers_UserId",
                table: "UserRecetas",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRecetas_Recetas_RecetaId",
                table: "UserRecetas",
                column: "RecetaId",
                principalTable: "Recetas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserPlanesEntrenamientos_AspNetUsers_ApplicationUserId",
                table: "UserPlanesEntrenamientos");

            migrationBuilder.DropForeignKey(
                name: "FK_UserPlanesEntrenamientos_AspNetUsers_UserId",
                table: "UserPlanesEntrenamientos");

            migrationBuilder.DropForeignKey(
                name: "FK_UserPlanesEntrenamientos_PlanEntranamiento_PlanEntrenamiento~",
                table: "UserPlanesEntrenamientos");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRecetas_AspNetUsers_UserId",
                table: "UserRecetas");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRecetas_Recetas_RecetaId",
                table: "UserRecetas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserRecetas",
                table: "UserRecetas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserPlanesEntrenamientos",
                table: "UserPlanesEntrenamientos");

            migrationBuilder.RenameTable(
                name: "UserRecetas",
                newName: "UserRecetasFavorites");

            migrationBuilder.RenameTable(
                name: "UserPlanesEntrenamientos",
                newName: "UserPlanesFavorites");

            migrationBuilder.RenameIndex(
                name: "IX_UserRecetas_UserId",
                table: "UserRecetasFavorites",
                newName: "IX_UserRecetasFavorites_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_UserRecetas_RecetaId",
                table: "UserRecetasFavorites",
                newName: "IX_UserRecetasFavorites_RecetaId");

            migrationBuilder.RenameIndex(
                name: "IX_UserPlanesEntrenamientos_UserId",
                table: "UserPlanesFavorites",
                newName: "IX_UserPlanesFavorites_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_UserPlanesEntrenamientos_PlanEntrenamientoId",
                table: "UserPlanesFavorites",
                newName: "IX_UserPlanesFavorites_PlanEntrenamientoId");

            migrationBuilder.RenameIndex(
                name: "IX_UserPlanesEntrenamientos_ApplicationUserId",
                table: "UserPlanesFavorites",
                newName: "IX_UserPlanesFavorites_ApplicationUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserRecetasFavorites",
                table: "UserRecetasFavorites",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserPlanesFavorites",
                table: "UserPlanesFavorites",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserPlanesFavorites_AspNetUsers_ApplicationUserId",
                table: "UserPlanesFavorites",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserPlanesFavorites_AspNetUsers_UserId",
                table: "UserPlanesFavorites",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserPlanesFavorites_PlanEntranamiento_PlanEntrenamientoId",
                table: "UserPlanesFavorites",
                column: "PlanEntrenamientoId",
                principalTable: "PlanEntranamiento",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRecetasFavorites_AspNetUsers_UserId",
                table: "UserRecetasFavorites",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRecetasFavorites_Recetas_RecetaId",
                table: "UserRecetasFavorites",
                column: "RecetaId",
                principalTable: "Recetas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
