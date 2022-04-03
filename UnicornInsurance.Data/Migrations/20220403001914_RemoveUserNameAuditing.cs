using Microsoft.EntityFrameworkCore.Migrations;

namespace UnicornInsurance.Data.Migrations
{
    public partial class RemoveUserNameAuditing : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Weapons");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                table: "Weapons");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "MobileSuits");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                table: "MobileSuits");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Deployments");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                table: "Deployments");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Weapons",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                table: "Weapons",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "MobileSuits",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                table: "MobileSuits",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Deployments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                table: "Deployments",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
