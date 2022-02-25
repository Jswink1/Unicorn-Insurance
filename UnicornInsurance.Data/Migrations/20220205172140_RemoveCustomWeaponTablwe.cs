using Microsoft.EntityFrameworkCore.Migrations;

namespace UnicornInsurance.Data.Migrations
{
    public partial class RemoveCustomWeaponTablwe : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CustomWeapons");

            migrationBuilder.DropColumn(
                name: "HasCustomWeapon",
                table: "MobileSuits");

            migrationBuilder.AddColumn<int>(
                name: "CustomWeaponId",
                table: "MobileSuits",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "MobileSuits",
                keyColumn: "Id",
                keyValue: 1,
                column: "CustomWeaponId",
                value: 6);

            migrationBuilder.UpdateData(
                table: "MobileSuits",
                keyColumn: "Id",
                keyValue: 2,
                column: "CustomWeaponId",
                value: 7);

            migrationBuilder.UpdateData(
                table: "MobileSuits",
                keyColumn: "Id",
                keyValue: 3,
                column: "CustomWeaponId",
                value: 8);

            migrationBuilder.UpdateData(
                table: "MobileSuits",
                keyColumn: "Id",
                keyValue: 7,
                column: "CustomWeaponId",
                value: 9);

            migrationBuilder.CreateIndex(
                name: "IX_MobileSuits_CustomWeaponId",
                table: "MobileSuits",
                column: "CustomWeaponId",
                unique: true,
                filter: "[CustomWeaponId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_MobileSuits_Weapons_CustomWeaponId",
                table: "MobileSuits",
                column: "CustomWeaponId",
                principalTable: "Weapons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MobileSuits_Weapons_CustomWeaponId",
                table: "MobileSuits");

            migrationBuilder.DropIndex(
                name: "IX_MobileSuits_CustomWeaponId",
                table: "MobileSuits");

            migrationBuilder.DropColumn(
                name: "CustomWeaponId",
                table: "MobileSuits");

            migrationBuilder.AddColumn<bool>(
                name: "HasCustomWeapon",
                table: "MobileSuits",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "CustomWeapons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MobileSuitId = table.Column<int>(type: "int", nullable: false),
                    WeaponId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomWeapons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomWeapons_ToMobileSuits",
                        column: x => x.MobileSuitId,
                        principalTable: "MobileSuits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CustomWeapons_ToWeapons",
                        column: x => x.WeaponId,
                        principalTable: "Weapons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "CustomWeapons",
                columns: new[] { "Id", "MobileSuitId", "WeaponId" },
                values: new object[,]
                {
                    { 1, 1, 6 },
                    { 2, 2, 7 },
                    { 3, 3, 8 },
                    { 4, 7, 9 }
                });

            migrationBuilder.UpdateData(
                table: "MobileSuits",
                keyColumn: "Id",
                keyValue: 1,
                column: "HasCustomWeapon",
                value: true);

            migrationBuilder.UpdateData(
                table: "MobileSuits",
                keyColumn: "Id",
                keyValue: 2,
                column: "HasCustomWeapon",
                value: true);

            migrationBuilder.UpdateData(
                table: "MobileSuits",
                keyColumn: "Id",
                keyValue: 3,
                column: "HasCustomWeapon",
                value: true);

            migrationBuilder.UpdateData(
                table: "MobileSuits",
                keyColumn: "Id",
                keyValue: 7,
                column: "HasCustomWeapon",
                value: true);

            migrationBuilder.CreateIndex(
                name: "IX_CustomWeapons_MobileSuitId",
                table: "CustomWeapons",
                column: "MobileSuitId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CustomWeapons_MobileSuitId1",
                table: "CustomWeapons",
                column: "MobileSuitId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomWeapons_WeaponId",
                table: "CustomWeapons",
                column: "WeaponId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CustomWeapons_WeaponId1",
                table: "CustomWeapons",
                column: "WeaponId");
        }
    }
}
