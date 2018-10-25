using Microsoft.EntityFrameworkCore.Migrations;

namespace DeviceManager.Api.Migrations
{
    public partial class DeviceCode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DeviceCode",
                table: "Devices",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeviceCode",
                table: "Devices");
        }
    }
}
