using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TdM.Database.Migrations
{
    /// <inheritdoc />
    public partial class ShortDescriptionAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CurtaDescricao",
                table: "Regioes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CurtaDescricao",
                table: "Povos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CurtaDescricao",
                table: "Personagens",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CurtaDescricao",
                table: "Mundos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CurtaDescricao",
                table: "Criaturas",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CurtaDescricao",
                table: "Continentes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurtaDescricao",
                table: "Regioes");

            migrationBuilder.DropColumn(
                name: "CurtaDescricao",
                table: "Povos");

            migrationBuilder.DropColumn(
                name: "CurtaDescricao",
                table: "Personagens");

            migrationBuilder.DropColumn(
                name: "CurtaDescricao",
                table: "Mundos");

            migrationBuilder.DropColumn(
                name: "CurtaDescricao",
                table: "Criaturas");

            migrationBuilder.DropColumn(
                name: "CurtaDescricao",
                table: "Continentes");
        }
    }
}
