using Microsoft.EntityFrameworkCore.Migrations;

namespace HWParts.Core.Migrations
{
    public partial class UpdateMemoryEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CasLatency",
                table: "TB_MEMORIES");

            migrationBuilder.DropColumn(
                name: "HeatSpreader",
                table: "TB_MEMORIES");

            migrationBuilder.DropColumn(
                name: "Item",
                table: "TB_MEMORIES");

            migrationBuilder.DropColumn(
                name: "MultiChannelKit",
                table: "TB_MEMORIES");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "TB_MEMORIES");

            migrationBuilder.DropColumn(
                name: "Order",
                table: "TB_MEMORIES");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "TB_MEMORIES");

            migrationBuilder.DropColumn(
                name: "Timing",
                table: "TB_MEMORIES");

            migrationBuilder.DropColumn(
                name: "Voltage",
                table: "TB_MEMORIES");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CasLatency",
                table: "TB_MEMORIES",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HeatSpreader",
                table: "TB_MEMORIES",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Item",
                table: "TB_MEMORIES",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MultiChannelKit",
                table: "TB_MEMORIES",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "TB_MEMORIES",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Order",
                table: "TB_MEMORIES",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "TB_MEMORIES",
                type: "decimal(10, 4)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "Timing",
                table: "TB_MEMORIES",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Voltage",
                table: "TB_MEMORIES",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
