using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlazorVehicleReservations.API.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Client",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    DOB = table.Column<DateTime>(type: "date", nullable: true),
                    Gender = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Country = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Client", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Vehicle",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Manufacturer = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Model = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Type = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Color = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Year = table.Column<short>(type: "smallint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehicle", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Reservation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientId = table.Column<int>(type: "int", nullable: false),
                    VehicleId = table.Column<int>(type: "int", nullable: false),
                    ReservedFrom = table.Column<DateTime>(type: "datetime", nullable: false),
                    ReservedUntil = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reservation_Client",
                        column: x => x.ClientId,
                        principalTable: "Client",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Reservation_Vehicle",
                        column: x => x.VehicleId,
                        principalTable: "Vehicle",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Client",
                columns: new[] { "Id", "Country", "DOB", "FirstName", "Gender", "LastName" },
                values: new object[,]
                {
                    { 1, "Croatia", new DateTime(2000, 8, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "Marko", "Male", "Marulić" },
                    { 2, "Croatia", new DateTime(1995, 8, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ivo", "Male", "Ivić" },
                    { 3, "Croatia", new DateTime(1980, 8, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "Alenko", "Male", "Alenić" }
                });

            migrationBuilder.InsertData(
                table: "Vehicle",
                columns: new[] { "Id", "Color", "Manufacturer", "Model", "Type", "Year" },
                values: new object[,]
                {
                    { 1, "Black", "BMW", "Series 3", "Sedan", (short)2010 },
                    { 2, "Red", "Ferrari", "Maranello", "Limousine", (short)1993 },
                    { 3, "Yellow", "Lamborghini", "Diablo", "Limousine", (short)1990 }
                });

            migrationBuilder.InsertData(
                table: "Reservation",
                columns: new[] { "Id", "ClientId", "ReservedFrom", "ReservedUntil", "VehicleId" },
                values: new object[] { 1, 1, new DateTime(2022, 8, 11, 9, 5, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 8, 12, 9, 5, 0, 0, DateTimeKind.Unspecified), 1 });

            migrationBuilder.InsertData(
                table: "Reservation",
                columns: new[] { "Id", "ClientId", "ReservedFrom", "ReservedUntil", "VehicleId" },
                values: new object[] { 2, 1, new DateTime(2022, 8, 11, 9, 5, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 8, 12, 9, 5, 0, 0, DateTimeKind.Unspecified), 2 });

            migrationBuilder.CreateIndex(
                name: "IX_Reservation_ClientId",
                table: "Reservation",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservation_VehicleId",
                table: "Reservation",
                column: "VehicleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reservation");

            migrationBuilder.DropTable(
                name: "Client");

            migrationBuilder.DropTable(
                name: "Vehicle");
        }
    }
}
