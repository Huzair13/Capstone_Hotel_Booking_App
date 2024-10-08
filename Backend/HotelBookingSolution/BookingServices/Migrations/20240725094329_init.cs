﻿//using System;
//using Microsoft.EntityFrameworkCore.Migrations;

//#nullable disable

//namespace BookingServices.Migrations
//{
//    public partial class init : Migration
//    {
//        protected override void Up(MigrationBuilder migrationBuilder)
//        {
//            migrationBuilder.CreateTable(
//                name: "Bookings",
//                columns: table => new
//                {
//                    Id = table.Column<int>(type: "int", nullable: false)
//                        .Annotation("SqlServer:Identity", "1, 1"),
//                    HotelId = table.Column<int>(type: "int", nullable: false),
//                    UserId = table.Column<int>(type: "int", nullable: false),
//                    CheckInDate = table.Column<DateTime>(type: "datetime2", nullable: false),
//                    CheckOutDate = table.Column<DateTime>(type: "datetime2", nullable: false),
//                    NumberOfGuests = table.Column<int>(type: "int", nullable: false),
//                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
//                    IsPaid = table.Column<bool>(type: "bit", nullable: false),
//                    IsCancelled = table.Column<bool>(type: "bit", nullable: false)
//                },
//                constraints: table =>
//                {
//                    table.PrimaryKey("PK_Bookings", x => x.Id);
//                });

//            migrationBuilder.CreateTable(
//                name: "BookingDetail",
//                columns: table => new
//                {
//                    Id = table.Column<int>(type: "int", nullable: false)
//                        .Annotation("SqlServer:Identity", "1, 1"),
//                    BookingId = table.Column<int>(type: "int", nullable: false),
//                    RoomNumber = table.Column<int>(type: "int", nullable: false),
//                    Rent = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
//                    HotelId = table.Column<int>(type: "int", nullable: false)
//                },
//                constraints: table =>
//                {
//                    table.PrimaryKey("PK_BookingDetail", x => x.Id);
//                    table.ForeignKey(
//                        name: "FK_BookingDetail_Bookings_BookingId",
//                        column: x => x.BookingId,
//                        principalTable: "Bookings",
//                        principalColumn: "Id",
//                        onDelete: ReferentialAction.Cascade);
//                });

//            migrationBuilder.CreateIndex(
//                name: "IX_BookingDetail_BookingId",
//                table: "BookingDetail",
//                column: "BookingId");
//        }

//        protected override void Down(MigrationBuilder migrationBuilder)
//        {
//            migrationBuilder.DropTable(
//                name: "BookingDetail");

//            migrationBuilder.DropTable(
//                name: "Bookings");
//        }
//    }
//}
