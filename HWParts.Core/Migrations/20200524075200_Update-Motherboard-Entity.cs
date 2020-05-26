using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HWParts.Core.Migrations
{
    public partial class UpdateMotherboardEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Order",
                table: "TB_MOTHERBOARDS");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "TB_PROCESSORS",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "TB_MOTHERBOARDS",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "TB_MEMORIES",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "TB_GRAPHICS_CARDS",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "TB_PROCESSORS",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(Guid));

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "TB_MOTHERBOARDS",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(Guid));

            migrationBuilder.AddColumn<int>(
                name: "Order",
                table: "TB_MOTHERBOARDS",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "TB_MEMORIES",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(Guid));

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "TB_GRAPHICS_CARDS",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(Guid));
        }
    }
}
