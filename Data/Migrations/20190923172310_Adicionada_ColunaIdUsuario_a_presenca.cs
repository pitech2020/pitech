using Microsoft.EntityFrameworkCore.Migrations;

namespace Presence.API.Data.Migrations
{
    public partial class Adicionada_ColunaIdUsuario_a_presenca : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Presencas",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Presencas_UserId",
                table: "Presencas",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Presencas_AspNetUsers_UserId",
                table: "Presencas",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Presencas_AspNetUsers_UserId",
                table: "Presencas");

            migrationBuilder.DropIndex(
                name: "IX_Presencas_UserId",
                table: "Presencas");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Presencas");
        }
    }
}
