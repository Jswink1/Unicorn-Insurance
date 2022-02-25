using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnicornInsurance.Identity.Models;
using UnicornInsurance.Identity.Seeds;

namespace UnicornInsurance.Identity
{
    public class UnicornIdentityDBContext : IdentityDbContext<ApplicationUser>
    {
        public UnicornIdentityDBContext(DbContextOptions<UnicornIdentityDBContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new RoleSeed());
            builder.ApplyConfiguration(new UserSeed());
            builder.ApplyConfiguration(new UserRoleSeed());
        }
    }
}
