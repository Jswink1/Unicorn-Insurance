using Microsoft.EntityFrameworkCore.Migrations;

namespace UnicornInsurance.Identity.Migrations
{
    public partial class FixUserSeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                columns: new[] { "ConcurrencyStamp", "NormalizedUserName", "PasswordHash", "SecurityStamp" },
                values: new object[] { "72ef00f1-bcc7-4bb1-ba05-7332fae0bfef", "ADMIN@USER.COM", "AQAAAAEAACcQAAAAEFVnIh4FTAXfKVbDYGKLQDyBmtBmz7ukMcBL75xAIafaGxEEcbA0tFeRU+eW56sYHA==", "1b0c42c2-99a5-497b-91e5-51e3207ae7d6" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9e224968-33e4-4652-b7b7-8574d048cdb9",
                columns: new[] { "ConcurrencyStamp", "NormalizedUserName", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ac77dd8c-0ce3-4232-82e8-3bd923a14b66", "USER@USER.COM", "AQAAAAEAACcQAAAAEIK3FRR3zqIuWbZCzPLbs3kmyekdBhtNmnFOzyvDds2miL7ca1p+8Rzp1G6NF/gWRQ==", "64c9eb71-d0fc-40fa-8543-e682d4478210" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cac43a6e-f7bb-4448-baaf-1add431ccbbf",
                column: "ConcurrencyStamp",
                value: "ac901753-b276-434d-956b-2d2dfd822cbb");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "j3kh4b53-dfg7-5354-dfsg-sdfg23g6d3q4",
                column: "ConcurrencyStamp",
                value: "2542bc16-b90a-4878-9479-43b4bde66e2a");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "NormalizedUserName", "PasswordHash", "SecurityStamp" },
                values: new object[] { "74db6912-29dc-4e16-8ea7-642fd3d1284a", "ADMIN@LOCALHOST.COM", "AQAAAAEAACcQAAAAEGIZVcscxJj53oNJaaZkYaEKMmy3DLX1aAoADAtqhR+8+qug3cYm30YYwVOJxEjrDA==", "ad81d569-2bc3-4c75-aa3a-dc6b72bc63ea" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9e224968-33e4-4652-b7b7-8574d048cdb9",
                columns: new[] { "ConcurrencyStamp", "NormalizedUserName", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7b3135af-aa35-460b-a35f-c8d69f6299a2", "USER@LOCALHOST.COM", "AQAAAAEAACcQAAAAENMBJKYtj22OIqRSoS+vYRFbG8nIpwtS+FGQcMObfaipF2PUQ8MrqJJIy3nFSO8OFg==", "98ba3ffe-1821-4f90-bd24-64ea75c210fd" });
        }
    }
}
