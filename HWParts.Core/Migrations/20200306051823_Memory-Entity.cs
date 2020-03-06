using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HWParts.Core.Migrations
{
    public partial class MemoryEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TB_MEMORIES",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: true),
                    PlatformId = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Brand = table.Column<string>(nullable: true),
                    Order = table.Column<int>(nullable: false),
                    CasLatency = table.Column<string>(nullable: true),
                    Voltage = table.Column<string>(nullable: true),
                    MultiChannelKit = table.Column<string>(nullable: true),
                    Timing = table.Column<string>(nullable: true),
                    HeatSpreader = table.Column<string>(nullable: true),
                    Model = table.Column<string>(nullable: true),
                    Item = table.Column<string>(nullable: true),
                    Price = table.Column<decimal>(type: "decimal(10, 4)", nullable: false),
                    ImageUrl = table.Column<string>(nullable: true),
                    Url = table.Column<string>(nullable: true),
                    Platform = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_MEMORIES", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TB_MEMORIES");
        }
    }
}
