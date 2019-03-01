using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DeviceManager.Api.Migrations
{
    public partial class DeviceGroups : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DeviceGroups",
                columns: table => new
                {
                    DeviceGroupId = table.Column<Guid>(nullable: false),
                    Company = table.Column<string>(nullable: true),
                    OperatingSystem = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviceGroups", x => x.DeviceGroupId);
                });

            migrationBuilder.CreateTable(
                name: "Devices",
                columns: table => new
                {
                    DeviceId = table.Column<Guid>(nullable: false),
                    DeviceTitle = table.Column<string>(nullable: true),
                    DeviceCode = table.Column<string>(nullable: true),
                    DeviceGroupId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Devices", x => x.DeviceId);
                    table.ForeignKey(
                        name: "FK_Devices_DeviceGroups_DeviceGroupId",
                        column: x => x.DeviceGroupId,
                        principalTable: "DeviceGroups",
                        principalColumn: "DeviceGroupId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "DeviceGroups",
                columns: new[] { "DeviceGroupId", "Company", "OperatingSystem" },
                values: new object[] { new Guid("843a92af-9174-49a3-a2e7-08f99919d6ca"), "Microsoft", "Windows 10" });

            migrationBuilder.InsertData(
                table: "Devices",
                columns: new[] { "DeviceId", "DeviceCode", "DeviceGroupId", "DeviceTitle" },
                values: new object[] { new Guid("1ee62dd5-d698-4e67-a260-f5a66f86f0df"), "Surface568", new Guid("843a92af-9174-49a3-a2e7-08f99919d6ca"), "Surface Tablet" });

            migrationBuilder.InsertData(
                table: "Devices",
                columns: new[] { "DeviceId", "DeviceCode", "DeviceGroupId", "DeviceTitle" },
                values: new object[] { new Guid("9b34ae90-f226-43df-8ad0-7cfdce2f16a7"), "Xbox1234", new Guid("843a92af-9174-49a3-a2e7-08f99919d6ca"), "X Box" });

            migrationBuilder.CreateIndex(
                name: "IX_Devices_DeviceGroupId",
                table: "Devices",
                column: "DeviceGroupId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Devices");

            migrationBuilder.DropTable(
                name: "DeviceGroups");
        }
    }
}
