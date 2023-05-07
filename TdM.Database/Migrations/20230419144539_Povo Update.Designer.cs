﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TdM.Database.Data;

#nullable disable

namespace TdM.Database.Migrations
{
    [DbContext(typeof(TavernaDbContext))]
    [Migration("20230419144539_Povo Update")]
    partial class PovoUpdate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
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

            modelBuilder.Entity("CriaturaPovo", b =>
                {
                    b.Property<Guid>("CriaturasId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("PovosId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("CriaturasId", "PovosId");

                    b.HasIndex("PovosId");

                    b.ToTable("CriaturaPovo");
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

            modelBuilder.Entity("PersonagemPovo", b =>
                {
                    b.Property<Guid>("PersonagensId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("PovosId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("PersonagensId", "PovosId");

                    b.HasIndex("PovosId");

                    b.ToTable("PersonagemPovo");
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

                    b.Property<string>("CurtaDescricao")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImgBox")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImgCard")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("MundoFK")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("nvarchar(32)");

                    b.Property<DateTime>("PublishedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("UrlHandle")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Visible")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("MundoFK");

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

                    b.Property<string>("ImgBox")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImgCard")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("MundoFK")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("PublishedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Titulo")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("nvarchar(32)");

                    b.Property<string>("UrlHandle")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Visible")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("MundoFK");

                    b.ToTable("Contos");
                });

            modelBuilder.Entity("TdM.Database.Models.Domain.Criatura", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CurtaDescricao")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImgBox")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImgCard")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("MundoFK")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("nvarchar(32)");

                    b.Property<DateTime>("PublishedDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("Tipo")
                        .HasColumnType("int");

                    b.Property<string>("UrlHandle")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Visible")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("MundoFK");

                    b.ToTable("Criaturas");
                });

            modelBuilder.Entity("TdM.Database.Models.Domain.Mundo", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Autor")
                        .HasColumnType("int");

                    b.Property<string>("CurtaDescricao")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImgBox")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("nvarchar(32)");

                    b.Property<DateTime>("PublishedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("UrlHandle")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

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

                    b.Property<Guid?>("ContinenteFK")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CurtaDescricao")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImgBox")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImgCard")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("MundoFK")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("nvarchar(32)");

                    b.Property<DateTime>("PublishedDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("Raca")
                        .HasColumnType("int");

                    b.Property<Guid?>("RegiaoFK")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Titulo")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("nvarchar(32)");

                    b.Property<string>("UrlHandle")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Visible")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("ContinenteFK");

                    b.HasIndex("MundoFK");

                    b.HasIndex("RegiaoFK");

                    b.ToTable("Personagens");
                });

            modelBuilder.Entity("TdM.Database.Models.Domain.Povo", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CurtaDescricao")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImgBox")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImgCard")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("MundoFK")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("nvarchar(32)");

                    b.Property<DateTime>("PublishedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("UrlHandle")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Visible")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("MundoFK");

                    b.ToTable("Povos");
                });

            modelBuilder.Entity("TdM.Database.Models.Domain.Regiao", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("ContinenteFK")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CurtaDescricao")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImgBox")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImgCard")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("MundoFK")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("nvarchar(32)");

                    b.Property<DateTime>("PublishedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Simbolo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UrlHandle")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Visible")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("ContinenteFK");

                    b.HasIndex("MundoFK");

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

            modelBuilder.Entity("CriaturaPovo", b =>
                {
                    b.HasOne("TdM.Database.Models.Domain.Criatura", null)
                        .WithMany()
                        .HasForeignKey("CriaturasId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TdM.Database.Models.Domain.Povo", null)
                        .WithMany()
                        .HasForeignKey("PovosId")
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

            modelBuilder.Entity("PersonagemPovo", b =>
                {
                    b.HasOne("TdM.Database.Models.Domain.Personagem", null)
                        .WithMany()
                        .HasForeignKey("PersonagensId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TdM.Database.Models.Domain.Povo", null)
                        .WithMany()
                        .HasForeignKey("PovosId")
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
                        .HasForeignKey("MundoFK")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("Mundo");
                });

            modelBuilder.Entity("TdM.Database.Models.Domain.Conto", b =>
                {
                    b.HasOne("TdM.Database.Models.Domain.Mundo", "Mundo")
                        .WithMany("Contos")
                        .HasForeignKey("MundoFK")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("Mundo");
                });

            modelBuilder.Entity("TdM.Database.Models.Domain.Criatura", b =>
                {
                    b.HasOne("TdM.Database.Models.Domain.Mundo", "Mundo")
                        .WithMany("Criaturas")
                        .HasForeignKey("MundoFK")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("Mundo");
                });

            modelBuilder.Entity("TdM.Database.Models.Domain.Personagem", b =>
                {
                    b.HasOne("TdM.Database.Models.Domain.Continente", "Continente")
                        .WithMany("Personagens")
                        .HasForeignKey("ContinenteFK")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("TdM.Database.Models.Domain.Mundo", "Mundo")
                        .WithMany("Personagens")
                        .HasForeignKey("MundoFK")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("TdM.Database.Models.Domain.Regiao", "Regiao")
                        .WithMany("Personagens")
                        .HasForeignKey("RegiaoFK")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("Continente");

                    b.Navigation("Mundo");

                    b.Navigation("Regiao");
                });

            modelBuilder.Entity("TdM.Database.Models.Domain.Povo", b =>
                {
                    b.HasOne("TdM.Database.Models.Domain.Mundo", "Mundo")
                        .WithMany("Povos")
                        .HasForeignKey("MundoFK")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("Mundo");
                });

            modelBuilder.Entity("TdM.Database.Models.Domain.Regiao", b =>
                {
                    b.HasOne("TdM.Database.Models.Domain.Continente", "Continente")
                        .WithMany("Regioes")
                        .HasForeignKey("ContinenteFK")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("TdM.Database.Models.Domain.Mundo", "Mundo")
                        .WithMany("Regioes")
                        .HasForeignKey("MundoFK")
                        .OnDelete(DeleteBehavior.SetNull);

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

                    b.Navigation("Contos");

                    b.Navigation("Criaturas");

                    b.Navigation("Personagens");

                    b.Navigation("Povos");

                    b.Navigation("Regioes");
                });

            modelBuilder.Entity("TdM.Database.Models.Domain.Regiao", b =>
                {
                    b.Navigation("Personagens");
                });
        }
    }
}
