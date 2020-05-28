using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HWParts.Core.Migrations
{
    public partial class CreateStorageEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CoolingDevice",
                table: "TB_PROCESSORS");

            migrationBuilder.DropColumn(
                name: "Item",
                table: "TB_PROCESSORS");

            migrationBuilder.DropColumn(
                name: "L2Cache",
                table: "TB_PROCESSORS");

            migrationBuilder.DropColumn(
                name: "L3Cache",
                table: "TB_PROCESSORS");

            migrationBuilder.DropColumn(
                name: "ManufacturingTech",
                table: "TB_PROCESSORS");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "TB_PROCESSORS");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "TB_PROCESSORS");

            migrationBuilder.DropColumn(
                name: "Series",
                table: "TB_PROCESSORS");

            migrationBuilder.CreateTable(
                name: "TB_STORAGE",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: true),
                    Brand = table.Column<string>(nullable: true),
                    Model = table.Column<string>(nullable: true),
                    PlatformId = table.Column<string>(nullable: true),
                    ImageUrl = table.Column<string>(nullable: true),
                    Url = table.Column<string>(nullable: true),
                    Platform = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_STORAGE", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TB_STORAGE");

            migrationBuilder.AddColumn<string>(
                name: "CoolingDevice",
                table: "TB_PROCESSORS",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Item",
                table: "TB_PROCESSORS",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "L2Cache",
                table: "TB_PROCESSORS",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "L3Cache",
                table: "TB_PROCESSORS",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ManufacturingTech",
                table: "TB_PROCESSORS",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "TB_PROCESSORS",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "TB_PROCESSORS",
                type: "decimal(6, 2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "Series",
                table: "TB_PROCESSORS",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
