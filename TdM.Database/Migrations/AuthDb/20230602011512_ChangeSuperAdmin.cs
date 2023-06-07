using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TdM.Database.Migrations.AuthDb
{
    /// <inheritdoc />
    public partial class ChangeSuperAdmin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "4971067b-f956-43c4-bf06-9ddbf7f0840e", "467c51e6-2773-4676-be93-ccc8f7b64de9" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "52d3b884-61d9-4cca-8430-9755b38aab9a", "467c51e6-2773-4676-be93-ccc8f7b64de9" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "728b2541-6b59-4e4b-8903-932da4896354", "467c51e6-2773-4676-be93-ccc8f7b64de9" });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "467c51e6-2773-4676-be93-ccc8f7b64de9");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "FE39CB62-A779-41D5-A2A7-F35FB81D6719", 0, "20079f5d-0428-4c48-bda2-2e3db0571546", "lucaspalandi97@gmail.com", false, false, null, "LUCASPALANDI97@GMAIL.COM", "PALANDI", "AQAAAAIAAYagAAAAEOjhfg8YkeqexsTkXUfXUfyxlOX4NUjqV4AzdXYwXlvX3/3l3rb9X93YCZIKktU6Ww==", null, false, "ad177d21-1609-4b41-b847-3477a2e21fdf", false, "Palandi" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "4971067b-f956-43c4-bf06-9ddbf7f0840e", "FE39CB62-A779-41D5-A2A7-F35FB81D6719" },
                    { "52d3b884-61d9-4cca-8430-9755b38aab9a", "FE39CB62-A779-41D5-A2A7-F35FB81D6719" },
                    { "728b2541-6b59-4e4b-8903-932da4896354", "FE39CB62-A779-41D5-A2A7-F35FB81D6719" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "4971067b-f956-43c4-bf06-9ddbf7f0840e", "FE39CB62-A779-41D5-A2A7-F35FB81D6719" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "52d3b884-61d9-4cca-8430-9755b38aab9a", "FE39CB62-A779-41D5-A2A7-F35FB81D6719" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "728b2541-6b59-4e4b-8903-932da4896354", "FE39CB62-A779-41D5-A2A7-F35FB81D6719" });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "FE39CB62-A779-41D5-A2A7-F35FB81D6719");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "467c51e6-2773-4676-be93-ccc8f7b64de9", 0, "79cc81c3-82f8-4981-949e-ad6a92a43a0c", "superadmin@taverna.com", false, false, null, "SUPERADMIN@TAVERNA.COM", "SUPERADMIN@TAVERNA.COM", "AQAAAAIAAYagAAAAEFcVyK/8BDmj5l5vWrL1Vr8A8fsOJFSJNIw7IbHobJ44y5aVORwP66zyL3SGl8EaNw==", null, false, "777afdd7-2def-4b7d-8d86-3b1d9f117e9c", false, "superadmin@taverna.com" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "4971067b-f956-43c4-bf06-9ddbf7f0840e", "467c51e6-2773-4676-be93-ccc8f7b64de9" },
                    { "52d3b884-61d9-4cca-8430-9755b38aab9a", "467c51e6-2773-4676-be93-ccc8f7b64de9" },
                    { "728b2541-6b59-4e4b-8903-932da4896354", "467c51e6-2773-4676-be93-ccc8f7b64de9" }
                });
        }
    }
}
