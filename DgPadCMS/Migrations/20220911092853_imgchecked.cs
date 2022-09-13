using Microsoft.EntityFrameworkCore.Migrations;

namespace DgPadCMS.Migrations
{
    public partial class imgchecked : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "ImgChecked",
                table: "postTypes",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImgChecked",
                table: "postTypes");
        }
    }
}
