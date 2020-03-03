using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HWParts.Core.Migrations
{
    public partial class GraphicsCardEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TB_GRAPHICS_CARDS",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: true),
                    PlatformId = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Brand = table.Column<string>(nullable: true),
                    Order = table.Column<int>(nullable: false),
                    ChipsetManufacturer = table.Column<string>(nullable: true),
                    CoreClock = table.Column<string>(nullable: true),
                    MaxResolution = table.Column<string>(nullable: true),
                    DisplayPort = table.Column<string>(nullable: true),
                    Hdmi = table.Column<string>(nullable: true),
                    Dvi = table.Column<string>(nullable: true),
                    CardDimensions = table.Column<string>(nullable: true),
                    Model = table.Column<string>(nullable: true),
                    Item = table.Column<string>(nullable: true),
                    Price = table.Column<decimal>(type: "decimal(10, 4)", nullable: false),
                    ImageUrl = table.Column<string>(nullable: true),
                    Url = table.Column<string>(nullable: true),
                    Platform = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_GRAPHICS_CARDS", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TB_GRAPHICS_CARDS");
        }
    }
}
