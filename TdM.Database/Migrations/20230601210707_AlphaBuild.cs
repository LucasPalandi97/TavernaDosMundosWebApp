using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TdM.Database.Migrations
{
    /// <inheritdoc />
    public partial class AlphaBuild : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UrlHandles");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
    }
}
