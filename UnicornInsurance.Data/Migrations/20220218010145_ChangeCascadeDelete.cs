using Microsoft.EntityFrameworkCore.Migrations;

namespace UnicornInsurance.Data.Migrations
{
    public partial class ChangeCascadeDelete : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MobileSuits_Weapons_CustomWeaponId",
                table: "MobileSuits");

            migrationBuilder.AddForeignKey(
                name: "FK_MobileSuits_Weapons_CustomWeaponId",
                table: "MobileSuits",
                column: "CustomWeaponId",
                principalTable: "Weapons",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MobileSuits_Weapons_CustomWeaponId",
                table: "MobileSuits");

            migrationBuilder.AddForeignKey(
                name: "FK_MobileSuits_Weapons_CustomWeaponId",
                table: "MobileSuits",
                column: "CustomWeaponId",
                principalTable: "Weapons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
