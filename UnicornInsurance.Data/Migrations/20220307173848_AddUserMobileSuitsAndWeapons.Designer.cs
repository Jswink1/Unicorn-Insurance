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
    [Migration("20220307173848_AddUserMobileSuitsAndWeapons")]
    partial class AddUserMobileSuitsAndWeapons
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.13")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

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

                    b.Property<int?>("CustomWeaponId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("Height")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("ImageUrl")
                        .HasMaxLength(2000)
                        .HasColumnType("nvarchar(2000)");

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

                    b.HasIndex("CustomWeaponId")
                        .IsUnique()
                        .HasFilter("[CustomWeaponId] IS NOT NULL");

                    b.ToTable("MobileSuits");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Armor = "Gundarium Alloy",
                            CustomWeaponId = 6,
                            DateCreated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "\"To my only desire, the beast of possibility, the symbol of hope…\"",
                            Height = "19.7 meters",
                            LastModifiedDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Manufacturer = "Anaheim Electronics",
                            Name = "RX-0 Unicorn Gundam",
                            PowerOutput = "3480 kW (Unmeasurable in Destroy Mode)",
                            Price = 50000m,
                            Type = "Prototype Full Psycho-Frame Mobile Suit",
                            Weight = "42.7 metric tons"
                        },
                        new
                        {
                            Id = 2,
                            Armor = "E-Carbon",
                            CustomWeaponId = 7,
                            DateCreated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "Also known as the \"Gundam Seven Swords\". Specializes in close-quarters combat. Generates multifunctional GN Particles from the semi-perpetual GN Drive mounted in the center of the frame, which provides almost limitless energy for combat, flight, and even stealthpurposes.",
                            Height = "18.3 meters",
                            LastModifiedDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Manufacturer = "Celestial Being",
                            Name = "GN-001 Gundam Exia",
                            Price = 44999m,
                            Type = "Close Quarters Combat Mobile Suit",
                            Weight = "57.2 metric tons"
                        },
                        new
                        {
                            Id = 3,
                            Armor = "Gundarium alloy super ceramic composite",
                            CustomWeaponId = 8,
                            DateCreated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "Designed with an emphasis on mobility. The Shining Gundam specializes in martial-arts fighting.",
                            Height = "16.2 meters",
                            LastModifiedDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Manufacturer = "Neo Japan",
                            Name = "GF13-017NJ Shining Gundam",
                            Price = 41000m,
                            Type = "Close Quarters Combat Mobile Fighter",
                            Weight = "15.5 metric tons"
                        },
                        new
                        {
                            Id = 4,
                            Armor = "Nanolaminate Armor",
                            DateCreated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "Customized for close combat, with an ferocious appearance. The arms are elongated, and the limbs are outfitted with various weapons.",
                            Height = "19 meters",
                            LastModifiedDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Manufacturer = "Teiwaz",
                            Name = "ASW-G-08 Gundam Barbatos Lupus Rex",
                            Price = 41000m,
                            Type = "Custom Close Quarters Combat Mobile Suit",
                            Weight = "32.1 metric tons"
                        },
                        new
                        {
                            Id = 5,
                            DateCreated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "Aesthetically resembles a samurai, with two swords and  an enlarged V-fin that is modeled after the Crest-horn.",
                            Height = "17.9 meters",
                            LastModifiedDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Manufacturer = "Nils Nielsen",
                            Name = "Sengoku Astray Gundam",
                            Price = 42000m,
                            Type = "Custom Close Quarters Combat Mobile Suit",
                            Weight = "58.2 metric tons"
                        },
                        new
                        {
                            Id = 6,
                            DateCreated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "Originally designed as the prototype for a transformable mobile suit, however, defects in the frame led to the abandonment of the machinesdevelopment. The gold colored appearance is due to the application of a beam-resistant coating in the armor, giving the machine some limited protection against beam attacks",
                            Height = "21.4 meters",
                            LastModifiedDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Manufacturer = "Anaheim Electronics",
                            Name = "MSN-00100 Hyaku Shiki",
                            Price = 42000m,
                            Type = "Prototype Attack-Use Mobile Suit",
                            Weight = "54.5 metric tons"
                        },
                        new
                        {
                            Id = 7,
                            Armor = "Luna Titanium",
                            CustomWeaponId = 9,
                            DateCreated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "The original Gundam. Turned the tide of war in favor of the Earth Federation during the One Year War against the Principality of Zeon",
                            Height = "18.0 meters",
                            LastModifiedDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Manufacturer = "Earth Federation",
                            Name = "RX-78-2 Gundam",
                            Price = 39999m,
                            Type = "Prototype Close Quarters Combat Mobile Suit",
                            Weight = "60.0 metric tons"
                        },
                        new
                        {
                            Id = 8,
                            Armor = "Gundarium Alloy",
                            DateCreated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "Sporting multiple vernier thrusters throughout its frame, the unit is capable of achieving precise movements and high speeds. With its overwhelming combat ability, crimson body, and mono-eye sensors, the Sinanju reminds all who see it of the legendary \"Red Comet\".",
                            Height = "22.6 meters",
                            LastModifiedDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Manufacturer = "Anaheim Electronics",
                            Name = "MSN-06S Sinanju",
                            PowerOutput = "3240 kW",
                            Price = 44999m,
                            Type = "Prototype Attack-Use Mobile Suit",
                            Weight = "56.9 metric tons"
                        });
                });

            modelBuilder.Entity("UnicornInsurance.Models.MobileSuitCartItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ApplicationUserId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MobileSuitId")
                        .HasColumnType("int");

                    b.Property<decimal>("Price")
                        .HasColumnType("money");

                    b.HasKey("Id");

                    b.HasIndex("MobileSuitId");

                    b.ToTable("MobileSuitCartItems");
                });

            modelBuilder.Entity("UnicornInsurance.Models.MobileSuitPurchase", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("MobileSuitId")
                        .HasColumnType("int");

                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.Property<decimal>("Price")
                        .HasColumnType("money");

                    b.HasKey("Id");

                    b.HasIndex("MobileSuitId");

                    b.HasIndex("OrderId");

                    b.ToTable("MobileSuitPurchases");
                });

            modelBuilder.Entity("UnicornInsurance.Models.OrderHeader", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ApplicationUserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("OrderDate")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("OrderTotal")
                        .HasColumnType("money");

                    b.Property<DateTime>("PaymentDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("PaymentStatus")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TransactionId")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("OrderHeaders");
                });

            modelBuilder.Entity("UnicornInsurance.Models.UserMobileSuit", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ApplicationUserId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("CustomWeaponId")
                        .HasColumnType("int");

                    b.Property<int>("MobileSuitId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("MobileSuitId");

                    b.ToTable("UserMobileSuits");
                });

            modelBuilder.Entity("UnicornInsurance.Models.UserWeapon", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ApplicationUserId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("EquippedMobileSuitId")
                        .HasColumnType("int");

                    b.Property<bool>("IsCustomWeapon")
                        .HasColumnType("bit");

                    b.Property<int?>("UserMobileSuitId")
                        .HasColumnType("int");

                    b.Property<int>("WeaponId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("EquippedMobileSuitId");

                    b.HasIndex("UserMobileSuitId")
                        .IsUnique()
                        .HasFilter("[UserMobileSuitId] IS NOT NULL");

                    b.HasIndex("WeaponId");

                    b.ToTable("UserWeapons");
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

                    b.Property<string>("ImageUrl")
                        .HasMaxLength(2000)
                        .HasColumnType("nvarchar(2000)");

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

                    b.HasData(
                        new
                        {
                            Id = 1,
                            DateCreated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "A compact Mega-Particle Cannon. Powered by an Energy-Capitor or Energy-Pack, which stores Minovsky Particles in a condensed state.",
                            IsCustomWeapon = false,
                            LastModifiedDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Beam Rifle",
                            Price = 1750m
                        },
                        new
                        {
                            Id = 2,
                            DateCreated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "A short-range version of the Beam Rifle. Though it may lack in range, there is certainly no lack in power. Due to the shortened barrel, the focus of the beam is more widespread and destructive.",
                            IsCustomWeapon = false,
                            LastModifiedDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Beam Spray Gun",
                            Price = 1650m
                        },
                        new
                        {
                            Id = 3,
                            DateCreated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "Emits high-energy Minovsky particles, which are then contained by a blade-shaped I-field via manipulation of electromagnetic fields to form a sustained blade of superheated plasma. The Minovsky particles are stored in a E-cap within the hilt of the beam saber, which is recharged using the mobile suit's reactor when the saber is returned to its storage rack, or via a plug in the hands of certain mobile suits",
                            IsCustomWeapon = false,
                            LastModifiedDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Beam Saber",
                            Price = 1850m
                        },
                        new
                        {
                            Id = 4,
                            DateCreated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "60mm Rotary Cannons mounted on the head of a Mobile Suit. One of the most common armaments found on a Mobile Suit. They are weak in power, and low in accuracy. Their main purpose is to intercept enemy missles, fire warning shots, destroy light targets, and aid in close-quarters combat.",
                            IsCustomWeapon = false,
                            LastModifiedDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Head-Mounted Vulcans",
                            Price = 1100m
                        },
                        new
                        {
                            Id = 5,
                            DateCreated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "Funnel-shaped drone units that are remotely controlled by a Newtype pilot. They are equipped with a small beam cannons, and an energy cell to propel the funnel. When the funnels are not in use, they are attached to the mother suit's surface hardpoints for recharging.",
                            IsCustomWeapon = false,
                            LastModifiedDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Funnels",
                            Price = 1999m
                        },
                        new
                        {
                            Id = 6,
                            DateCreated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "An operating system designed with the specific purpose of combating NewTypes. A pilot can activate the NT-D system at will, or it will activate on its own in the presence of an enemy NewType. Once activated, the mobile suit will enter \"Destroy Mode\", and the pilot can control their Mobile Suit directly through brain-waves.",
                            IsCustomWeapon = true,
                            LastModifiedDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "NewType-Destroyer System",
                            Price = 3000m
                        },
                        new
                        {
                            Id = 7,
                            DateCreated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "A feature of the GN Drive that temporarily increases the performance of the equipped Mobile Suit. Once activated, a machine can perform at three times the normal output for a limited time.The GN Particles emitted from the mobile suit during Trand-Am will cause the suit to glow red.",
                            IsCustomWeapon = true,
                            LastModifiedDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Trans-Am System",
                            Price = 2750m
                        },
                        new
                        {
                            Id = 8,
                            DateCreated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "An ultimate martial-arts move, in which the finger joints of the Mobile Suit are uncovered and the hand becomes coated inliquid metal, concentrating a massive amount of energy into the hand that can be utilized in a devastating attack.",
                            IsCustomWeapon = true,
                            LastModifiedDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Shining Finger",
                            Price = 2199m
                        },
                        new
                        {
                            Id = 9,
                            DateCreated = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "Simple defensive equipment that can be carried or mounted to a Mobile Suit. Weighing 10 tons, it is designed for shock diffusion and absorption rather than robustness. Features a triple honeycomb structure, with a high-density ceramic material based on super-hard steel alloy sandwiched between aramid fibers and an outermost layer made of luna titanium alloy. The surface is filled with a resin made of a polymer material for improved elasticity.",
                            IsCustomWeapon = true,
                            LastModifiedDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "RX Shield",
                            Price = 1000m
                        });
                });

            modelBuilder.Entity("UnicornInsurance.Models.WeaponCartItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ApplicationUserId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Count")
                        .HasColumnType("int");

                    b.Property<decimal>("Price")
                        .HasColumnType("money");

                    b.Property<int>("WeaponId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("WeaponId");

                    b.ToTable("WeaponCartItems");
                });

            modelBuilder.Entity("UnicornInsurance.Models.WeaponPurchase", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Count")
                        .HasColumnType("int");

                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.Property<decimal>("Price")
                        .HasColumnType("money");

                    b.Property<int>("WeaponId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.HasIndex("WeaponId");

                    b.ToTable("WeaponPurchases");
                });

            modelBuilder.Entity("UnicornInsurance.Models.MobileSuit", b =>
                {
                    b.HasOne("UnicornInsurance.Models.Weapon", "CustomWeapon")
                        .WithOne()
                        .HasForeignKey("UnicornInsurance.Models.MobileSuit", "CustomWeaponId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("CustomWeapon");
                });

            modelBuilder.Entity("UnicornInsurance.Models.MobileSuitCartItem", b =>
                {
                    b.HasOne("UnicornInsurance.Models.MobileSuit", "MobileSuit")
                        .WithMany()
                        .HasForeignKey("MobileSuitId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("MobileSuit");
                });

            modelBuilder.Entity("UnicornInsurance.Models.MobileSuitPurchase", b =>
                {
                    b.HasOne("UnicornInsurance.Models.MobileSuit", "MobileSuit")
                        .WithMany()
                        .HasForeignKey("MobileSuitId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("UnicornInsurance.Models.OrderHeader", "OrderHeader")
                        .WithMany()
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("MobileSuit");

                    b.Navigation("OrderHeader");
                });

            modelBuilder.Entity("UnicornInsurance.Models.UserMobileSuit", b =>
                {
                    b.HasOne("UnicornInsurance.Models.MobileSuit", "MobileSuit")
                        .WithMany()
                        .HasForeignKey("MobileSuitId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("MobileSuit");
                });

            modelBuilder.Entity("UnicornInsurance.Models.UserWeapon", b =>
                {
                    b.HasOne("UnicornInsurance.Models.UserMobileSuit", "EquippedMobileSuit")
                        .WithMany()
                        .HasForeignKey("EquippedMobileSuitId");

                    b.HasOne("UnicornInsurance.Models.UserMobileSuit", null)
                        .WithOne("CustomWeapon")
                        .HasForeignKey("UnicornInsurance.Models.UserWeapon", "UserMobileSuitId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("UnicornInsurance.Models.Weapon", "Weapon")
                        .WithMany()
                        .HasForeignKey("WeaponId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("EquippedMobileSuit");

                    b.Navigation("Weapon");
                });

            modelBuilder.Entity("UnicornInsurance.Models.WeaponCartItem", b =>
                {
                    b.HasOne("UnicornInsurance.Models.Weapon", "Weapon")
                        .WithMany()
                        .HasForeignKey("WeaponId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Weapon");
                });

            modelBuilder.Entity("UnicornInsurance.Models.WeaponPurchase", b =>
                {
                    b.HasOne("UnicornInsurance.Models.OrderHeader", "OrderHeader")
                        .WithMany()
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("UnicornInsurance.Models.Weapon", "Weapon")
                        .WithMany()
                        .HasForeignKey("WeaponId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("OrderHeader");

                    b.Navigation("Weapon");
                });

            modelBuilder.Entity("UnicornInsurance.Models.UserMobileSuit", b =>
                {
                    b.Navigation("CustomWeapon");
                });
#pragma warning restore 612, 618
        }
    }
}
