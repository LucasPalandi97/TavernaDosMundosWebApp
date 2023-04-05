using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TdM.Database.Migrations
{
    /// <inheritdoc />
    public partial class AddImgCardImgBox : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImgSrc",
                table: "Regioes",
                newName: "ImgCard");

            migrationBuilder.RenameColumn(
                name: "ImgSrc",
                table: "Povos",
                newName: "ImgCard");

            migrationBuilder.RenameColumn(
                name: "ImgSrc",
                table: "Personagens",
                newName: "ImgCard");

            migrationBuilder.RenameColumn(
                name: "ImgSrc",
                table: "Mundos",
                newName: "ImgBox");

            migrationBuilder.RenameColumn(
                name: "ImgSrc",
                table: "Criaturas",
                newName: "ImgCard");

            migrationBuilder.RenameColumn(
                name: "ImgSrc",
                table: "Contos",
                newName: "ImgCard");

            migrationBuilder.RenameColumn(
                name: "ImgSrc",
                table: "Continentes",
                newName: "ImgCard");

            migrationBuilder.AlterColumn<string>(
                name: "Simbolo",
                table: "Regioes",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(3)",
                oldMaxLength: 3);

            migrationBuilder.AddColumn<string>(
                name: "ImgBox",
                table: "Regioes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImgBox",
                table: "Povos",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImgBox",
                table: "Personagens",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImgBox",
                table: "Criaturas",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImgBox",
                table: "Contos",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImgBox",
                table: "Continentes",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImgBox",
                table: "Regioes");

            migrationBuilder.DropColumn(
                name: "ImgBox",
                table: "Povos");

            migrationBuilder.DropColumn(
                name: "ImgBox",
                table: "Personagens");

            migrationBuilder.DropColumn(
                name: "ImgBox",
                table: "Criaturas");

            migrationBuilder.DropColumn(
                name: "ImgBox",
                table: "Contos");

            migrationBuilder.DropColumn(
                name: "ImgBox",
                table: "Continentes");

            migrationBuilder.RenameColumn(
                name: "ImgCard",
                table: "Regioes",
                newName: "ImgSrc");

            migrationBuilder.RenameColumn(
                name: "ImgCard",
                table: "Povos",
                newName: "ImgSrc");

            migrationBuilder.RenameColumn(
                name: "ImgCard",
                table: "Personagens",
                newName: "ImgSrc");

            migrationBuilder.RenameColumn(
                name: "ImgBox",
                table: "Mundos",
                newName: "ImgSrc");

            migrationBuilder.RenameColumn(
                name: "ImgCard",
                table: "Criaturas",
                newName: "ImgSrc");

            migrationBuilder.RenameColumn(
                name: "ImgCard",
                table: "Contos",
                newName: "ImgSrc");

            migrationBuilder.RenameColumn(
                name: "ImgCard",
                table: "Continentes",
                newName: "ImgSrc");

            migrationBuilder.AlterColumn<string>(
                name: "Simbolo",
                table: "Regioes",
                type: "nvarchar(3)",
                maxLength: 3,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
