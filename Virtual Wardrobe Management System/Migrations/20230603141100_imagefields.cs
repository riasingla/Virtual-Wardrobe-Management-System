using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VIRTUAL_WARDROBE_MANAGEMENT_SYSTEM.Migrations
{
    public partial class imagefields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Item1",
                table: "Outfits",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Item2",
                table: "Outfits",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Item1",
                table: "Outfits");

            migrationBuilder.DropColumn(
                name: "Item2",
                table: "Outfits");
        }
    }
}
