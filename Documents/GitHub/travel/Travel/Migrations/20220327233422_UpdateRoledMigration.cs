using Microsoft.EntityFrameworkCore.Migrations;

namespace Travel.Migrations
{
    public partial class UpdateRoledMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Roles");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Roles",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true);
        }
    }
}
