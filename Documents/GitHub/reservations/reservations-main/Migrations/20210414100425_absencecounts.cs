using Microsoft.EntityFrameworkCore.Migrations;

namespace reservation_system.Migrations
{
    public partial class absencecounts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "studentId",
                table: "ReservationViewmodel",
                type: "varchar(255) CHARACTER SET utf8mb4",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ReservationViewmodel_studentId",
                table: "ReservationViewmodel",
                column: "studentId");

            migrationBuilder.AddForeignKey(
                name: "FK_ReservationViewmodel_AspNetUsers_studentId",
                table: "ReservationViewmodel",
                column: "studentId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReservationViewmodel_AspNetUsers_studentId",
                table: "ReservationViewmodel");

            migrationBuilder.DropIndex(
                name: "IX_ReservationViewmodel_studentId",
                table: "ReservationViewmodel");

            migrationBuilder.DropColumn(
                name: "studentId",
                table: "ReservationViewmodel");
        }
    }
}
