﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ParkDataLayer.Model;

#nullable disable

namespace ParkDataLayer.Migrations
{
    [DbContext(typeof(DBContext))]
    partial class DBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.25")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("ParkDataLayer.Model.HuisEF", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<bool>("Actief")
                        .HasColumnType("bit");

                    b.Property<int>("Nr")
                        .HasColumnType("int");

                    b.Property<string>("ParkId")
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("Straat")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.HasKey("Id");

                    b.HasIndex("ParkId");

                    b.ToTable("Huizen");
                });

            modelBuilder.Entity("ParkDataLayer.Model.HuurcontractEF", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)");

                    b.Property<int>("Aantaldagen")
                        .HasColumnType("int");

                    b.Property<DateTime>("EindDatum")
                        .HasColumnType("datetime2");

                    b.Property<int?>("HuisId")
                        .HasColumnType("int");

                    b.Property<int?>("HuurderId")
                        .HasColumnType("int");

                    b.Property<DateTime>("StartDatum")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("HuisId");

                    b.HasIndex("HuurderId");

                    b.ToTable("Huurcontracten");
                });

            modelBuilder.Entity("ParkDataLayer.Model.HuurderEF", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Adres")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Email")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Naam")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Tel")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Huurders");
                });

            modelBuilder.Entity("ParkDataLayer.Model.ParkEF", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("Locatie")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("Naam")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.HasKey("Id");

                    b.ToTable("Parken");
                });

            modelBuilder.Entity("ParkDataLayer.Model.HuisEF", b =>
                {
                    b.HasOne("ParkDataLayer.Model.ParkEF", "Park")
                        .WithMany("_huis")
                        .HasForeignKey("ParkId");

                    b.Navigation("Park");
                });

            modelBuilder.Entity("ParkDataLayer.Model.HuurcontractEF", b =>
                {
                    b.HasOne("ParkDataLayer.Model.HuisEF", "Huis")
                        .WithMany("Huurcontracten")
                        .HasForeignKey("HuisId");

                    b.HasOne("ParkDataLayer.Model.HuurderEF", "Huurder")
                        .WithMany()
                        .HasForeignKey("HuurderId");

                    b.Navigation("Huis");

                    b.Navigation("Huurder");
                });

            modelBuilder.Entity("ParkDataLayer.Model.HuisEF", b =>
                {
                    b.Navigation("Huurcontracten");
                });

            modelBuilder.Entity("ParkDataLayer.Model.ParkEF", b =>
                {
                    b.Navigation("_huis");
                });
#pragma warning restore 612, 618
        }
    }
}
