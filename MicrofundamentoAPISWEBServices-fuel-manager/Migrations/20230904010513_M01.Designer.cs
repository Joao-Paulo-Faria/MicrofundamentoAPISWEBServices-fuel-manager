﻿// <auto-generated />
using System;
using MicrofundamentoAPISWEBServices_fuel_manager.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace MicrofundamentoAPISWEBServices_fuel_manager.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20230904010513_M01")]
    partial class M01
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("MicrofundamentoAPISWEBServices_fuel_manager.Models.Consumo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Data")
                        .HasColumnType("datetime2");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Tipo")
                        .HasColumnType("int");

                    b.Property<decimal>("Valor")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("VeiculoId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("VeiculoId");

                    b.ToTable("Consumos");
                });

            modelBuilder.Entity("MicrofundamentoAPISWEBServices_fuel_manager.Models.LinkDto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("ConsumoId")
                        .HasColumnType("int");

                    b.Property<string>("Href")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Metodo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Rel")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("VeiculoId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ConsumoId");

                    b.HasIndex("VeiculoId");

                    b.ToTable("LinkDto");
                });

            modelBuilder.Entity("MicrofundamentoAPISWEBServices_fuel_manager.Models.Usuario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Perfil")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Usuarios");
                });

            modelBuilder.Entity("MicrofundamentoAPISWEBServices_fuel_manager.Models.Veiculo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AnoFabricacao")
                        .HasColumnType("int");

                    b.Property<int>("AnoModelo")
                        .HasColumnType("int");

                    b.Property<string>("Marca")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Modelo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Placa")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Veiculos");
                });

            modelBuilder.Entity("MicrofundamentoAPISWEBServices_fuel_manager.Models.VeiculoUsuarios", b =>
                {
                    b.Property<int>("VeiculoId")
                        .HasColumnType("int");

                    b.Property<int>("UsuarioId")
                        .HasColumnType("int");

                    b.HasKey("VeiculoId", "UsuarioId");

                    b.HasIndex("UsuarioId");

                    b.ToTable("VeiculoUsuarios");
                });

            modelBuilder.Entity("MicrofundamentoAPISWEBServices_fuel_manager.Models.Consumo", b =>
                {
                    b.HasOne("MicrofundamentoAPISWEBServices_fuel_manager.Models.Veiculo", "Veiculo")
                        .WithMany("Consumos")
                        .HasForeignKey("VeiculoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Veiculo");
                });

            modelBuilder.Entity("MicrofundamentoAPISWEBServices_fuel_manager.Models.LinkDto", b =>
                {
                    b.HasOne("MicrofundamentoAPISWEBServices_fuel_manager.Models.Consumo", null)
                        .WithMany("Links")
                        .HasForeignKey("ConsumoId");

                    b.HasOne("MicrofundamentoAPISWEBServices_fuel_manager.Models.Veiculo", null)
                        .WithMany("Links")
                        .HasForeignKey("VeiculoId");
                });

            modelBuilder.Entity("MicrofundamentoAPISWEBServices_fuel_manager.Models.VeiculoUsuarios", b =>
                {
                    b.HasOne("MicrofundamentoAPISWEBServices_fuel_manager.Models.Usuario", "Usuario")
                        .WithMany("Veiculos")
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MicrofundamentoAPISWEBServices_fuel_manager.Models.Veiculo", "Veiculo")
                        .WithMany("Usuarios")
                        .HasForeignKey("VeiculoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Usuario");

                    b.Navigation("Veiculo");
                });

            modelBuilder.Entity("MicrofundamentoAPISWEBServices_fuel_manager.Models.Consumo", b =>
                {
                    b.Navigation("Links");
                });

            modelBuilder.Entity("MicrofundamentoAPISWEBServices_fuel_manager.Models.Usuario", b =>
                {
                    b.Navigation("Veiculos");
                });

            modelBuilder.Entity("MicrofundamentoAPISWEBServices_fuel_manager.Models.Veiculo", b =>
                {
                    b.Navigation("Consumos");

                    b.Navigation("Links");

                    b.Navigation("Usuarios");
                });
#pragma warning restore 612, 618
        }
    }
}
