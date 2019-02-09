using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace DeviceManager.Api.Migrations
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public partial class DeviceGroup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "DeviceGroupId",
                table: "Devices",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "DeviceGroup",
                columns: table => new
                {
                    DeviceGroupId = table.Column<Guid>(nullable: false),
                    Company = table.Column<string>(nullable: true),
                    OperatingSystem = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviceGroup", x => x.DeviceGroupId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Devices_DeviceGroupId",
                table: "Devices",
                column: "DeviceGroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_Devices_DeviceGroup_DeviceGroupId",
                table: "Devices",
                column: "DeviceGroupId",
                principalTable: "DeviceGroup",
                principalColumn: "DeviceGroupId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Devices_DeviceGroup_DeviceGroupId",
                table: "Devices");

            migrationBuilder.DropTable(
                name: "DeviceGroup");

            migrationBuilder.DropIndex(
                name: "IX_Devices_DeviceGroupId",
                table: "Devices");

            migrationBuilder.DropColumn(
                name: "DeviceGroupId",
                table: "Devices");
        }

#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    }
}
