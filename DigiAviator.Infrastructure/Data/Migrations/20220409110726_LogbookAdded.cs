using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DigiAviator.Infrastructure.Data.Migrations
{
    public partial class LogbookAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Logbooks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    MiddleName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    HolderId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Logbooks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Logbooks_AspNetUsers_HolderId",
                        column: x => x.HolderId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Flights",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DateOfFlight = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DepartureAirportICAO = table.Column<string>(type: "nvarchar(4)", maxLength: 4, nullable: false),
                    DepartureTimeUTC = table.Column<TimeSpan>(type: "time", nullable: false),
                    ArrivalAirportICAO = table.Column<string>(type: "nvarchar(4)", maxLength: 4, nullable: false),
                    ArrivalTimeUTC = table.Column<TimeSpan>(type: "time", nullable: false),
                    AircraftType = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    AircraftRegistration = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    TotalFlightTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    PilotInCommandName = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    LandingsDay = table.Column<int>(type: "int", nullable: false),
                    LandingsNight = table.Column<int>(type: "int", nullable: false),
                    PilotInCommandFunctionTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    CopilotFunctionTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    DualFunctionTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    InstructorFunctionTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    LogbookId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Flights", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Flights_Logbooks_LogbookId",
                        column: x => x.LogbookId,
                        principalTable: "Logbooks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Flights_LogbookId",
                table: "Flights",
                column: "LogbookId");

            migrationBuilder.CreateIndex(
                name: "IX_Logbooks_HolderId",
                table: "Logbooks",
                column: "HolderId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Flights");

            migrationBuilder.DropTable(
                name: "Logbooks");
        }
    }
}
