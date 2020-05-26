using Microsoft.EntityFrameworkCore.Migrations;

namespace HWParts.Core.Migrations
{
    public partial class UpdateGraphicsCardEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CardDimensions",
                table: "TB_GRAPHICS_CARDS");

            migrationBuilder.DropColumn(
                name: "ChipsetManufacturer",
                table: "TB_GRAPHICS_CARDS");

            migrationBuilder.DropColumn(
                name: "CoreClock",
                table: "TB_GRAPHICS_CARDS");

            migrationBuilder.DropColumn(
                name: "DisplayPort",
                table: "TB_GRAPHICS_CARDS");

            migrationBuilder.DropColumn(
                name: "Dvi",
                table: "TB_GRAPHICS_CARDS");

            migrationBuilder.DropColumn(
                name: "Hdmi",
                table: "TB_GRAPHICS_CARDS");

            migrationBuilder.DropColumn(
                name: "Item",
                table: "TB_GRAPHICS_CARDS");

            migrationBuilder.DropColumn(
                name: "MaxResolution",
                table: "TB_GRAPHICS_CARDS");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "TB_GRAPHICS_CARDS");

            migrationBuilder.DropColumn(
                name: "Order",
                table: "TB_GRAPHICS_CARDS");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "TB_GRAPHICS_CARDS");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CardDimensions",
                table: "TB_GRAPHICS_CARDS",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ChipsetManufacturer",
                table: "TB_GRAPHICS_CARDS",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CoreClock",
                table: "TB_GRAPHICS_CARDS",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DisplayPort",
                table: "TB_GRAPHICS_CARDS",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Dvi",
                table: "TB_GRAPHICS_CARDS",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Hdmi",
                table: "TB_GRAPHICS_CARDS",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Item",
                table: "TB_GRAPHICS_CARDS",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MaxResolution",
                table: "TB_GRAPHICS_CARDS",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "TB_GRAPHICS_CARDS",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Order",
                table: "TB_GRAPHICS_CARDS",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "TB_GRAPHICS_CARDS",
                type: "decimal(10, 4)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
