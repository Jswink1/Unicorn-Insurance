using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace UnicornInsurance.Data.Migrations
{
    public partial class AddUserMobileSuitInsurance : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "EndOfCoverage",
                table: "UserMobileSuits",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "InsurancePlan",
                table: "UserMobileSuits",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndOfCoverage",
                table: "UserMobileSuits");

            migrationBuilder.DropColumn(
                name: "InsurancePlan",
                table: "UserMobileSuits");
        }
    }
}
