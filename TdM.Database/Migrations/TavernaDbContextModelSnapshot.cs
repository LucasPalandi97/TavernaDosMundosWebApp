﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TdM.Database.Data;

#nullable disable

namespace TdM.Database.Migrations
{
    [DbContext(typeof(TavernaDbContext))]
    partial class TavernaDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ContinenteConto", b =>
                {
                    b.Property<Guid>("ContinentesId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ContosId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ContinentesId", "ContosId");

                    b.HasIndex("ContosId");

                    b.ToTable("ContinenteConto");
                });

            modelBuilder.Entity("ContinenteCriatura", b =>
                {
                    b.Property<Guid>("ContinentesId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CriaturasId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ContinentesId", "CriaturasId");

                    b.HasIndex("CriaturasId");

                    b.ToTable("ContinenteCriatura");
                });

            modelBuilder.Entity("ContinentePovo", b =>
                {
                    b.Property<Guid>("ContinentesId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("PovosId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ContinentesId", "PovosId");

                    b.HasIndex("PovosId");

                    b.ToTable("ContinentePovo");
                });

            modelBuilder.Entity("ContoCriatura", b =>
                {
                    b.Property<Guid>("ContosId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CriaturasId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ContosId", "CriaturasId");

                    b.HasIndex("CriaturasId");

                    b.ToTable("ContoCriatura");
                });

            modelBuilder.Entity("ContoMundo", b =>
                {
                    b.Property<Guid>("ContosId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("MundoId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ContosId", "MundoId");

                    b.HasIndex("MundoId");

                    b.ToTable("ContoMundo");
                });

            modelBuilder.Entity("ContoPersonagem", b =>
                {
                    b.Property<Guid>("ContosId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("PersonagensId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ContosId", "PersonagensId");

                    b.HasIndex("PersonagensId");

                    b.ToTable("ContoPersonagem");
                });

            modelBuilder.Entity("ContoPovo", b =>
                {
                    b.Property<Guid>("ContosId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("PovosId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ContosId", "PovosId");

                    b.HasIndex("PovosId");

                    b.ToTable("ContoPovo");
                });

            modelBuilder.Entity("ContoRegiao", b =>
                {
                    b.Property<Guid>("ContosId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("RegioesId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ContosId", "RegioesId");

                    b.HasIndex("RegioesId");

                    b.ToTable("ContoRegiao");
                });

            modelBuilder.Entity("CriaturaRegiao", b =>
                {
                    b.Property<Guid>("CriaturasId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("RegioesId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("CriaturasId", "RegioesId");

                    b.HasIndex("RegioesId");

                    b.ToTable("CriaturaRegiao");
                });

            modelBuilder.Entity("PovoRegiao", b =>
                {
                    b.Property<Guid>("PovosId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("RegioesId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("PovosId", "RegioesId");

                    b.HasIndex("RegioesId");

                    b.ToTable("PovoRegiao");
                });

            modelBuilder.Entity("TdM.Database.Models.Domain.Continente", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImgSrc")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("MundoId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("nvarchar(32)");

                    b.Property<bool>("Visible")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("MundoId");

                    b.ToTable("Continentes");
                });

            modelBuilder.Entity("TdM.Database.Models.Domain.Conto", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("AudioDrama")
                        .HasColumnType("bit");

                    b.Property<int>("Autor")
                        .HasColumnType("int");

                    b.Property<string>("Corpo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Data")
                        .HasColumnType("datetime2");

                    b.Property<string>("ImgSrc")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Titulo")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("nvarchar(32)");

                    b.Property<bool>("Visible")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("Contos");
                });

            modelBuilder.Entity("TdM.Database.Models.Domain.Criatura", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImgSrc")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("MundoId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("nvarchar(32)");

                    b.Property<int?>("Tipo")
                        .HasColumnType("int");

                    b.Property<bool>("Visible")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("MundoId");

                    b.ToTable("Criaturas");
                });

            modelBuilder.Entity("TdM.Database.Models.Domain.Mundo", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Autor")
                        .HasColumnType("int");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImgSrc")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("nvarchar(32)");

                    b.Property<bool>("Visible")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("Mundos");
                });

            modelBuilder.Entity("TdM.Database.Models.Domain.Personagem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Biografia")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Classe")
                        .HasColumnType("int");

                    b.Property<Guid?>("ContinenteId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ImgSrc")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("MundoId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("nvarchar(32)");

                    b.Property<Guid?>("PovoId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int?>("Raca")
                        .HasColumnType("int");

                    b.Property<Guid?>("RegiaoId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Titulo")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("nvarchar(32)");

                    b.Property<bool>("Visible")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("ContinenteId");

                    b.HasIndex("MundoId");

                    b.HasIndex("PovoId");

                    b.HasIndex("RegiaoId");

                    b.ToTable("Personagens");
                });

            modelBuilder.Entity("TdM.Database.Models.Domain.Povo", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int?>("ClassRaca")
                        .HasColumnType("int");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImgSrc")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("MundoId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("nvarchar(32)");

                    b.Property<bool>("Visible")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("MundoId");

                    b.ToTable("Povos");
                });

            modelBuilder.Entity("TdM.Database.Models.Domain.Regiao", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("ContinenteId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImgSrc")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("MundoId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("nvarchar(32)");

                    b.Property<string>("Simbolo")
                        .IsRequired()
                        .HasMaxLength(3)
                        .HasColumnType("nvarchar(3)");

                    b.Property<bool>("Visible")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("ContinenteId");

                    b.HasIndex("MundoId");

                    b.ToTable("Regioes");
                });

            modelBuilder.Entity("ContinenteConto", b =>
                {
                    b.HasOne("TdM.Database.Models.Domain.Continente", null)
                        .WithMany()
                        .HasForeignKey("ContinentesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TdM.Database.Models.Domain.Conto", null)
                        .WithMany()
                        .HasForeignKey("ContosId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ContinenteCriatura", b =>
                {
                    b.HasOne("TdM.Database.Models.Domain.Continente", null)
                        .WithMany()
                        .HasForeignKey("ContinentesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TdM.Database.Models.Domain.Criatura", null)
                        .WithMany()
                        .HasForeignKey("CriaturasId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ContinentePovo", b =>
                {
                    b.HasOne("TdM.Database.Models.Domain.Continente", null)
                        .WithMany()
                        .HasForeignKey("ContinentesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TdM.Database.Models.Domain.Povo", null)
                        .WithMany()
                        .HasForeignKey("PovosId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ContoCriatura", b =>
                {
                    b.HasOne("TdM.Database.Models.Domain.Conto", null)
                        .WithMany()
                        .HasForeignKey("ContosId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TdM.Database.Models.Domain.Criatura", null)
                        .WithMany()
                        .HasForeignKey("CriaturasId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ContoMundo", b =>
                {
                    b.HasOne("TdM.Database.Models.Domain.Mundo", null)
                        .WithMany()
                        .HasForeignKey("ContosId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TdM.Database.Models.Domain.Conto", null)
                        .WithMany()
                        .HasForeignKey("MundoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ContoPersonagem", b =>
                {
                    b.HasOne("TdM.Database.Models.Domain.Conto", null)
                        .WithMany()
                        .HasForeignKey("ContosId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TdM.Database.Models.Domain.Personagem", null)
                        .WithMany()
                        .HasForeignKey("PersonagensId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ContoPovo", b =>
                {
                    b.HasOne("TdM.Database.Models.Domain.Conto", null)
                        .WithMany()
                        .HasForeignKey("ContosId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TdM.Database.Models.Domain.Povo", null)
                        .WithMany()
                        .HasForeignKey("PovosId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ContoRegiao", b =>
                {
                    b.HasOne("TdM.Database.Models.Domain.Conto", null)
                        .WithMany()
                        .HasForeignKey("ContosId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TdM.Database.Models.Domain.Regiao", null)
                        .WithMany()
                        .HasForeignKey("RegioesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CriaturaRegiao", b =>
                {
                    b.HasOne("TdM.Database.Models.Domain.Criatura", null)
                        .WithMany()
                        .HasForeignKey("CriaturasId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TdM.Database.Models.Domain.Regiao", null)
                        .WithMany()
                        .HasForeignKey("RegioesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("PovoRegiao", b =>
                {
                    b.HasOne("TdM.Database.Models.Domain.Povo", null)
                        .WithMany()
                        .HasForeignKey("PovosId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TdM.Database.Models.Domain.Regiao", null)
                        .WithMany()
                        .HasForeignKey("RegioesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TdM.Database.Models.Domain.Continente", b =>
                {
                    b.HasOne("TdM.Database.Models.Domain.Mundo", "Mundo")
                        .WithMany("Continentes")
                        .HasForeignKey("MundoId");

                    b.Navigation("Mundo");
                });

            modelBuilder.Entity("TdM.Database.Models.Domain.Criatura", b =>
                {
                    b.HasOne("TdM.Database.Models.Domain.Mundo", "Mundo")
                        .WithMany("Criaturas")
                        .HasForeignKey("MundoId");

                    b.Navigation("Mundo");
                });

            modelBuilder.Entity("TdM.Database.Models.Domain.Personagem", b =>
                {
                    b.HasOne("TdM.Database.Models.Domain.Continente", null)
                        .WithMany("Personagens")
                        .HasForeignKey("ContinenteId");

                    b.HasOne("TdM.Database.Models.Domain.Mundo", "Mundo")
                        .WithMany("Personagens")
                        .HasForeignKey("MundoId");

                    b.HasOne("TdM.Database.Models.Domain.Povo", "Povo")
                        .WithMany("Personagens")
                        .HasForeignKey("PovoId");

                    b.HasOne("TdM.Database.Models.Domain.Regiao", "Regiao")
                        .WithMany("Personagens")
                        .HasForeignKey("RegiaoId");

                    b.Navigation("Mundo");

                    b.Navigation("Povo");

                    b.Navigation("Regiao");
                });

            modelBuilder.Entity("TdM.Database.Models.Domain.Povo", b =>
                {
                    b.HasOne("TdM.Database.Models.Domain.Mundo", "Mundo")
                        .WithMany("Povos")
                        .HasForeignKey("MundoId");

                    b.Navigation("Mundo");
                });

            modelBuilder.Entity("TdM.Database.Models.Domain.Regiao", b =>
                {
                    b.HasOne("TdM.Database.Models.Domain.Continente", "Continente")
                        .WithMany("Regioes")
                        .HasForeignKey("ContinenteId");

                    b.HasOne("TdM.Database.Models.Domain.Mundo", "Mundo")
                        .WithMany("Regioes")
                        .HasForeignKey("MundoId");

                    b.Navigation("Continente");

                    b.Navigation("Mundo");
                });

            modelBuilder.Entity("TdM.Database.Models.Domain.Continente", b =>
                {
                    b.Navigation("Personagens");

                    b.Navigation("Regioes");
                });

            modelBuilder.Entity("TdM.Database.Models.Domain.Mundo", b =>
                {
                    b.Navigation("Continentes");

                    b.Navigation("Criaturas");

                    b.Navigation("Personagens");

                    b.Navigation("Povos");

                    b.Navigation("Regioes");
                });

            modelBuilder.Entity("TdM.Database.Models.Domain.Povo", b =>
                {
                    b.Navigation("Personagens");
                });

            modelBuilder.Entity("TdM.Database.Models.Domain.Regiao", b =>
                {
                    b.Navigation("Personagens");
                });
#pragma warning restore 612, 618
        }
    }
}
