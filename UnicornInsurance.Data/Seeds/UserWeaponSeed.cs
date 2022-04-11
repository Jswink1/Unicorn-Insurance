using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnicornInsurance.Models;

namespace UnicornInsurance.Data.Seeds
{
    public class UserWeaponSeed : IEntityTypeConfiguration<UserWeapon>
    {
        public void Configure(EntityTypeBuilder<UserWeapon> builder)
        {
            builder.HasData(
                new UserWeapon
                {
                    Id = 1,
                    WeaponId = 1,
                    ApplicationUserId = "9e224968-33e4-4652-b7b7-8574d048cdb9",
                    IsCustomWeapon = false,
                    EquippedMobileSuitId = null
                }
            );
        }
    }
}
