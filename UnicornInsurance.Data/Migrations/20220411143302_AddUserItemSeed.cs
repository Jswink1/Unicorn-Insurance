using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace UnicornInsurance.Data.Migrations
{
    public partial class AddUserItemSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "UserMobileSuits",
                columns: new[] { "Id", "ApplicationUserId", "EndOfCoverage", "InsurancePlan", "MobileSuitId" },
                values: new object[] { 1, "9e224968-33e4-4652-b7b7-8574d048cdb9", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 1 });

            migrationBuilder.InsertData(
                table: "UserWeapons",
                columns: new[] { "Id", "ApplicationUserId", "EquippedMobileSuitId", "IsCustomWeapon", "WeaponId" },
                values: new object[] { 1, "9e224968-33e4-4652-b7b7-8574d048cdb9", null, false, 1 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UserMobileSuits",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "UserWeapons",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
