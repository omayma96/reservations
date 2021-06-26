using Microsoft.EntityFrameworkCore.Migrations;

namespace reservation_system.Migrations
{
    public partial class reservationAbs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "absentcount",
                table: "ReservationViewmodel",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "absentcount",
                table: "ReservationViewmodel");
        }
    }
}
