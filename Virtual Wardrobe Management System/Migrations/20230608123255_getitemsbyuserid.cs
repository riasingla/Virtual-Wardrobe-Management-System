using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VIRTUAL_WARDROBE_MANAGEMENT_SYSTEM.Migrations
{
    public partial class getitemsbyuserid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "ClothingItems",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "ClothingItems");
        }
    }
}
