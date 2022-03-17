using Microsoft.EntityFrameworkCore.Migrations;

namespace UnicornInsurance.Data.Migrations
{
    public partial class RefactorSeedsAgain : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Weapons",
                keyColumn: "Id",
                keyValue: 2,
                column: "ImageUrl",
                value: "\\images\\weapons\\BeamSprayGun.webp");

            migrationBuilder.UpdateData(
                table: "Weapons",
                keyColumn: "Id",
                keyValue: 3,
                column: "ImageUrl",
                value: "\\images\\weapons\\BeamSaber.webp");

            migrationBuilder.UpdateData(
                table: "Weapons",
                keyColumn: "Id",
                keyValue: 5,
                column: "ImageUrl",
                value: "\\images\\weapons\\Funnels.jpg");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
                keyValue: 5,
                column: "ImageUrl",
                value: "\\images\\weapons\\Funnels.webp");
        }
    }
}
