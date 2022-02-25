using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnicornInsurance.Identity.Models;

namespace UnicornInsurance.Identity.Seeds
{
    public class UserSeed : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            var hasher = new PasswordHasher<ApplicationUser>();
            builder.HasData(
                 new ApplicationUser
                 {
                     Id = "8e445865-a24d-4543-a6c6-9443d048cdb9",
                     Email = "admin@user.com",
                     NormalizedEmail = "ADMIN@USER.COM",
                     Name = "Admin",
                     UserName = "admin@user.com",
                     NormalizedUserName = "ADMIN@USER.COM",
                     PasswordHash = hasher.HashPassword(null, "password"),
                     EmailConfirmed = true
                 },
                 new ApplicationUser
                 {
                     Id = "9e224968-33e4-4652-b7b7-8574d048cdb9",
                     Email = "user@user.com",
                     NormalizedEmail = "USER@USER.COM",
                     Name = "User",
                     UserName = "user@user.com",
                     NormalizedUserName = "USER@USER.COM",
                     PasswordHash = hasher.HashPassword(null, "password"),
                     EmailConfirmed = true
                 }
            );
        }
    }
}
