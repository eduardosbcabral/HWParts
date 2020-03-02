using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HWParts.Core.Migrations
{
    public partial class MotherboardEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TB_MOTHERBOARDS",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: true),
                    PlatformId = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Brand = table.Column<string>(nullable: true),
                    ProcessorBrand = table.Column<string>(nullable: true),
                    NumberOfMemorySlots = table.Column<string>(nullable: true),
                    MemoryStandard = table.Column<string>(nullable: true),
                    OnboardVideoChipset = table.Column<string>(nullable: true),
                    AudioChipset = table.Column<string>(nullable: true),
                    AudioChannels = table.Column<string>(nullable: true),
                    Model = table.Column<string>(nullable: true),
                    Item = table.Column<string>(nullable: true),
                    Price = table.Column<decimal>(type: "decimal(6, 2)", nullable: false),
                    ImageUrl = table.Column<string>(nullable: true),
                    Url = table.Column<string>(nullable: true),
                    Platform = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_MOTHERBOARDS", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TB_MOTHERBOARDS");
        }
    }
}
