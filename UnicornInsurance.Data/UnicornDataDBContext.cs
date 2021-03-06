using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using UnicornInsurance.Data.Seeds;
using UnicornInsurance.Models;
using UnicornInsurance.Models.Common;

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
        public virtual DbSet<UserMobileSuit> UserMobileSuits { get; set; }
        public virtual DbSet<UserWeapon> UserWeapons { get; set; }
        public virtual DbSet<Deployment> Deployments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<MobileSuit>(entity =>
            {
                entity.Property(e => e.Price).HasColumnType("money");

                entity.HasOne(d => d.CustomWeapon)
                    .WithOne().OnDelete(DeleteBehavior.SetNull);
            });

            modelBuilder.Entity<Weapon>(entity =>
            {
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

            modelBuilder.Entity<UserMobileSuit>(entity =>
            {
                entity.Property(e => e.IsDamaged).HasDefaultValue(false);
            });

            OnModelCreatingPartial(modelBuilder);            

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(UnicornDataDBContext).Assembly);
        }

        public virtual async Task<int> SaveChangesAsync()
        {
            foreach (var entry in base.ChangeTracker.Entries<BaseModel>()
                .Where(q => q.State == EntityState.Added || q.State == EntityState.Modified))
            {
                entry.Entity.LastModifiedDate = DateTime.Now;

                if (entry.State == EntityState.Added)
                {
                    entry.Entity.DateCreated = DateTime.Now;
                }
            }

            var result = await base.SaveChangesAsync();

            return result;
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
