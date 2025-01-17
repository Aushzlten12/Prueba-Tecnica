﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TiendaProductos.Api.Data;

#nullable disable

namespace TiendaProductos.Api.Data.Migrations
{
    [DbContext(typeof(TiendaProductosContext))]
    [Migration("20240716024519_SeedMoreProducts")]
    partial class SeedMoreProducts
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("TiendaProductos.Api.Entities.Producto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(65,30)");

                    b.HasKey("Id");

                    b.ToTable("Productos");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Televisor",
                            Price = 1599.99m
                        },
                        new
                        {
                            Id = 2,
                            Name = "Laptop",
                            Price = 2700.00m
                        },
                        new
                        {
                            Id = 3,
                            Name = "Tablet",
                            Price = 800.00m
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
