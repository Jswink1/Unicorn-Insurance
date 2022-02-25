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
    public class MobileSuitSeed : IEntityTypeConfiguration<MobileSuit>
    {
        public void Configure(EntityTypeBuilder<MobileSuit> builder)
        {
            builder.HasData(
                new MobileSuit
                {
                    Id = 1,
                    Name = "RX-0 Unicorn Gundam",
                    Type = "Prototype Full Psycho-Frame Mobile Suit",
                    Description = "\"To my only desire, the beast of possibility, the symbol of hope…\"",
                    Price = 50000m,
                    Manufacturer = "Anaheim Electronics",
                    Height = "19.7 meters",
                    Weight = "42.7 metric tons",
                    PowerOutput = "3480 kW (Unmeasurable in Destroy Mode)",
                    Armor = "Gundarium Alloy",
                    CustomWeaponId = 6
                },
                new MobileSuit
                {
                    Id = 2,
                    Name = "GN-001 Gundam Exia",
                    Type = "Close Quarters Combat Mobile Suit",
                    Description = "Also known as the \"Gundam Seven Swords\". Specializes in close-quarters combat. Generates multifunctional GN Particles from the " +
                                  "semi-perpetual GN Drive mounted in the center of the frame, which provides almost limitless energy for combat, flight, and even stealth" +
                                  "purposes.",
                    Price = 44999m,
                    Manufacturer = "Celestial Being",
                    Height = "18.3 meters",
                    Weight = "57.2 metric tons",
                    Armor = "E-Carbon",
                    CustomWeaponId = 7
                },
                new MobileSuit
                {
                    Id = 3,
                    Name = "GF13-017NJ Shining Gundam",
                    Type = "Close Quarters Combat Mobile Fighter",
                    Description = "Designed with an emphasis on mobility. The Shining Gundam specializes in martial-arts fighting.",
                    Price = 41000m,
                    Manufacturer = "Neo Japan",
                    Height = "16.2 meters",
                    Weight = "15.5 metric tons",
                    Armor = "Gundarium alloy super ceramic composite",
                    CustomWeaponId = 8
                },
                new MobileSuit
                {
                    Id = 4,
                    Name = "ASW-G-08 Gundam Barbatos Lupus Rex",
                    Type = "Custom Close Quarters Combat Mobile Suit",
                    Description = "Customized for close combat, with an ferocious appearance. The arms are elongated, and the limbs are outfitted with various weapons.",
                    Price = 41000m,
                    Manufacturer = "Teiwaz",
                    Height = "19 meters",
                    Weight = "32.1 metric tons",
                    Armor = "Nanolaminate Armor"
                },
                new MobileSuit
                {
                    Id = 5,
                    Name = "Sengoku Astray Gundam",
                    Type = "Custom Close Quarters Combat Mobile Suit",
                    Description = "Aesthetically resembles a samurai, with two swords and  an enlarged V-fin that is modeled after the Crest-horn.",
                    Price = 42000m,
                    Manufacturer = "Nils Nielsen",
                    Height = "17.9 meters",
                    Weight = "58.2 metric tons"
                },
                new MobileSuit
                {
                    Id = 6,
                    Name = "MSN-00100 Hyaku Shiki",
                    Type = "Prototype Attack-Use Mobile Suit",
                    Description = "Originally designed as the prototype for a transformable mobile suit, however, defects in the frame led to the abandonment of the machines" +
                                  "development. The gold colored appearance is due to the application of a beam-resistant coating in the armor, " +
                                  "giving the machine some limited protection against beam attacks",
                    Price = 42000m,
                    Manufacturer = "Anaheim Electronics",
                    Height = "21.4 meters",
                    Weight = "54.5 metric tons"
                },
                new MobileSuit
                {
                    Id = 7,
                    Name = "RX-78-2 Gundam",
                    Type = "Prototype Close Quarters Combat Mobile Suit",
                    Description = "The original Gundam. Turned the tide of war in favor of the Earth Federation during the One Year War against the Principality of Zeon",
                    Price = 39999m,
                    Manufacturer = "Earth Federation",
                    Height = "18.0 meters",
                    Weight = "60.0 metric tons",
                    Armor = "Luna Titanium",
                    CustomWeaponId = 9
                },
                new MobileSuit
                {
                    Id = 8,
                    Name = "MSN-06S Sinanju",
                    Type = "Prototype Attack-Use Mobile Suit",
                    Description = "Sporting multiple vernier thrusters throughout its frame, the unit is capable of achieving precise movements and high speeds. With its " +
                                  "overwhelming combat ability, crimson body, and mono-eye sensors, the Sinanju reminds all who see it of the legendary \"Red Comet\".",
                    Price = 44999m,
                    Manufacturer = "Anaheim Electronics",
                    Height = "22.6 meters",
                    Weight = "56.9 metric tons",
                    PowerOutput = "3240 kW",
                    Armor = "Gundarium Alloy"
                }
            );
        }
    }
}
