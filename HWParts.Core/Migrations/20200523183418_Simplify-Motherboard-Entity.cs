using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HWParts.Core.Migrations
{
    public partial class SimplifyMotherboardEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AudioChannels",
                table: "TB_MOTHERBOARDS");

            migrationBuilder.DropColumn(
                name: "AudioChipset",
                table: "TB_MOTHERBOARDS");

            migrationBuilder.DropColumn(
                name: "BackIOPorts",
                table: "TB_MOTHERBOARDS");

            migrationBuilder.DropColumn(
                name: "ChannelSupported",
                table: "TB_MOTHERBOARDS");

            migrationBuilder.DropColumn(
                name: "Chipset",
                table: "TB_MOTHERBOARDS");

            migrationBuilder.DropColumn(
                name: "CpuSocketType",
                table: "TB_MOTHERBOARDS");

            migrationBuilder.DropColumn(
                name: "CpuType",
                table: "TB_MOTHERBOARDS");

            migrationBuilder.DropColumn(
                name: "CrawledAt",
                table: "TB_MOTHERBOARDS");

            migrationBuilder.DropColumn(
                name: "Dimensions",
                table: "TB_MOTHERBOARDS");

            migrationBuilder.DropColumn(
                name: "FirstAvailable",
                table: "TB_MOTHERBOARDS");

            migrationBuilder.DropColumn(
                name: "FormFactor",
                table: "TB_MOTHERBOARDS");

            migrationBuilder.DropColumn(
                name: "LanChipset",
                table: "TB_MOTHERBOARDS");

            migrationBuilder.DropColumn(
                name: "M2",
                table: "TB_MOTHERBOARDS");

            migrationBuilder.DropColumn(
                name: "MaxLanSpeed",
                table: "TB_MOTHERBOARDS");

            migrationBuilder.DropColumn(
                name: "MaximumMemorySupported",
                table: "TB_MOTHERBOARDS");

            migrationBuilder.DropColumn(
                name: "MemoryStandard",
                table: "TB_MOTHERBOARDS");

            migrationBuilder.DropColumn(
                name: "NumberOfMemorySlots",
                table: "TB_MOTHERBOARDS");

            migrationBuilder.DropColumn(
                name: "OnboardUsb",
                table: "TB_MOTHERBOARDS");

            migrationBuilder.DropColumn(
                name: "OnboardVideoChipset",
                table: "TB_MOTHERBOARDS");

            migrationBuilder.DropColumn(
                name: "OtherConnectors",
                table: "TB_MOTHERBOARDS");

            migrationBuilder.DropColumn(
                name: "PciExpress30x16",
                table: "TB_MOTHERBOARDS");

            migrationBuilder.DropColumn(
                name: "PciExpressx1",
                table: "TB_MOTHERBOARDS");

            migrationBuilder.DropColumn(
                name: "PowerPin",
                table: "TB_MOTHERBOARDS");

            migrationBuilder.DropColumn(
                name: "Sata6Gbs",
                table: "TB_MOTHERBOARDS");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AudioChannels",
                table: "TB_MOTHERBOARDS",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AudioChipset",
                table: "TB_MOTHERBOARDS",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BackIOPorts",
                table: "TB_MOTHERBOARDS",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ChannelSupported",
                table: "TB_MOTHERBOARDS",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Chipset",
                table: "TB_MOTHERBOARDS",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CpuSocketType",
                table: "TB_MOTHERBOARDS",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CpuType",
                table: "TB_MOTHERBOARDS",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CrawledAt",
                table: "TB_MOTHERBOARDS",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Dimensions",
                table: "TB_MOTHERBOARDS",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FirstAvailable",
                table: "TB_MOTHERBOARDS",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "FormFactor",
                table: "TB_MOTHERBOARDS",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LanChipset",
                table: "TB_MOTHERBOARDS",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "M2",
                table: "TB_MOTHERBOARDS",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MaxLanSpeed",
                table: "TB_MOTHERBOARDS",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MaximumMemorySupported",
                table: "TB_MOTHERBOARDS",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MemoryStandard",
                table: "TB_MOTHERBOARDS",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NumberOfMemorySlots",
                table: "TB_MOTHERBOARDS",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OnboardUsb",
                table: "TB_MOTHERBOARDS",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OnboardVideoChipset",
                table: "TB_MOTHERBOARDS",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OtherConnectors",
                table: "TB_MOTHERBOARDS",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PciExpress30x16",
                table: "TB_MOTHERBOARDS",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PciExpressx1",
                table: "TB_MOTHERBOARDS",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PowerPin",
                table: "TB_MOTHERBOARDS",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Sata6Gbs",
                table: "TB_MOTHERBOARDS",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
