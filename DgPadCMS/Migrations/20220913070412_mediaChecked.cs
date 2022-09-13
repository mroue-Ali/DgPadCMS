using Microsoft.EntityFrameworkCore.Migrations;

namespace DgPadCMS.Migrations
{
    public partial class mediaChecked : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "MediaChecked",
                table: "postTypes",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MediaChecked",
                table: "postTypes");
        }
    }
}
