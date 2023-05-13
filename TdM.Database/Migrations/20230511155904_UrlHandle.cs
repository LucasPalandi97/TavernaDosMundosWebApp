using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TdM.Database.Migrations
{
    /// <inheritdoc />
    public partial class UrlHandle : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "Regioes",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(32)",
                oldMaxLength: 32);

            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "Povos",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(32)",
                oldMaxLength: 32);

            migrationBuilder.AlterColumn<string>(
                name: "Titulo",
                table: "Personagens",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(32)",
                oldMaxLength: 32);

            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "Personagens",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(32)",
                oldMaxLength: 32);

            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "Mundos",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(32)",
                oldMaxLength: 32);

            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "Criaturas",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(32)",
                oldMaxLength: 32);

            migrationBuilder.AlterColumn<string>(
                name: "Titulo",
                table: "Contos",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(32)",
                oldMaxLength: 32);

            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "Continentes",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(32)",
                oldMaxLength: 32);

            migrationBuilder.CreateTable(
                name: "UrlHandles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UrlHandleValue = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UrlHandles", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UrlHandles");

            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "Regioes",
                type: "nvarchar(32)",
                maxLength: 32,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "Povos",
                type: "nvarchar(32)",
                maxLength: 32,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Titulo",
                table: "Personagens",
                type: "nvarchar(32)",
                maxLength: 32,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "Personagens",
                type: "nvarchar(32)",
                maxLength: 32,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "Mundos",
                type: "nvarchar(32)",
                maxLength: 32,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "Criaturas",
                type: "nvarchar(32)",
                maxLength: 32,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Titulo",
                table: "Contos",
                type: "nvarchar(32)",
                maxLength: 32,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "Continentes",
                type: "nvarchar(32)",
                maxLength: 32,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
