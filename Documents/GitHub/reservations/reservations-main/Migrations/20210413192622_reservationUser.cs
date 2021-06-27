using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace reservation_system.Migrations
{
    public partial class reservationUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ReservationsType",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    type = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    nbrPlace = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReservationsType", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Reservations",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Status = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    ReservationUserId = table.Column<string>(type: "varchar(255) CHARACTER SET utf8mb4", nullable: true),
                    TypeReservationid = table.Column<int>(type: "int", nullable: true),
                    reservationId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservations", x => x.id);
                    table.ForeignKey(
                        name: "FK_Reservations_AspNetUsers_ReservationUserId",
                        column: x => x.ReservationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Reservations_ReservationsType_TypeReservationid",
                        column: x => x.TypeReservationid,
                        principalTable: "ReservationsType",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ReservationViewmodel",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Fname = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    Lname = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    Classe = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    Typeid = table.Column<int>(type: "int", nullable: true),
                    Status = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReservationViewmodel", x => x.id);
                    table.ForeignKey(
                        name: "FK_ReservationViewmodel_ReservationsType_Typeid",
                        column: x => x.Typeid,
                        principalTable: "ReservationsType",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_ReservationUserId",
                table: "Reservations",
                column: "ReservationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_TypeReservationid",
                table: "Reservations",
                column: "TypeReservationid");

            migrationBuilder.CreateIndex(
                name: "IX_ReservationViewmodel_Typeid",
                table: "ReservationViewmodel",
                column: "Typeid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reservations");

            migrationBuilder.DropTable(
                name: "ReservationViewmodel");

            migrationBuilder.DropTable(
                name: "ReservationsType");
        }
    }
}
