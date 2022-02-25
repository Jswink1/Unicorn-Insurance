﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using UnicornInsurance.Data;

namespace UnicornInsurance.Data.Migrations
{
    [DbContext(typeof(UnicornDataDBContext))]
    [Migration("20220204213140_Start")]
    partial class Start
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.13")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("UnicornInsurance.Models.CustomWeapon", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("MobileSuitId")
                        .HasColumnType("int");

                    b.Property<int>("WeaponId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("MobileSuitId")
                        .IsUnique();

                    b.HasIndex("WeaponId")
                        .IsUnique();

                    b.HasIndex(new[] { "MobileSuitId" }, "IX_CustomWeapons_MobileSuitId")
                        .HasDatabaseName("IX_CustomWeapons_MobileSuitId1");

                    b.HasIndex(new[] { "WeaponId" }, "IX_CustomWeapons_WeaponId")
                        .HasDatabaseName("IX_CustomWeapons_WeaponId1");

                    b.ToTable("CustomWeapons");
                });

            modelBuilder.Entity("UnicornInsurance.Models.MobileSuit", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Armor")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<bool>("HasCustomWeapon")
                        .HasColumnType("bit");

                    b.Property<string>("Height")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("LastModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Manufacturer")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("PowerOutput")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<decimal>("Price")
                        .HasColumnType("money");

                    b.Property<string>("Type")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Weight")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("MobileSuits");
                });

            modelBuilder.Entity("UnicornInsurance.Models.Weapon", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<bool>("IsCustomWeapon")
                        .HasColumnType("bit");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("LastModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<decimal>("Price")
                        .HasColumnType("money");

                    b.HasKey("Id");

                    b.ToTable("Weapons");
                });

            modelBuilder.Entity("UnicornInsurance.Models.CustomWeapon", b =>
                {
                    b.HasOne("UnicornInsurance.Models.MobileSuit", "MobileSuit")
                        .WithOne("CustomWeapon")
                        .HasForeignKey("UnicornInsurance.Models.CustomWeapon", "MobileSuitId")
                        .HasConstraintName("FK_CustomWeapons_ToMobileSuits")
                        .IsRequired();

                    b.HasOne("UnicornInsurance.Models.Weapon", "Weapon")
                        .WithOne("CustomWeapon")
                        .HasForeignKey("UnicornInsurance.Models.CustomWeapon", "WeaponId")
                        .HasConstraintName("FK_CustomWeapons_ToWeapons")
                        .IsRequired();

                    b.Navigation("MobileSuit");

                    b.Navigation("Weapon");
                });

            modelBuilder.Entity("UnicornInsurance.Models.MobileSuit", b =>
                {
                    b.Navigation("CustomWeapon");
                });

            modelBuilder.Entity("UnicornInsurance.Models.Weapon", b =>
                {
                    b.Navigation("CustomWeapon");
                });
#pragma warning restore 612, 618
        }
    }
}
