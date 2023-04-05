using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TdM.Database.Migrations
{
    /// <inheritdoc />
    public partial class Inicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Mundos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Autor = table.Column<int>(type: "int", nullable: false),
                    ImgSrc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Visible = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mundos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Continentes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImgSrc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Visible = table.Column<bool>(type: "bit", nullable: false),
                    MundoFk = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Continentes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Continentes_Mundos_MundoFk",
                        column: x => x.MundoFk,
                        principalTable: "Mundos",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Contos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Titulo = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    Corpo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Autor = table.Column<int>(type: "int", nullable: false),
                    Data = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AudioDrama = table.Column<bool>(type: "bit", nullable: false),
                    ImgSrc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Visible = table.Column<bool>(type: "bit", nullable: false),
                    MundoFk = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contos_Mundos_MundoFk",
                        column: x => x.MundoFk,
                        principalTable: "Mundos",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Povos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    ClassRaca = table.Column<int>(type: "int", nullable: true),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImgSrc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Visible = table.Column<bool>(type: "bit", nullable: false),
                    MundoFk = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Povos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Povos_Mundos_MundoFk",
                        column: x => x.MundoFk,
                        principalTable: "Mundos",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Regioes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    Simbolo = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImgSrc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Visible = table.Column<bool>(type: "bit", nullable: false),
                    ContinenteFk = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    MundoFk = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Regioes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Regioes_Continentes_ContinenteFk",
                        column: x => x.ContinenteFk,
                        principalTable: "Continentes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Regioes_Mundos_MundoFk",
                        column: x => x.MundoFk,
                        principalTable: "Mundos",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ContinenteConto",
                columns: table => new
                {
                    ContinentesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ContosId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContinenteConto", x => new { x.ContinentesId, x.ContosId });
                    table.ForeignKey(
                        name: "FK_ContinenteConto_Continentes_ContinentesId",
                        column: x => x.ContinentesId,
                        principalTable: "Continentes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ContinenteConto_Contos_ContosId",
                        column: x => x.ContosId,
                        principalTable: "Contos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ContinentePovo",
                columns: table => new
                {
                    ContinentesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PovosId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContinentePovo", x => new { x.ContinentesId, x.PovosId });
                    table.ForeignKey(
                        name: "FK_ContinentePovo_Continentes_ContinentesId",
                        column: x => x.ContinentesId,
                        principalTable: "Continentes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ContinentePovo_Povos_PovosId",
                        column: x => x.PovosId,
                        principalTable: "Povos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ContoPovo",
                columns: table => new
                {
                    ContosId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PovosId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContoPovo", x => new { x.ContosId, x.PovosId });
                    table.ForeignKey(
                        name: "FK_ContoPovo_Contos_ContosId",
                        column: x => x.ContosId,
                        principalTable: "Contos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ContoPovo_Povos_PovosId",
                        column: x => x.PovosId,
                        principalTable: "Povos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Criaturas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    Tipo = table.Column<int>(type: "int", nullable: true),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImgSrc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Visible = table.Column<bool>(type: "bit", nullable: false),
                    MundoFk = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PovoId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Criaturas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Criaturas_Mundos_MundoFk",
                        column: x => x.MundoFk,
                        principalTable: "Mundos",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Criaturas_Povos_PovoId",
                        column: x => x.PovoId,
                        principalTable: "Povos",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ContoRegiao",
                columns: table => new
                {
                    ContosId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RegioesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContoRegiao", x => new { x.ContosId, x.RegioesId });
                    table.ForeignKey(
                        name: "FK_ContoRegiao_Contos_ContosId",
                        column: x => x.ContosId,
                        principalTable: "Contos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ContoRegiao_Regioes_RegioesId",
                        column: x => x.RegioesId,
                        principalTable: "Regioes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Personagens",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    Titulo = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    Classe = table.Column<int>(type: "int", nullable: true),
                    Raca = table.Column<int>(type: "int", nullable: true),
                    Biografia = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImgSrc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Visible = table.Column<bool>(type: "bit", nullable: false),
                    MundoFk = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    RegiaoFk = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ContinenteId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Personagens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Personagens_Continentes_ContinenteId",
                        column: x => x.ContinenteId,
                        principalTable: "Continentes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Personagens_Mundos_MundoFk",
                        column: x => x.MundoFk,
                        principalTable: "Mundos",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Personagens_Regioes_RegiaoFk",
                        column: x => x.RegiaoFk,
                        principalTable: "Regioes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PovoRegiao",
                columns: table => new
                {
                    PovosId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RegioesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PovoRegiao", x => new { x.PovosId, x.RegioesId });
                    table.ForeignKey(
                        name: "FK_PovoRegiao_Povos_PovosId",
                        column: x => x.PovosId,
                        principalTable: "Povos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PovoRegiao_Regioes_RegioesId",
                        column: x => x.RegioesId,
                        principalTable: "Regioes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ContinenteCriatura",
                columns: table => new
                {
                    ContinentesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CriaturasId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContinenteCriatura", x => new { x.ContinentesId, x.CriaturasId });
                    table.ForeignKey(
                        name: "FK_ContinenteCriatura_Continentes_ContinentesId",
                        column: x => x.ContinentesId,
                        principalTable: "Continentes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ContinenteCriatura_Criaturas_CriaturasId",
                        column: x => x.CriaturasId,
                        principalTable: "Criaturas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ContoCriatura",
                columns: table => new
                {
                    ContosId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CriaturasId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContoCriatura", x => new { x.ContosId, x.CriaturasId });
                    table.ForeignKey(
                        name: "FK_ContoCriatura_Contos_ContosId",
                        column: x => x.ContosId,
                        principalTable: "Contos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ContoCriatura_Criaturas_CriaturasId",
                        column: x => x.CriaturasId,
                        principalTable: "Criaturas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CriaturaRegiao",
                columns: table => new
                {
                    CriaturasId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RegioesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CriaturaRegiao", x => new { x.CriaturasId, x.RegioesId });
                    table.ForeignKey(
                        name: "FK_CriaturaRegiao_Criaturas_CriaturasId",
                        column: x => x.CriaturasId,
                        principalTable: "Criaturas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CriaturaRegiao_Regioes_RegioesId",
                        column: x => x.RegioesId,
                        principalTable: "Regioes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ContoPersonagem",
                columns: table => new
                {
                    ContosId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PersonagensId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContoPersonagem", x => new { x.ContosId, x.PersonagensId });
                    table.ForeignKey(
                        name: "FK_ContoPersonagem_Contos_ContosId",
                        column: x => x.ContosId,
                        principalTable: "Contos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ContoPersonagem_Personagens_PersonagensId",
                        column: x => x.PersonagensId,
                        principalTable: "Personagens",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PersonagemPovo",
                columns: table => new
                {
                    PersonagensId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PovosId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonagemPovo", x => new { x.PersonagensId, x.PovosId });
                    table.ForeignKey(
                        name: "FK_PersonagemPovo_Personagens_PersonagensId",
                        column: x => x.PersonagensId,
                        principalTable: "Personagens",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PersonagemPovo_Povos_PovosId",
                        column: x => x.PovosId,
                        principalTable: "Povos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ContinenteConto_ContosId",
                table: "ContinenteConto",
                column: "ContosId");

            migrationBuilder.CreateIndex(
                name: "IX_ContinenteCriatura_CriaturasId",
                table: "ContinenteCriatura",
                column: "CriaturasId");

            migrationBuilder.CreateIndex(
                name: "IX_ContinentePovo_PovosId",
                table: "ContinentePovo",
                column: "PovosId");

            migrationBuilder.CreateIndex(
                name: "IX_Continentes_MundoFk",
                table: "Continentes",
                column: "MundoFk");

            migrationBuilder.CreateIndex(
                name: "IX_ContoCriatura_CriaturasId",
                table: "ContoCriatura",
                column: "CriaturasId");

            migrationBuilder.CreateIndex(
                name: "IX_ContoPersonagem_PersonagensId",
                table: "ContoPersonagem",
                column: "PersonagensId");

            migrationBuilder.CreateIndex(
                name: "IX_ContoPovo_PovosId",
                table: "ContoPovo",
                column: "PovosId");

            migrationBuilder.CreateIndex(
                name: "IX_ContoRegiao_RegioesId",
                table: "ContoRegiao",
                column: "RegioesId");

            migrationBuilder.CreateIndex(
                name: "IX_Contos_MundoFk",
                table: "Contos",
                column: "MundoFk");

            migrationBuilder.CreateIndex(
                name: "IX_CriaturaRegiao_RegioesId",
                table: "CriaturaRegiao",
                column: "RegioesId");

            migrationBuilder.CreateIndex(
                name: "IX_Criaturas_MundoFk",
                table: "Criaturas",
                column: "MundoFk");

            migrationBuilder.CreateIndex(
                name: "IX_Criaturas_PovoId",
                table: "Criaturas",
                column: "PovoId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonagemPovo_PovosId",
                table: "PersonagemPovo",
                column: "PovosId");

            migrationBuilder.CreateIndex(
                name: "IX_Personagens_ContinenteId",
                table: "Personagens",
                column: "ContinenteId");

            migrationBuilder.CreateIndex(
                name: "IX_Personagens_MundoFk",
                table: "Personagens",
                column: "MundoFk");

            migrationBuilder.CreateIndex(
                name: "IX_Personagens_RegiaoFk",
                table: "Personagens",
                column: "RegiaoFk");

            migrationBuilder.CreateIndex(
                name: "IX_PovoRegiao_RegioesId",
                table: "PovoRegiao",
                column: "RegioesId");

            migrationBuilder.CreateIndex(
                name: "IX_Povos_MundoFk",
                table: "Povos",
                column: "MundoFk");

            migrationBuilder.CreateIndex(
                name: "IX_Regioes_ContinenteFk",
                table: "Regioes",
                column: "ContinenteFk");

            migrationBuilder.CreateIndex(
                name: "IX_Regioes_MundoFk",
                table: "Regioes",
                column: "MundoFk");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContinenteConto");

            migrationBuilder.DropTable(
                name: "ContinenteCriatura");

            migrationBuilder.DropTable(
                name: "ContinentePovo");

            migrationBuilder.DropTable(
                name: "ContoCriatura");

            migrationBuilder.DropTable(
                name: "ContoPersonagem");

            migrationBuilder.DropTable(
                name: "ContoPovo");

            migrationBuilder.DropTable(
                name: "ContoRegiao");

            migrationBuilder.DropTable(
                name: "CriaturaRegiao");

            migrationBuilder.DropTable(
                name: "PersonagemPovo");

            migrationBuilder.DropTable(
                name: "PovoRegiao");

            migrationBuilder.DropTable(
                name: "Contos");

            migrationBuilder.DropTable(
                name: "Criaturas");

            migrationBuilder.DropTable(
                name: "Personagens");

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
