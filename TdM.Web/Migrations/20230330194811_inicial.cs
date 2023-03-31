using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TdM.Web.Migrations
{
    /// <inheritdoc />
    public partial class inicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Mundos",
                columns: table => new
                {
                    MundoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Autor = table.Column<int>(type: "int", nullable: false),
                    Visible = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mundos", x => x.MundoId);
                });

            migrationBuilder.CreateTable(
                name: "Continentes",
                columns: table => new
                {
                    ContinenteId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MundoId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Continentes", x => x.ContinenteId);
                    table.ForeignKey(
                        name: "FK_Continentes_Mundos_MundoId",
                        column: x => x.MundoId,
                        principalTable: "Mundos",
                        principalColumn: "MundoId");
                });

            migrationBuilder.CreateTable(
                name: "Criaturas",
                columns: table => new
                {
                    CriaturaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    ClassTipo = table.Column<int>(type: "int", nullable: true),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MundoId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Criaturas", x => x.CriaturaId);
                    table.ForeignKey(
                        name: "FK_Criaturas_Mundos_MundoId",
                        column: x => x.MundoId,
                        principalTable: "Mundos",
                        principalColumn: "MundoId");
                });

            migrationBuilder.CreateTable(
                name: "Povos",
                columns: table => new
                {
                    PovoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    ClassRaca = table.Column<int>(type: "int", nullable: true),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MundoId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Povos", x => x.PovoId);
                    table.ForeignKey(
                        name: "FK_Povos_Mundos_MundoId",
                        column: x => x.MundoId,
                        principalTable: "Mundos",
                        principalColumn: "MundoId");
                });

            migrationBuilder.CreateTable(
                name: "Regioes",
                columns: table => new
                {
                    RegiaoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    Simbolo = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContinenteId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    MundoId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Regioes", x => x.RegiaoId);
                    table.ForeignKey(
                        name: "FK_Regioes_Continentes_ContinenteId",
                        column: x => x.ContinenteId,
                        principalTable: "Continentes",
                        principalColumn: "ContinenteId");
                    table.ForeignKey(
                        name: "FK_Regioes_Mundos_MundoId",
                        column: x => x.MundoId,
                        principalTable: "Mundos",
                        principalColumn: "MundoId");
                });

            migrationBuilder.CreateTable(
                name: "ContinenteCriatura",
                columns: table => new
                {
                    ContinentesContinenteId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CriaturasCriaturaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContinenteCriatura", x => new { x.ContinentesContinenteId, x.CriaturasCriaturaId });
                    table.ForeignKey(
                        name: "FK_ContinenteCriatura_Continentes_ContinentesContinenteId",
                        column: x => x.ContinentesContinenteId,
                        principalTable: "Continentes",
                        principalColumn: "ContinenteId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ContinenteCriatura_Criaturas_CriaturasCriaturaId",
                        column: x => x.CriaturasCriaturaId,
                        principalTable: "Criaturas",
                        principalColumn: "CriaturaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ContinentePovo",
                columns: table => new
                {
                    ContinentesContinenteId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PovosPovoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContinentePovo", x => new { x.ContinentesContinenteId, x.PovosPovoId });
                    table.ForeignKey(
                        name: "FK_ContinentePovo_Continentes_ContinentesContinenteId",
                        column: x => x.ContinentesContinenteId,
                        principalTable: "Continentes",
                        principalColumn: "ContinenteId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ContinentePovo_Povos_PovosPovoId",
                        column: x => x.PovosPovoId,
                        principalTable: "Povos",
                        principalColumn: "PovoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CriaturaRegiao",
                columns: table => new
                {
                    CriaturasCriaturaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RegioesRegiaoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CriaturaRegiao", x => new { x.CriaturasCriaturaId, x.RegioesRegiaoId });
                    table.ForeignKey(
                        name: "FK_CriaturaRegiao_Criaturas_CriaturasCriaturaId",
                        column: x => x.CriaturasCriaturaId,
                        principalTable: "Criaturas",
                        principalColumn: "CriaturaId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CriaturaRegiao_Regioes_RegioesRegiaoId",
                        column: x => x.RegioesRegiaoId,
                        principalTable: "Regioes",
                        principalColumn: "RegiaoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Personagens",
                columns: table => new
                {
                    PersonagemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    Titulo = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    ClassClasse = table.Column<int>(type: "int", nullable: true),
                    ClassRaca = table.Column<int>(type: "int", nullable: true),
                    Biografia = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MundoId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    RegiaoId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PovoId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ContinenteId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Personagens", x => x.PersonagemId);
                    table.ForeignKey(
                        name: "FK_Personagens_Continentes_ContinenteId",
                        column: x => x.ContinenteId,
                        principalTable: "Continentes",
                        principalColumn: "ContinenteId");
                    table.ForeignKey(
                        name: "FK_Personagens_Mundos_MundoId",
                        column: x => x.MundoId,
                        principalTable: "Mundos",
                        principalColumn: "MundoId");
                    table.ForeignKey(
                        name: "FK_Personagens_Povos_PovoId",
                        column: x => x.PovoId,
                        principalTable: "Povos",
                        principalColumn: "PovoId");
                    table.ForeignKey(
                        name: "FK_Personagens_Regioes_RegiaoId",
                        column: x => x.RegiaoId,
                        principalTable: "Regioes",
                        principalColumn: "RegiaoId");
                });

            migrationBuilder.CreateTable(
                name: "PovoRegiao",
                columns: table => new
                {
                    PovosPovoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RegioesRegiaoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PovoRegiao", x => new { x.PovosPovoId, x.RegioesRegiaoId });
                    table.ForeignKey(
                        name: "FK_PovoRegiao_Povos_PovosPovoId",
                        column: x => x.PovosPovoId,
                        principalTable: "Povos",
                        principalColumn: "PovoId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PovoRegiao_Regioes_RegioesRegiaoId",
                        column: x => x.RegioesRegiaoId,
                        principalTable: "Regioes",
                        principalColumn: "RegiaoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Contos",
                columns: table => new
                {
                    ContoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Titulo = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    Corpo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Autor = table.Column<int>(type: "int", nullable: false),
                    Data = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AudioDrama = table.Column<int>(type: "int", nullable: false),
                    MundoId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ContinenteId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    RegiaoId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PovoId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CriaturaId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PersonagemId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contos", x => x.ContoId);
                    table.ForeignKey(
                        name: "FK_Contos_Continentes_ContinenteId",
                        column: x => x.ContinenteId,
                        principalTable: "Continentes",
                        principalColumn: "ContinenteId");
                    table.ForeignKey(
                        name: "FK_Contos_Criaturas_CriaturaId",
                        column: x => x.CriaturaId,
                        principalTable: "Criaturas",
                        principalColumn: "CriaturaId");
                    table.ForeignKey(
                        name: "FK_Contos_Mundos_MundoId",
                        column: x => x.MundoId,
                        principalTable: "Mundos",
                        principalColumn: "MundoId");
                    table.ForeignKey(
                        name: "FK_Contos_Personagens_PersonagemId",
                        column: x => x.PersonagemId,
                        principalTable: "Personagens",
                        principalColumn: "PersonagemId");
                    table.ForeignKey(
                        name: "FK_Contos_Povos_PovoId",
                        column: x => x.PovoId,
                        principalTable: "Povos",
                        principalColumn: "PovoId");
                    table.ForeignKey(
                        name: "FK_Contos_Regioes_RegiaoId",
                        column: x => x.RegiaoId,
                        principalTable: "Regioes",
                        principalColumn: "RegiaoId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ContinenteCriatura_CriaturasCriaturaId",
                table: "ContinenteCriatura",
                column: "CriaturasCriaturaId");

            migrationBuilder.CreateIndex(
                name: "IX_ContinentePovo_PovosPovoId",
                table: "ContinentePovo",
                column: "PovosPovoId");

            migrationBuilder.CreateIndex(
                name: "IX_Continentes_MundoId",
                table: "Continentes",
                column: "MundoId");

            migrationBuilder.CreateIndex(
                name: "IX_Contos_ContinenteId",
                table: "Contos",
                column: "ContinenteId");

            migrationBuilder.CreateIndex(
                name: "IX_Contos_CriaturaId",
                table: "Contos",
                column: "CriaturaId");

            migrationBuilder.CreateIndex(
                name: "IX_Contos_MundoId",
                table: "Contos",
                column: "MundoId");

            migrationBuilder.CreateIndex(
                name: "IX_Contos_PersonagemId",
                table: "Contos",
                column: "PersonagemId");

            migrationBuilder.CreateIndex(
                name: "IX_Contos_PovoId",
                table: "Contos",
                column: "PovoId");

            migrationBuilder.CreateIndex(
                name: "IX_Contos_RegiaoId",
                table: "Contos",
                column: "RegiaoId");

            migrationBuilder.CreateIndex(
                name: "IX_CriaturaRegiao_RegioesRegiaoId",
                table: "CriaturaRegiao",
                column: "RegioesRegiaoId");

            migrationBuilder.CreateIndex(
                name: "IX_Criaturas_MundoId",
                table: "Criaturas",
                column: "MundoId");

            migrationBuilder.CreateIndex(
                name: "IX_Personagens_ContinenteId",
                table: "Personagens",
                column: "ContinenteId");

            migrationBuilder.CreateIndex(
                name: "IX_Personagens_MundoId",
                table: "Personagens",
                column: "MundoId");

            migrationBuilder.CreateIndex(
                name: "IX_Personagens_PovoId",
                table: "Personagens",
                column: "PovoId");

            migrationBuilder.CreateIndex(
                name: "IX_Personagens_RegiaoId",
                table: "Personagens",
                column: "RegiaoId");

            migrationBuilder.CreateIndex(
                name: "IX_PovoRegiao_RegioesRegiaoId",
                table: "PovoRegiao",
                column: "RegioesRegiaoId");

            migrationBuilder.CreateIndex(
                name: "IX_Povos_MundoId",
                table: "Povos",
                column: "MundoId");

            migrationBuilder.CreateIndex(
                name: "IX_Regioes_ContinenteId",
                table: "Regioes",
                column: "ContinenteId");

            migrationBuilder.CreateIndex(
                name: "IX_Regioes_MundoId",
                table: "Regioes",
                column: "MundoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContinenteCriatura");

            migrationBuilder.DropTable(
                name: "ContinentePovo");

            migrationBuilder.DropTable(
                name: "Contos");

            migrationBuilder.DropTable(
                name: "CriaturaRegiao");

            migrationBuilder.DropTable(
                name: "PovoRegiao");

            migrationBuilder.DropTable(
                name: "Personagens");

            migrationBuilder.DropTable(
                name: "Criaturas");

            migrationBuilder.DropTable(
                name: "Povos");

            migrationBuilder.DropTable(
                name: "Regioes");

            migrationBuilder.DropTable(
                name: "Continentes");

            migrationBuilder.DropTable(
                name: "Mundos");
        }
    }
}
