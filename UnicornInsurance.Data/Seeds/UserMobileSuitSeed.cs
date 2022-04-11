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
    public class UserMobileSuitSeed : IEntityTypeConfiguration<UserMobileSuit>
    {
        public void Configure(EntityTypeBuilder<UserMobileSuit> builder)
        {
            builder.HasData(
                new UserMobileSuit
                {
                    Id = 1,
                    MobileSuitId = 1,
                    ApplicationUserId = "9e224968-33e4-4652-b7b7-8574d048cdb9",
                    IsDamaged = false                    
                }
            );
        }
    }
}
