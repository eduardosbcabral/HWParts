using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HWParts.Core.Migrations
{
    public partial class NewDataProcessorEntity : Migration
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

            migrationBuilder.AddColumn<string>(
                name: "AudioChannels",
                table: "TB_PROCESSORS",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AudioChipset",
                table: "TB_PROCESSORS",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BackIOPorts",
                table: "TB_PROCESSORS",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ChannelSupported",
                table: "TB_PROCESSORS",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Chipset",
                table: "TB_PROCESSORS",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CpuSocketType",
                table: "TB_PROCESSORS",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CpuType",
                table: "TB_PROCESSORS",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CrawledAt",
                table: "TB_PROCESSORS",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Dimensions",
                table: "TB_PROCESSORS",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FirstAvailable",
                table: "TB_PROCESSORS",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "FormFactor",
                table: "TB_PROCESSORS",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LanChipset",
                table: "TB_PROCESSORS",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "M2",
                table: "TB_PROCESSORS",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MaxLanSpeed",
                table: "TB_PROCESSORS",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MaximumMemorySupported",
                table: "TB_PROCESSORS",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MemoryStandard",
                table: "TB_PROCESSORS",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NumberOfMemorySlots",
                table: "TB_PROCESSORS",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OnboardUsb",
                table: "TB_PROCESSORS",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OnboardVideoChipset",
                table: "TB_PROCESSORS",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Order",
                table: "TB_PROCESSORS",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "OtherConnectors",
                table: "TB_PROCESSORS",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PciExpress30x16",
                table: "TB_PROCESSORS",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PciExpressx1",
                table: "TB_PROCESSORS",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PowerPin",
                table: "TB_PROCESSORS",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Sata6Gbs",
                table: "TB_PROCESSORS",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AudioChannels",
                table: "TB_PROCESSORS");

            migrationBuilder.DropColumn(
                name: "AudioChipset",
                table: "TB_PROCESSORS");

            migrationBuilder.DropColumn(
                name: "BackIOPorts",
                table: "TB_PROCESSORS");

            migrationBuilder.DropColumn(
                name: "ChannelSupported",
                table: "TB_PROCESSORS");

            migrationBuilder.DropColumn(
                name: "Chipset",
                table: "TB_PROCESSORS");

            migrationBuilder.DropColumn(
                name: "CpuSocketType",
                table: "TB_PROCESSORS");

            migrationBuilder.DropColumn(
                name: "CpuType",
                table: "TB_PROCESSORS");

            migrationBuilder.DropColumn(
                name: "CrawledAt",
                table: "TB_PROCESSORS");

            migrationBuilder.DropColumn(
                name: "Dimensions",
                table: "TB_PROCESSORS");

            migrationBuilder.DropColumn(
                name: "FirstAvailable",
                table: "TB_PROCESSORS");

            migrationBuilder.DropColumn(
                name: "FormFactor",
                table: "TB_PROCESSORS");

            migrationBuilder.DropColumn(
                name: "LanChipset",
                table: "TB_PROCESSORS");

            migrationBuilder.DropColumn(
                name: "M2",
                table: "TB_PROCESSORS");

            migrationBuilder.DropColumn(
                name: "MaxLanSpeed",
                table: "TB_PROCESSORS");

            migrationBuilder.DropColumn(
                name: "MaximumMemorySupported",
                table: "TB_PROCESSORS");

            migrationBuilder.DropColumn(
                name: "MemoryStandard",
                table: "TB_PROCESSORS");

            migrationBuilder.DropColumn(
                name: "NumberOfMemorySlots",
                table: "TB_PROCESSORS");

            migrationBuilder.DropColumn(
                name: "OnboardUsb",
                table: "TB_PROCESSORS");

            migrationBuilder.DropColumn(
                name: "OnboardVideoChipset",
                table: "TB_PROCESSORS");

            migrationBuilder.DropColumn(
                name: "Order",
                table: "TB_PROCESSORS");

            migrationBuilder.DropColumn(
                name: "OtherConnectors",
                table: "TB_PROCESSORS");

            migrationBuilder.DropColumn(
                name: "PciExpress30x16",
                table: "TB_PROCESSORS");

            migrationBuilder.DropColumn(
                name: "PciExpressx1",
                table: "TB_PROCESSORS");

            migrationBuilder.DropColumn(
                name: "PowerPin",
                table: "TB_PROCESSORS");

            migrationBuilder.DropColumn(
                name: "Sata6Gbs",
                table: "TB_PROCESSORS");

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
