using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace UnicornInsurance.Data.Migrations
{
    public partial class SeedDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "MobileSuits",
                columns: new[] { "Id", "Armor", "CreatedBy", "DateCreated", "Description", "HasCustomWeapon", "Height", "LastModifiedBy", "LastModifiedDate", "Manufacturer", "Name", "PowerOutput", "Price", "Type", "Weight" },
                values: new object[,]
                {
                    { 1, "Gundarium Alloy", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "\"To my only desire, the beast of possibility, the symbol of hope…\"", true, "19.7 meters", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Anaheim Electronics", "RX-0 Unicorn Gundam", "3480 kW (Unmeasurable in Destroy Mode)", 50000m, "Prototype Full Psycho-Frame Mobile Suit", "42.7 metric tons" },
                    { 2, "E-Carbon", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Also known as the \"Gundam Seven Swords\". Specializes in close-quarters combat. Generates multifunctional GN Particles from the semi-perpetual GN Drive mounted in the center of the frame, which provides almost limitless energy for combat, flight, and even stealthpurposes.", true, "18.3 meters", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Celestial Being", "GN-001 Gundam Exia", null, 44999m, "Close Quarters Combat Mobile Suit", "57.2 metric tons" },
                    { 3, "Gundarium alloy super ceramic composite", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Designed with an emphasis on mobility. The Shining Gundam specializes in martial-arts fighting.", true, "16.2 meters", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Neo Japan", "GF13-017NJ Shining Gundam", null, 41000m, "Close Quarters Combat Mobile Fighter", "15.5 metric tons" },
                    { 4, "Nanolaminate Armor", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Customized for close combat, with an ferocious appearance. The arms are elongated, and the limbs are outfitted with various weapons.", false, "19 meters", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Teiwaz", "ASW-G-08 Gundam Barbatos Lupus Rex", null, 41000m, "Custom Close Quarters Combat Mobile Suit", "32.1 metric tons" },
                    { 5, null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Aesthetically resembles a samurai, with two swords and  an enlarged V-fin that is modeled after the Crest-horn.", false, "17.9 meters", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Nils Nielsen", "Sengoku Astray Gundam", null, 42000m, "Custom Close Quarters Combat Mobile Suit", "58.2 metric tons" },
                    { 6, null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Originally designed as the prototype for a transformable mobile suit, however, defects in the frame led to the abandonment of the machinesdevelopment. The gold colored appearance is due to the application of a beam-resistant coating in the armor, giving the machine some limited protection against beam attacks", false, "21.4 meters", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Anaheim Electronics", "MSN-00100 Hyaku Shiki", null, 42000m, "Prototype Attack-Use Mobile Suit", "54.5 metric tons" },
                    { 7, "Luna Titanium", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "The original Gundam. Turned the tide of war in favor of the Earth Federation during the One Year War against the Principality of Zeon", true, "18.0 meters", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Earth Federation", "RX-78-2 Gundam", null, 39999m, "Prototype Close Quarters Combat Mobile Suit", "60.0 metric tons" },
                    { 8, "Gundarium Alloy", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sporting multiple vernier thrusters throughout its frame, the unit is capable of achieving precise movements and high speeds. With its overwhelming combat ability, crimson body, and mono-eye sensors, the Sinanju reminds all who see it of the legendary \"Red Comet\".", false, "22.6 meters", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Anaheim Electronics", "MSN-06S Sinanju", "3240 kW", 44999m, "Prototype Attack-Use Mobile Suit", "56.9 metric tons" }
                });

            migrationBuilder.InsertData(
                table: "Weapons",
                columns: new[] { "Id", "CreatedBy", "DateCreated", "Description", "IsCustomWeapon", "LastModifiedBy", "LastModifiedDate", "Name", "Price" },
                values: new object[,]
                {
                    { 7, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "A feature of the GN Drive that temporarily increases the performance of the equipped Mobile Suit. Once activated, a machine can perform at three times the normal output for a limited time.The GN Particles emitted from the mobile suit during Trand-Am will cause the suit to glow red.", true, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Trans-Am System", 2750m },
                    { 6, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "An operating system designed with the specific purpose of combating NewTypes. A pilot can activate the NT-D system at will, or it will activate on its own in the presence of an enemy NewType. Once activated, the mobile suit will enter \"Destroy Mode\", and the pilot can control their Mobile Suit directly through brain-waves.", true, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "NewType-Destroyer System", 3000m },
                    { 5, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Funnel-shaped drone units that are remotely controlled by a Newtype pilot. They are equipped with a small beam cannons, and an energy cell to propel the funnel. When the funnels are not in use, they are attached to the mother suit's surface hardpoints for recharging.", false, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Funnels", 1999m },
                    { 1, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "A compact Mega-Particle Cannon. Powered by an Energy-Capitor or Energy-Pack, which stores Minovsky Particles in a condensed state.", false, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Beam Rifle", 1750m },
                    { 3, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Emits high-energy Minovsky particles, which are then contained by a blade-shaped I-field via manipulation of electromagnetic fields to form a sustained blade of superheated plasma. The Minovsky particles are stored in a E-cap within the hilt of the beam saber, which is recharged using the mobile suit's reactor when the saber is returned to its storage rack, or via a plug in the hands of certain mobile suits", false, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Beam Saber", 1850m },
                    { 2, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "A short-range version of the Beam Rifle. Though it may lack in range, there is certainly no lack in power. Due to the shortened barrel, the focus of the beam is more widespread and destructive.", false, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Beam Spray Gun", 1650m },
                    { 8, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "An ultimate martial-arts move, in which the finger joints of the Mobile Suit are uncovered and the hand becomes coated inliquid metal, concentrating a massive amount of energy into the hand that can be utilized in a devastating attack.", true, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Shining Finger", 2199m },
                    { 4, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "60mm Rotary Cannons mounted on the head of a Mobile Suit. One of the most common armaments found on a Mobile Suit. They are weak in power, and low in accuracy. Their main purpose is to intercept enemy missles, fire warning shots, destroy light targets, and aid in close-quarters combat.", false, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Head-Mounted Vulcans", 1100m },
                    { 9, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Simple defensive equipment that can be carried or mounted to a Mobile Suit. Weighing 10 tons, it is designed for shock diffusion and absorption rather than robustness. Features a triple honeycomb structure, with a high-density ceramic material based on super-hard steel alloy sandwiched between aramid fibers and an outermost layer made of luna titanium alloy. The surface is filled with a resin made of a polymer material for improved elasticity.", true, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "RX Shield", 1000m }
                });

            migrationBuilder.InsertData(
                table: "CustomWeapons",
                columns: new[] { "Id", "MobileSuitId", "WeaponId" },
                values: new object[,]
                {
                    { 1, 1, 6 },
                    { 2, 2, 7 },
                    { 3, 3, 8 },
                    { 4, 7, 9 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "CustomWeapons",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "CustomWeapons",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "CustomWeapons",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "CustomWeapons",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "MobileSuits",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "MobileSuits",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "MobileSuits",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "MobileSuits",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Weapons",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Weapons",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Weapons",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Weapons",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Weapons",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "MobileSuits",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "MobileSuits",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "MobileSuits",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "MobileSuits",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Weapons",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Weapons",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Weapons",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Weapons",
                keyColumn: "Id",
                keyValue: 9);
        }
    }
}
