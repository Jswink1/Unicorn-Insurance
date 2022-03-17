using Microsoft.EntityFrameworkCore.Migrations;

namespace UnicornInsurance.Data.Migrations
{
    public partial class RefactorSeeds : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "MobileSuits",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Description", "ImageUrl" },
                values: new object[] { "\"To my only desire, the symbol of hope, the beast of possibility...\"", "\\images\\mobilesuits\\UnicornGundam.webp" });

            migrationBuilder.UpdateData(
                table: "MobileSuits",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Description", "ImageUrl" },
                values: new object[] { "Also known as the \"Gundam Seven Swords\". Specializes in close-quarters combat. Generates multifunctional GN Particles from the semi-perpetual GN Drive mounted in the center of the frame, which provides almost limitless energy for combat, flight, and even stealth purposes.", "\\images\\mobilesuits\\Exia.webp" });

            migrationBuilder.UpdateData(
                table: "MobileSuits",
                keyColumn: "Id",
                keyValue: 3,
                column: "ImageUrl",
                value: "\\images\\mobilesuits\\ShiningGundam.webp");

            migrationBuilder.UpdateData(
                table: "MobileSuits",
                keyColumn: "Id",
                keyValue: 4,
                column: "ImageUrl",
                value: "\\images\\mobilesuits\\BarbatosLupus.webp");

            migrationBuilder.UpdateData(
                table: "MobileSuits",
                keyColumn: "Id",
                keyValue: 5,
                column: "ImageUrl",
                value: "\\images\\mobilesuits\\SengokuAstray.webp");

            migrationBuilder.UpdateData(
                table: "MobileSuits",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Description", "ImageUrl" },
                values: new object[] { "Originally designed as the prototype for a transformable mobile suit, however, defects in the frame led to the abandonment of the machines development. The gold colored appearance is due to the application of a beam-resistant coating in the armor, giving the machine some limited protection against beam attacks", "\\images\\mobilesuits\\HyakuShiki.webp" });

            migrationBuilder.UpdateData(
                table: "MobileSuits",
                keyColumn: "Id",
                keyValue: 7,
                column: "ImageUrl",
                value: "\\images\\mobilesuits\\RX78Gundam.webp");

            migrationBuilder.UpdateData(
                table: "MobileSuits",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "Description", "ImageUrl" },
                values: new object[] { "Sporting multiple vernier thrusters throughout its frame, this unit is capable of achieving precise movements and high speeds. With its overwhelming combat ability, crimson body, and mono-eye sensors, the Sinanju reminds all who see it of the legendary \"Red Comet\".", "\\images\\mobilesuits\\Sinanju.webp" });

            migrationBuilder.UpdateData(
                table: "Weapons",
                keyColumn: "Id",
                keyValue: 1,
                column: "ImageUrl",
                value: "\\images\\weapons\\BeamRifle.webp");

            migrationBuilder.UpdateData(
                table: "Weapons",
                keyColumn: "Id",
                keyValue: 2,
                column: "ImageUrl",
                value: "\\images\\weapons\\BeamRifle.webp");

            migrationBuilder.UpdateData(
                table: "Weapons",
                keyColumn: "Id",
                keyValue: 3,
                column: "ImageUrl",
                value: "\\images\\weapons\\BeamSrayGun.webp");

            migrationBuilder.UpdateData(
                table: "Weapons",
                keyColumn: "Id",
                keyValue: 4,
                column: "ImageUrl",
                value: "\\images\\weapons\\HeadMountedVulcans.PNG");

            migrationBuilder.UpdateData(
                table: "Weapons",
                keyColumn: "Id",
                keyValue: 5,
                column: "ImageUrl",
                value: "\\images\\weapons\\Funnels.webp");

            migrationBuilder.UpdateData(
                table: "Weapons",
                keyColumn: "Id",
                keyValue: 7,
                column: "Description",
                value: "A feature of the GN Drive that temporarily increases the performance of the equipped Mobile Suit. Once activated, a machine can perform at three times the normal output for a limited time. The GN Particles emitted from the mobile suit during Trand-Am will cause the suit to glow red.");

            migrationBuilder.UpdateData(
                table: "Weapons",
                keyColumn: "Id",
                keyValue: 8,
                column: "Description",
                value: "An ultimate martial-arts move, in which the finger joints of the Mobile Suit are uncovered and the hand becomes coated in liquid metal, concentrating a massive amount of energy into the hand that can be utilized in a devastating attack.");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "MobileSuits",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Description", "ImageUrl" },
                values: new object[] { "\"To my only desire, the beast of possibility, the symbol of hope…\"", null });

            migrationBuilder.UpdateData(
                table: "MobileSuits",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Description", "ImageUrl" },
                values: new object[] { "Also known as the \"Gundam Seven Swords\". Specializes in close-quarters combat. Generates multifunctional GN Particles from the semi-perpetual GN Drive mounted in the center of the frame, which provides almost limitless energy for combat, flight, and even stealthpurposes.", null });

            migrationBuilder.UpdateData(
                table: "MobileSuits",
                keyColumn: "Id",
                keyValue: 3,
                column: "ImageUrl",
                value: null);

            migrationBuilder.UpdateData(
                table: "MobileSuits",
                keyColumn: "Id",
                keyValue: 4,
                column: "ImageUrl",
                value: null);

            migrationBuilder.UpdateData(
                table: "MobileSuits",
                keyColumn: "Id",
                keyValue: 5,
                column: "ImageUrl",
                value: null);

            migrationBuilder.UpdateData(
                table: "MobileSuits",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "Description", "ImageUrl" },
                values: new object[] { "Originally designed as the prototype for a transformable mobile suit, however, defects in the frame led to the abandonment of the machinesdevelopment. The gold colored appearance is due to the application of a beam-resistant coating in the armor, giving the machine some limited protection against beam attacks", null });

            migrationBuilder.UpdateData(
                table: "MobileSuits",
                keyColumn: "Id",
                keyValue: 7,
                column: "ImageUrl",
                value: null);

            migrationBuilder.UpdateData(
                table: "MobileSuits",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "Description", "ImageUrl" },
                values: new object[] { "Sporting multiple vernier thrusters throughout its frame, the unit is capable of achieving precise movements and high speeds. With its overwhelming combat ability, crimson body, and mono-eye sensors, the Sinanju reminds all who see it of the legendary \"Red Comet\".", null });

            migrationBuilder.UpdateData(
                table: "Weapons",
                keyColumn: "Id",
                keyValue: 1,
                column: "ImageUrl",
                value: null);

            migrationBuilder.UpdateData(
                table: "Weapons",
                keyColumn: "Id",
                keyValue: 2,
                column: "ImageUrl",
                value: null);

            migrationBuilder.UpdateData(
                table: "Weapons",
                keyColumn: "Id",
                keyValue: 3,
                column: "ImageUrl",
                value: null);

            migrationBuilder.UpdateData(
                table: "Weapons",
                keyColumn: "Id",
                keyValue: 4,
                column: "ImageUrl",
                value: null);

            migrationBuilder.UpdateData(
                table: "Weapons",
                keyColumn: "Id",
                keyValue: 5,
                column: "ImageUrl",
                value: null);

            migrationBuilder.UpdateData(
                table: "Weapons",
                keyColumn: "Id",
                keyValue: 7,
                column: "Description",
                value: "A feature of the GN Drive that temporarily increases the performance of the equipped Mobile Suit. Once activated, a machine can perform at three times the normal output for a limited time.The GN Particles emitted from the mobile suit during Trand-Am will cause the suit to glow red.");

            migrationBuilder.UpdateData(
                table: "Weapons",
                keyColumn: "Id",
                keyValue: 8,
                column: "Description",
                value: "An ultimate martial-arts move, in which the finger joints of the Mobile Suit are uncovered and the hand becomes coated inliquid metal, concentrating a massive amount of energy into the hand that can be utilized in a devastating attack.");
        }
    }
}
