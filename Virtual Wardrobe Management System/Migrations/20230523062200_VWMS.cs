using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VIRTUAL_WARDROBE_MANAGEMENT_SYSTEM.Migrations
{
    public partial class VWMS : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SearchandFilter",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Colour = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Size = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OutfitName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OutfitIfd = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SearchandFilter", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConfirmPassword = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Outfits",
                columns: table => new
                {
                    OutfitId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OutfitName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OutfitImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsersUserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Outfits", x => x.OutfitId);
                    table.ForeignKey(
                        name: "FK_Outfits_Users_UsersUserId",
                        column: x => x.UsersUserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClothingItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Colour = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Size = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OutfitId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClothingItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClothingItems_Outfits_OutfitId",
                        column: x => x.OutfitId,
                        principalTable: "Outfits",
                        principalColumn: "OutfitId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClothingItems_OutfitId",
                table: "ClothingItems",
                column: "OutfitId");

            migrationBuilder.CreateIndex(
                name: "IX_Outfits_UsersUserId",
                table: "Outfits",
                column: "UsersUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClothingItems");

            migrationBuilder.DropTable(
                name: "SearchandFilter");

            migrationBuilder.DropTable(
                name: "Outfits");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
