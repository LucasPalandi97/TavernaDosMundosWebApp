using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TdM.Database.Migrations
{
    /// <inheritdoc />
    public partial class MundoFKchanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Continentes_Mundos_MundoFk",
                table: "Continentes");

            migrationBuilder.DropForeignKey(
                name: "FK_Contos_Mundos_MundoFk",
                table: "Contos");

            migrationBuilder.DropForeignKey(
                name: "FK_Criaturas_Mundos_MundoFk",
                table: "Criaturas");

            migrationBuilder.DropForeignKey(
                name: "FK_Personagens_Mundos_MundoFk",
                table: "Personagens");

            migrationBuilder.DropForeignKey(
                name: "FK_Personagens_Regioes_RegiaoFk",
                table: "Personagens");

            migrationBuilder.DropForeignKey(
                name: "FK_Povos_Mundos_MundoFk",
                table: "Povos");

            migrationBuilder.DropForeignKey(
                name: "FK_Regioes_Continentes_ContinenteFk",
                table: "Regioes");

            migrationBuilder.DropForeignKey(
                name: "FK_Regioes_Mundos_MundoFk",
                table: "Regioes");

            migrationBuilder.RenameColumn(
                name: "MundoFk",
                table: "Regioes",
                newName: "MundoFK");

            migrationBuilder.RenameColumn(
                name: "ContinenteFk",
                table: "Regioes",
                newName: "ContinenteFK");

            migrationBuilder.RenameIndex(
                name: "IX_Regioes_MundoFk",
                table: "Regioes",
                newName: "IX_Regioes_MundoFK");

            migrationBuilder.RenameIndex(
                name: "IX_Regioes_ContinenteFk",
                table: "Regioes",
                newName: "IX_Regioes_ContinenteFK");

            migrationBuilder.RenameColumn(
                name: "MundoFk",
                table: "Povos",
                newName: "MundoFK");

            migrationBuilder.RenameIndex(
                name: "IX_Povos_MundoFk",
                table: "Povos",
                newName: "IX_Povos_MundoFK");

            migrationBuilder.RenameColumn(
                name: "RegiaoFk",
                table: "Personagens",
                newName: "RegiaoFK");

            migrationBuilder.RenameColumn(
                name: "MundoFk",
                table: "Personagens",
                newName: "MundoFK");

            migrationBuilder.RenameIndex(
                name: "IX_Personagens_RegiaoFk",
                table: "Personagens",
                newName: "IX_Personagens_RegiaoFK");

            migrationBuilder.RenameIndex(
                name: "IX_Personagens_MundoFk",
                table: "Personagens",
                newName: "IX_Personagens_MundoFK");

            migrationBuilder.RenameColumn(
                name: "MundoFk",
                table: "Criaturas",
                newName: "MundoFK");

            migrationBuilder.RenameIndex(
                name: "IX_Criaturas_MundoFk",
                table: "Criaturas",
                newName: "IX_Criaturas_MundoFK");

            migrationBuilder.RenameColumn(
                name: "MundoFk",
                table: "Contos",
                newName: "MundoFK");

            migrationBuilder.RenameIndex(
                name: "IX_Contos_MundoFk",
                table: "Contos",
                newName: "IX_Contos_MundoFK");

            migrationBuilder.RenameColumn(
                name: "MundoFk",
                table: "Continentes",
                newName: "MundoFK");

            migrationBuilder.RenameIndex(
                name: "IX_Continentes_MundoFk",
                table: "Continentes",
                newName: "IX_Continentes_MundoFK");

            migrationBuilder.AddForeignKey(
                name: "FK_Continentes_Mundos_MundoFK",
                table: "Continentes",
                column: "MundoFK",
                principalTable: "Mundos",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Contos_Mundos_MundoFK",
                table: "Contos",
                column: "MundoFK",
                principalTable: "Mundos",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Criaturas_Mundos_MundoFK",
                table: "Criaturas",
                column: "MundoFK",
                principalTable: "Mundos",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Personagens_Mundos_MundoFK",
                table: "Personagens",
                column: "MundoFK",
                principalTable: "Mundos",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Personagens_Regioes_RegiaoFK",
                table: "Personagens",
                column: "RegiaoFK",
                principalTable: "Regioes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Povos_Mundos_MundoFK",
                table: "Povos",
                column: "MundoFK",
                principalTable: "Mundos",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Regioes_Continentes_ContinenteFK",
                table: "Regioes",
                column: "ContinenteFK",
                principalTable: "Continentes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Regioes_Mundos_MundoFK",
                table: "Regioes",
                column: "MundoFK",
                principalTable: "Mundos",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Continentes_Mundos_MundoFK",
                table: "Continentes");

            migrationBuilder.DropForeignKey(
                name: "FK_Contos_Mundos_MundoFK",
                table: "Contos");

            migrationBuilder.DropForeignKey(
                name: "FK_Criaturas_Mundos_MundoFK",
                table: "Criaturas");

            migrationBuilder.DropForeignKey(
                name: "FK_Personagens_Mundos_MundoFK",
                table: "Personagens");

            migrationBuilder.DropForeignKey(
                name: "FK_Personagens_Regioes_RegiaoFK",
                table: "Personagens");

            migrationBuilder.DropForeignKey(
                name: "FK_Povos_Mundos_MundoFK",
                table: "Povos");

            migrationBuilder.DropForeignKey(
                name: "FK_Regioes_Continentes_ContinenteFK",
                table: "Regioes");

            migrationBuilder.DropForeignKey(
                name: "FK_Regioes_Mundos_MundoFK",
                table: "Regioes");

            migrationBuilder.RenameColumn(
                name: "MundoFK",
                table: "Regioes",
                newName: "MundoFk");

            migrationBuilder.RenameColumn(
                name: "ContinenteFK",
                table: "Regioes",
                newName: "ContinenteFk");

            migrationBuilder.RenameIndex(
                name: "IX_Regioes_MundoFK",
                table: "Regioes",
                newName: "IX_Regioes_MundoFk");

            migrationBuilder.RenameIndex(
                name: "IX_Regioes_ContinenteFK",
                table: "Regioes",
                newName: "IX_Regioes_ContinenteFk");

            migrationBuilder.RenameColumn(
                name: "MundoFK",
                table: "Povos",
                newName: "MundoFk");

            migrationBuilder.RenameIndex(
                name: "IX_Povos_MundoFK",
                table: "Povos",
                newName: "IX_Povos_MundoFk");

            migrationBuilder.RenameColumn(
                name: "RegiaoFK",
                table: "Personagens",
                newName: "RegiaoFk");

            migrationBuilder.RenameColumn(
                name: "MundoFK",
                table: "Personagens",
                newName: "MundoFk");

            migrationBuilder.RenameIndex(
                name: "IX_Personagens_RegiaoFK",
                table: "Personagens",
                newName: "IX_Personagens_RegiaoFk");

            migrationBuilder.RenameIndex(
                name: "IX_Personagens_MundoFK",
                table: "Personagens",
                newName: "IX_Personagens_MundoFk");

            migrationBuilder.RenameColumn(
                name: "MundoFK",
                table: "Criaturas",
                newName: "MundoFk");

            migrationBuilder.RenameIndex(
                name: "IX_Criaturas_MundoFK",
                table: "Criaturas",
                newName: "IX_Criaturas_MundoFk");

            migrationBuilder.RenameColumn(
                name: "MundoFK",
                table: "Contos",
                newName: "MundoFk");

            migrationBuilder.RenameIndex(
                name: "IX_Contos_MundoFK",
                table: "Contos",
                newName: "IX_Contos_MundoFk");

            migrationBuilder.RenameColumn(
                name: "MundoFK",
                table: "Continentes",
                newName: "MundoFk");

            migrationBuilder.RenameIndex(
                name: "IX_Continentes_MundoFK",
                table: "Continentes",
                newName: "IX_Continentes_MundoFk");

            migrationBuilder.AddForeignKey(
                name: "FK_Continentes_Mundos_MundoFk",
                table: "Continentes",
                column: "MundoFk",
                principalTable: "Mundos",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Contos_Mundos_MundoFk",
                table: "Contos",
                column: "MundoFk",
                principalTable: "Mundos",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Criaturas_Mundos_MundoFk",
                table: "Criaturas",
                column: "MundoFk",
                principalTable: "Mundos",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Personagens_Mundos_MundoFk",
                table: "Personagens",
                column: "MundoFk",
                principalTable: "Mundos",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Personagens_Regioes_RegiaoFk",
                table: "Personagens",
                column: "RegiaoFk",
                principalTable: "Regioes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Povos_Mundos_MundoFk",
                table: "Povos",
                column: "MundoFk",
                principalTable: "Mundos",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Regioes_Continentes_ContinenteFk",
                table: "Regioes",
                column: "ContinenteFk",
                principalTable: "Continentes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Regioes_Mundos_MundoFk",
                table: "Regioes",
                column: "MundoFk",
                principalTable: "Mundos",
                principalColumn: "Id");
        }
    }
}
