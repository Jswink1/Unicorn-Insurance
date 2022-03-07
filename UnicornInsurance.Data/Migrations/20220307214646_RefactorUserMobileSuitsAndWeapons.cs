using Microsoft.EntityFrameworkCore.Migrations;

namespace UnicornInsurance.Data.Migrations
{
    public partial class RefactorUserMobileSuitsAndWeapons : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserWeapons_UserMobileSuits_UserMobileSuitId",
                table: "UserWeapons");

            migrationBuilder.DropIndex(
                name: "IX_UserWeapons_UserMobileSuitId",
                table: "UserWeapons");

            migrationBuilder.DropColumn(
                name: "UserMobileSuitId",
                table: "UserWeapons");

            migrationBuilder.DropColumn(
                name: "CustomWeaponId",
                table: "UserMobileSuits");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserMobileSuitId",
                table: "UserWeapons",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CustomWeaponId",
                table: "UserMobileSuits",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserWeapons_UserMobileSuitId",
                table: "UserWeapons",
                column: "UserMobileSuitId",
                unique: true,
                filter: "[UserMobileSuitId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_UserWeapons_UserMobileSuits_UserMobileSuitId",
                table: "UserWeapons",
                column: "UserMobileSuitId",
                principalTable: "UserMobileSuits",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
