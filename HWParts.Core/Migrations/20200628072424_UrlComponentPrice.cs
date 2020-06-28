using Microsoft.EntityFrameworkCore.Migrations;

namespace HWParts.Core.Migrations
{
    public partial class UrlComponentPrice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Url",
                table: "TB_COMPONENTS_PRICES",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Url",
                table: "TB_COMPONENTS_PRICES");
        }
    }
}
