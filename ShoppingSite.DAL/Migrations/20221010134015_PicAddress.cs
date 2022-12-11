using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShoppingSite.DAL.Migrations
{
    public partial class PicAddress : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PictureFullAddress",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PictureFullAddress",
                table: "Products");
        }
    }
}
