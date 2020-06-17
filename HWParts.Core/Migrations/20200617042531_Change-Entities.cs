using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HWParts.Core.Migrations
{
    public partial class ChangeEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Brand",
                table: "TB_STORAGE");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "TB_STORAGE");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "TB_STORAGE");

            migrationBuilder.DropColumn(
                name: "Model",
                table: "TB_STORAGE");

            migrationBuilder.DropColumn(
                name: "Platform",
                table: "TB_STORAGE");

            migrationBuilder.DropColumn(
                name: "PlatformId",
                table: "TB_STORAGE");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "TB_STORAGE");

            migrationBuilder.DropColumn(
                name: "Url",
                table: "TB_STORAGE");

            migrationBuilder.DropColumn(
                name: "Brand",
                table: "TB_PROCESSORS");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "TB_PROCESSORS");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "TB_PROCESSORS");

            migrationBuilder.DropColumn(
                name: "Model",
                table: "TB_PROCESSORS");

            migrationBuilder.DropColumn(
                name: "Platform",
                table: "TB_PROCESSORS");

            migrationBuilder.DropColumn(
                name: "PlatformId",
                table: "TB_PROCESSORS");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "TB_PROCESSORS");

            migrationBuilder.DropColumn(
                name: "Url",
                table: "TB_PROCESSORS");

            migrationBuilder.DropColumn(
                name: "Brand",
                table: "TB_POWER_SUPPLIES");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "TB_POWER_SUPPLIES");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "TB_POWER_SUPPLIES");

            migrationBuilder.DropColumn(
                name: "Model",
                table: "TB_POWER_SUPPLIES");

            migrationBuilder.DropColumn(
                name: "Platform",
                table: "TB_POWER_SUPPLIES");

            migrationBuilder.DropColumn(
                name: "PlatformId",
                table: "TB_POWER_SUPPLIES");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "TB_POWER_SUPPLIES");

            migrationBuilder.DropColumn(
                name: "Url",
                table: "TB_POWER_SUPPLIES");

            migrationBuilder.DropColumn(
                name: "Brand",
                table: "TB_MOTHERBOARDS");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "TB_MOTHERBOARDS");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "TB_MOTHERBOARDS");

            migrationBuilder.DropColumn(
                name: "Model",
                table: "TB_MOTHERBOARDS");

            migrationBuilder.DropColumn(
                name: "Platform",
                table: "TB_MOTHERBOARDS");

            migrationBuilder.DropColumn(
                name: "PlatformId",
                table: "TB_MOTHERBOARDS");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "TB_MOTHERBOARDS");

            migrationBuilder.DropColumn(
                name: "Url",
                table: "TB_MOTHERBOARDS");

            migrationBuilder.DropColumn(
                name: "Brand",
                table: "TB_MEMORIES");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "TB_MEMORIES");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "TB_MEMORIES");

            migrationBuilder.DropColumn(
                name: "Model",
                table: "TB_MEMORIES");

            migrationBuilder.DropColumn(
                name: "Platform",
                table: "TB_MEMORIES");

            migrationBuilder.DropColumn(
                name: "PlatformId",
                table: "TB_MEMORIES");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "TB_MEMORIES");

            migrationBuilder.DropColumn(
                name: "Url",
                table: "TB_MEMORIES");

            migrationBuilder.DropColumn(
                name: "Brand",
                table: "TB_GRAPHICS_CARDS");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "TB_GRAPHICS_CARDS");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "TB_GRAPHICS_CARDS");

            migrationBuilder.DropColumn(
                name: "Model",
                table: "TB_GRAPHICS_CARDS");

            migrationBuilder.DropColumn(
                name: "Platform",
                table: "TB_GRAPHICS_CARDS");

            migrationBuilder.DropColumn(
                name: "PlatformId",
                table: "TB_GRAPHICS_CARDS");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "TB_GRAPHICS_CARDS");

            migrationBuilder.DropColumn(
                name: "Url",
                table: "TB_GRAPHICS_CARDS");

            migrationBuilder.DropColumn(
                name: "Brand",
                table: "TB_CASES");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "TB_CASES");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "TB_CASES");

            migrationBuilder.DropColumn(
                name: "Model",
                table: "TB_CASES");

            migrationBuilder.DropColumn(
                name: "Platform",
                table: "TB_CASES");

            migrationBuilder.DropColumn(
                name: "PlatformId",
                table: "TB_CASES");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "TB_CASES");

            migrationBuilder.DropColumn(
                name: "Url",
                table: "TB_CASES");

            migrationBuilder.CreateTable(
                name: "ComponentBase",
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
                    Platform = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComponentBase", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_TB_CASES_ComponentBase_Id",
                table: "TB_CASES",
                column: "Id",
                principalTable: "ComponentBase",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TB_GRAPHICS_CARDS_ComponentBase_Id",
                table: "TB_GRAPHICS_CARDS",
                column: "Id",
                principalTable: "ComponentBase",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TB_MEMORIES_ComponentBase_Id",
                table: "TB_MEMORIES",
                column: "Id",
                principalTable: "ComponentBase",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TB_MOTHERBOARDS_ComponentBase_Id",
                table: "TB_MOTHERBOARDS",
                column: "Id",
                principalTable: "ComponentBase",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TB_POWER_SUPPLIES_ComponentBase_Id",
                table: "TB_POWER_SUPPLIES",
                column: "Id",
                principalTable: "ComponentBase",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TB_PROCESSORS_ComponentBase_Id",
                table: "TB_PROCESSORS",
                column: "Id",
                principalTable: "ComponentBase",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TB_STORAGE_ComponentBase_Id",
                table: "TB_STORAGE",
                column: "Id",
                principalTable: "ComponentBase",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TB_CASES_ComponentBase_Id",
                table: "TB_CASES");

            migrationBuilder.DropForeignKey(
                name: "FK_TB_GRAPHICS_CARDS_ComponentBase_Id",
                table: "TB_GRAPHICS_CARDS");

            migrationBuilder.DropForeignKey(
                name: "FK_TB_MEMORIES_ComponentBase_Id",
                table: "TB_MEMORIES");

            migrationBuilder.DropForeignKey(
                name: "FK_TB_MOTHERBOARDS_ComponentBase_Id",
                table: "TB_MOTHERBOARDS");

            migrationBuilder.DropForeignKey(
                name: "FK_TB_POWER_SUPPLIES_ComponentBase_Id",
                table: "TB_POWER_SUPPLIES");

            migrationBuilder.DropForeignKey(
                name: "FK_TB_PROCESSORS_ComponentBase_Id",
                table: "TB_PROCESSORS");

            migrationBuilder.DropForeignKey(
                name: "FK_TB_STORAGE_ComponentBase_Id",
                table: "TB_STORAGE");

            migrationBuilder.DropTable(
                name: "ComponentBase");

            migrationBuilder.AddColumn<string>(
                name: "Brand",
                table: "TB_STORAGE",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "TB_STORAGE",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "TB_STORAGE",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Model",
                table: "TB_STORAGE",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Platform",
                table: "TB_STORAGE",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PlatformId",
                table: "TB_STORAGE",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "TB_STORAGE",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Url",
                table: "TB_STORAGE",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Brand",
                table: "TB_PROCESSORS",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "TB_PROCESSORS",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "TB_PROCESSORS",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Model",
                table: "TB_PROCESSORS",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Platform",
                table: "TB_PROCESSORS",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PlatformId",
                table: "TB_PROCESSORS",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "TB_PROCESSORS",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Url",
                table: "TB_PROCESSORS",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Brand",
                table: "TB_POWER_SUPPLIES",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "TB_POWER_SUPPLIES",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "TB_POWER_SUPPLIES",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Model",
                table: "TB_POWER_SUPPLIES",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Platform",
                table: "TB_POWER_SUPPLIES",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PlatformId",
                table: "TB_POWER_SUPPLIES",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "TB_POWER_SUPPLIES",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Url",
                table: "TB_POWER_SUPPLIES",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Brand",
                table: "TB_MOTHERBOARDS",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "TB_MOTHERBOARDS",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "TB_MOTHERBOARDS",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Model",
                table: "TB_MOTHERBOARDS",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Platform",
                table: "TB_MOTHERBOARDS",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PlatformId",
                table: "TB_MOTHERBOARDS",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "TB_MOTHERBOARDS",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Url",
                table: "TB_MOTHERBOARDS",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Brand",
                table: "TB_MEMORIES",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "TB_MEMORIES",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "TB_MEMORIES",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Model",
                table: "TB_MEMORIES",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Platform",
                table: "TB_MEMORIES",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PlatformId",
                table: "TB_MEMORIES",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "TB_MEMORIES",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Url",
                table: "TB_MEMORIES",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Brand",
                table: "TB_GRAPHICS_CARDS",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "TB_GRAPHICS_CARDS",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "TB_GRAPHICS_CARDS",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Model",
                table: "TB_GRAPHICS_CARDS",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Platform",
                table: "TB_GRAPHICS_CARDS",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PlatformId",
                table: "TB_GRAPHICS_CARDS",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "TB_GRAPHICS_CARDS",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Url",
                table: "TB_GRAPHICS_CARDS",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Brand",
                table: "TB_CASES",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "TB_CASES",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "TB_CASES",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Model",
                table: "TB_CASES",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Platform",
                table: "TB_CASES",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PlatformId",
                table: "TB_CASES",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "TB_CASES",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Url",
                table: "TB_CASES",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
