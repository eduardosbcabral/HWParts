using Microsoft.EntityFrameworkCore.Migrations;

namespace HWParts.Core.Migrations
{
    public partial class UpdateComponentsTableName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Components",
                table: "Components");

            migrationBuilder.RenameTable(
                name: "Components",
                newName: "TB_COMPONENTS");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TB_COMPONENTS",
                table: "TB_COMPONENTS",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_TB_COMPONENTS",
                table: "TB_COMPONENTS");

            migrationBuilder.RenameTable(
                name: "TB_COMPONENTS",
                newName: "Components");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Components",
                table: "Components",
                column: "Id");
        }
    }
}
