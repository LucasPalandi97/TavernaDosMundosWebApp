using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TdM.Database.Migrations
{
    /// <inheritdoc />
    public partial class addUrlAndDate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Data",
                table: "Contos",
                newName: "PublishedDate");

            migrationBuilder.AddColumn<DateTime>(
                name: "PublishedDate",
                table: "Regioes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "UrlHandle",
                table: "Regioes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "PublishedDate",
                table: "Povos",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "UrlHandle",
                table: "Povos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "PublishedDate",
                table: "Personagens",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "UrlHandle",
                table: "Personagens",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "PublishedDate",
                table: "Mundos",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "UrlHandle",
                table: "Mundos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "PublishedDate",
                table: "Criaturas",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "UrlHandle",
                table: "Criaturas",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UrlHandle",
                table: "Contos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "PublishedDate",
                table: "Continentes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "UrlHandle",
                table: "Continentes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PublishedDate",
                table: "Regioes");

            migrationBuilder.DropColumn(
                name: "UrlHandle",
                table: "Regioes");

            migrationBuilder.DropColumn(
                name: "PublishedDate",
                table: "Povos");

            migrationBuilder.DropColumn(
                name: "UrlHandle",
                table: "Povos");

            migrationBuilder.DropColumn(
                name: "PublishedDate",
                table: "Personagens");

            migrationBuilder.DropColumn(
                name: "UrlHandle",
                table: "Personagens");

            migrationBuilder.DropColumn(
                name: "PublishedDate",
                table: "Mundos");

            migrationBuilder.DropColumn(
                name: "UrlHandle",
                table: "Mundos");

            migrationBuilder.DropColumn(
                name: "PublishedDate",
                table: "Criaturas");

            migrationBuilder.DropColumn(
                name: "UrlHandle",
                table: "Criaturas");

            migrationBuilder.DropColumn(
                name: "UrlHandle",
                table: "Contos");

            migrationBuilder.DropColumn(
                name: "PublishedDate",
                table: "Continentes");

            migrationBuilder.DropColumn(
                name: "UrlHandle",
                table: "Continentes");

            migrationBuilder.RenameColumn(
                name: "PublishedDate",
                table: "Contos",
                newName: "Data");
        }
    }
}
