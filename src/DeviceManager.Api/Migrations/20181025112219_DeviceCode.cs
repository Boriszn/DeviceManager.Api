using Microsoft.EntityFrameworkCore.Migrations;

namespace DeviceManager.Api.Migrations
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
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
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
