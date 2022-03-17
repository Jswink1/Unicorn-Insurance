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
    public class WeaponSeed : IEntityTypeConfiguration<Weapon>
    {
        public void Configure(EntityTypeBuilder<Weapon> builder)
        {
            builder.HasData(
                new Weapon
                {
                    Id = 1,
                    Name = "Beam Rifle",
                    Description = "A compact Mega-Particle Cannon. Powered by an Energy-Capitor or Energy-Pack, which stores Minovsky Particles in a condensed state.",
                    Price = 1750m,
                    IsCustomWeapon = false,
                    ImageUrl = "\\images\\weapons\\BeamRifle.webp"
                },
                new Weapon
                {
                    Id = 2,
                    Name = "Beam Spray Gun",
                    Description = "A short-range version of the Beam Rifle. Though it may lack in range, there is certainly no lack in power. " +
                                  "Due to the shortened barrel, the focus of the beam is more widespread and destructive.",
                    Price = 1650m,
                    IsCustomWeapon = false,
                    ImageUrl = "\\images\\weapons\\BeamSprayGun.webp"
                },
                new Weapon
                {
                    Id = 3,
                    Name = "Beam Saber",
                    Description = "Emits high-energy Minovsky particles, which are then contained by a blade-shaped I-field via manipulation of electromagnetic fields " +
                                  "to form a sustained blade of superheated plasma. The Minovsky particles are stored in a E-cap within the hilt of the beam saber, which " +
                                  "is recharged using the mobile suit's reactor when the saber is returned to its storage rack, or via a plug in the hands of certain mobile suits",
                    Price = 1850m,
                    IsCustomWeapon = false,
                    ImageUrl = "\\images\\weapons\\BeamSaber.webp"
                },
                new Weapon
                {
                    Id = 4,
                    Name = "Head-Mounted Vulcans",
                    Description = "60mm Rotary Cannons mounted on the head of a Mobile Suit. One of the most common armaments found on a Mobile Suit. " +
                                  "They are weak in power, and low in accuracy. Their main purpose is to intercept enemy missles, fire warning shots, destroy light targets, " +
                                  "and aid in close-quarters combat.",
                    Price = 1100m,
                    IsCustomWeapon = false,
                    ImageUrl = "\\images\\weapons\\HeadMountedVulcans.PNG"
                },
                new Weapon
                {
                    Id = 5,
                    Name = "Funnels",
                    Description = "Funnel-shaped drone units that are remotely controlled by a Newtype pilot. They are equipped with a small beam cannons, " +
                                  "and an energy cell to propel the funnel. When the funnels are not in use, they are attached to the mother suit's surface hardpoints " +
                                  "for recharging.",
                    Price = 1999m,
                    IsCustomWeapon = false,
                    ImageUrl = "\\images\\weapons\\Funnels.jpg"
                },
                new Weapon
                {
                    Id = 6,
                    Name = "NewType-Destroyer System",
                    Description = "An operating system designed with the specific purpose of combating NewTypes. A pilot can activate the NT-D system at will, or it will " +
                                          "activate on its own in the presence of an enemy NewType. Once activated, the mobile suit will enter \"Destroy Mode\", and the pilot " +
                                          "can control their Mobile Suit directly through brain-waves.",
                    Price = 3000m,
                    IsCustomWeapon = true
                },
                new Weapon
                {
                    Id = 7,
                    Name = "Trans-Am System",
                    Description = "A feature of the GN Drive that temporarily increases the performance of the equipped Mobile Suit. Once activated, a machine " +
                                          "can perform at three times the normal output for a limited time. The GN Particles emitted from the mobile suit during Trand-Am " +
                                          "will cause the suit to glow red.",
                    Price = 2750m,
                    IsCustomWeapon = true
                },
                new Weapon
                {
                    Id = 8,
                    Name = "Shining Finger",
                    Description = "An ultimate martial-arts move, in which the finger joints of the Mobile Suit are uncovered and the hand becomes coated in " +
                                          "liquid metal, concentrating a massive amount of energy into the hand that can be utilized in a devastating attack.",
                    Price = 2199m,
                    IsCustomWeapon = true
                },
                new Weapon
                {
                    Id = 9,
                    Name = "RX Shield",
                    Description = "Simple defensive equipment that can be carried or mounted to a Mobile Suit. Weighing 10 tons, it is designed for shock diffusion " +
                                          "and absorption rather than robustness. Features a triple honeycomb structure, with a high-density ceramic material based on " +
                                          "super-hard steel alloy sandwiched between aramid fibers and an outermost layer made of luna titanium alloy. The surface is filled " +
                                          "with a resin made of a polymer material for improved elasticity.",
                    Price = 1000m,
                    IsCustomWeapon = true
                }
            );
        }
    }
}
