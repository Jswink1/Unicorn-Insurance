using Microsoft.EntityFrameworkCore.Migrations;

namespace UnicornInsurance.Data.Migrations
{
    public partial class AddUserMobileSuitsAndWeapons : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserMobileSuits",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MobileSuitId = table.Column<int>(type: "int", nullable: false),
                    CustomWeaponId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserMobileSuits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserMobileSuits_MobileSuits_MobileSuitId",
                        column: x => x.MobileSuitId,
                        principalTable: "MobileSuits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserWeapons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsCustomWeapon = table.Column<bool>(type: "bit", nullable: false),
                    WeaponId = table.Column<int>(type: "int", nullable: false),
                    EquippedMobileSuitId = table.Column<int>(type: "int", nullable: true),
                    UserMobileSuitId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserWeapons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserWeapons_UserMobileSuits_EquippedMobileSuitId",
                        column: x => x.EquippedMobileSuitId,
                        principalTable: "UserMobileSuits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserWeapons_UserMobileSuits_UserMobileSuitId",
                        column: x => x.UserMobileSuitId,
                        principalTable: "UserMobileSuits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_UserWeapons_Weapons_WeaponId",
                        column: x => x.WeaponId,
                        principalTable: "Weapons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserMobileSuits_MobileSuitId",
                table: "UserMobileSuits",
                column: "MobileSuitId");

            migrationBuilder.CreateIndex(
                name: "IX_UserWeapons_EquippedMobileSuitId",
                table: "UserWeapons",
                column: "EquippedMobileSuitId");

            migrationBuilder.CreateIndex(
                name: "IX_UserWeapons_UserMobileSuitId",
                table: "UserWeapons",
                column: "UserMobileSuitId",
                unique: true,
                filter: "[UserMobileSuitId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_UserWeapons_WeaponId",
                table: "UserWeapons",
                column: "WeaponId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserWeapons");

            migrationBuilder.DropTable(
                name: "UserMobileSuits");
        }
    }
}
