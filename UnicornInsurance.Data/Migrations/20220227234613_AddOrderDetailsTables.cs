using Microsoft.EntityFrameworkCore.Migrations;

namespace UnicornInsurance.Data.Migrations
{
    public partial class AddOrderDetailsTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MobileSuitPurchases",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    MobileSuitId = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "money", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MobileSuitPurchases", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MobileSuitPurchases_MobileSuits_MobileSuitId",
                        column: x => x.MobileSuitId,
                        principalTable: "MobileSuits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MobileSuitPurchases_OrderHeaders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "OrderHeaders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WeaponPurchases",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    WeaponId = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "money", nullable: false),
                    Count = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeaponPurchases", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WeaponPurchases_OrderHeaders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "OrderHeaders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WeaponPurchases_Weapons_WeaponId",
                        column: x => x.WeaponId,
                        principalTable: "Weapons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MobileSuitPurchases_MobileSuitId",
                table: "MobileSuitPurchases",
                column: "MobileSuitId");

            migrationBuilder.CreateIndex(
                name: "IX_MobileSuitPurchases_OrderId",
                table: "MobileSuitPurchases",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_WeaponPurchases_OrderId",
                table: "WeaponPurchases",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_WeaponPurchases_WeaponId",
                table: "WeaponPurchases",
                column: "WeaponId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MobileSuitPurchases");

            migrationBuilder.DropTable(
                name: "WeaponPurchases");
        }
    }
}
