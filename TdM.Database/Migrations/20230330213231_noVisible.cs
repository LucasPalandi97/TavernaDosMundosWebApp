using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TdM.Web.Migrations
{
    /// <inheritdoc />
    public partial class noVisible : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Visible",
                table: "Mundos");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Visible",
                table: "Mundos",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
