using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using UnicornInsurance.Data.Seeds;
using UnicornInsurance.Models;

#nullable disable

namespace UnicornInsurance.Data
{
    public partial class UnicornDataDBContext : DbContext
    {
        public UnicornDataDBContext()
        {
        }

        public UnicornDataDBContext(DbContextOptions<UnicornDataDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<MobileSuit> MobileSuits { get; set; }
        public virtual DbSet<Weapon> Weapons { get; set; }
        public virtual DbSet<MobileSuitCartItem> MobileSuitCartItems { get; set; }
        public virtual DbSet<WeaponCartItem> WeaponCartItems { get; set; }
        public virtual DbSet<OrderHeader> OrderHeaders { get; set; }
        public virtual DbSet<MobileSuitPurchase> MobileSuitPurchases { get; set; }
        public virtual DbSet<WeaponPurchase> WeaponPurchases { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<MobileSuit>(entity =>
            {
                entity.Property(e => e.Armor).HasMaxLength(100);

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.Height).HasMaxLength(100);

                entity.Property(e => e.ImageUrl).HasMaxLength(2000);

                entity.Property(e => e.Manufacturer).HasMaxLength(100);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.PowerOutput).HasMaxLength(100);

                entity.Property(e => e.Price).HasColumnType("money");

                entity.Property(e => e.Type).HasMaxLength(200);

                entity.Property(e => e.Weight).HasMaxLength(100);

                entity.HasOne(d => d.CustomWeapon)
                    .WithOne().OnDelete(DeleteBehavior.SetNull);
            });

            modelBuilder.Entity<Weapon>(entity =>
            {
                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.ImageUrl).HasMaxLength(2000);

                entity.Property(e => e.IsCustomWeapon).IsRequired();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.Price).HasColumnType("money");                
            });

            modelBuilder.Entity<MobileSuitCartItem>(entity =>
            {
                entity.Property(e => e.Price).HasColumnType("money");
            });

            modelBuilder.Entity<WeaponCartItem>(entity =>
            {
                entity.Property(e => e.Price).HasColumnType("money");
            });

            modelBuilder.Entity<OrderHeader>(entity =>
            {
                entity.Property(e => e.OrderTotal).HasColumnType("money");
            });

            modelBuilder.Entity<MobileSuitPurchase>(entity =>
            {
                entity.Property(e => e.Price).HasColumnType("money");
            });

            modelBuilder.Entity<WeaponPurchase>(entity =>
            {
                entity.Property(e => e.Price).HasColumnType("money");
            });

            OnModelCreatingPartial(modelBuilder);

            modelBuilder.ApplyConfiguration(new MobileSuitSeed());
            modelBuilder.ApplyConfiguration(new WeaponSeed());
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
