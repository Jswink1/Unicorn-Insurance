using Microsoft.EntityFrameworkCore.Migrations;

namespace UnicornInsurance.Data.Migrations
{
    public partial class AddForeignKeyPropertyToProducts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_WeaponCartItems_WeaponId",
                table: "WeaponCartItems",
                column: "WeaponId");

            migrationBuilder.CreateIndex(
                name: "IX_MobileSuitCartItems_MobileSuitId",
                table: "MobileSuitCartItems",
                column: "MobileSuitId");

            migrationBuilder.AddForeignKey(
                name: "FK_MobileSuitCartItems_MobileSuits_MobileSuitId",
                table: "MobileSuitCartItems",
                column: "MobileSuitId",
                principalTable: "MobileSuits",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WeaponCartItems_Weapons_WeaponId",
                table: "WeaponCartItems",
                column: "WeaponId",
                principalTable: "Weapons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MobileSuitCartItems_MobileSuits_MobileSuitId",
                table: "MobileSuitCartItems");

            migrationBuilder.DropForeignKey(
                name: "FK_WeaponCartItems_Weapons_WeaponId",
                table: "WeaponCartItems");

            migrationBuilder.DropIndex(
                name: "IX_WeaponCartItems_WeaponId",
                table: "WeaponCartItems");

            migrationBuilder.DropIndex(
                name: "IX_MobileSuitCartItems_MobileSuitId",
                table: "MobileSuitCartItems");
        }
    }
}
