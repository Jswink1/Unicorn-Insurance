using Microsoft.EntityFrameworkCore.Migrations;

namespace UnicornInsurance.Identity.Migrations
{
    public partial class AddEmailValidation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EmailValidationToken",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cac43a6e-f7bb-4448-baaf-1add431ccbbf",
                column: "ConcurrencyStamp",
                value: "206c1714-85b2-4422-8752-fb1cec1754de");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "j3kh4b53-dfg7-5354-dfsg-sdfg23g6d3q4",
                column: "ConcurrencyStamp",
                value: "274d9c19-451e-4d4e-85b4-9c54e80523c2");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "078917af-75e0-4f23-b093-111e829f1724", "AQAAAAEAACcQAAAAEPFeW7gdKq68iYzCqhIrnE9E0bHP2IiGdlvc5pXlSP/gSRosxbrRZ7OVvQHez+xyHA==", "34e22499-2366-4f1e-b0e3-ebb2199d82c3" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9e224968-33e4-4652-b7b7-8574d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5e9e86c1-77e6-4a57-841d-0da09f59325c", "AQAAAAEAACcQAAAAELq+SSbroNmElws5hNKC1EQPNzvdYS3Lf6rvGva17XJIC/1Sm9i44xr7B6K4U7SMiw==", "59b289e1-4d2e-414b-b0b5-abf2cb4f308f" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmailValidationToken",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cac43a6e-f7bb-4448-baaf-1add431ccbbf",
                column: "ConcurrencyStamp",
                value: "c456f71e-189e-437b-9861-f622e3440408");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "j3kh4b53-dfg7-5354-dfsg-sdfg23g6d3q4",
                column: "ConcurrencyStamp",
                value: "27c3daa6-bb73-4235-9cbe-cfdb8e57a6de");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "72ef00f1-bcc7-4bb1-ba05-7332fae0bfef", "AQAAAAEAACcQAAAAEFVnIh4FTAXfKVbDYGKLQDyBmtBmz7ukMcBL75xAIafaGxEEcbA0tFeRU+eW56sYHA==", "1b0c42c2-99a5-497b-91e5-51e3207ae7d6" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9e224968-33e4-4652-b7b7-8574d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ac77dd8c-0ce3-4232-82e8-3bd923a14b66", "AQAAAAEAACcQAAAAEIK3FRR3zqIuWbZCzPLbs3kmyekdBhtNmnFOzyvDds2miL7ca1p+8Rzp1G6NF/gWRQ==", "64c9eb71-d0fc-40fa-8543-e682d4478210" });
        }
    }
}
