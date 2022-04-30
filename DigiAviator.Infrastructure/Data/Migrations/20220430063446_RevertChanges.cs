using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DigiAviator.Infrastructure.Data.Migrations
{
    public partial class RevertChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PilotInCommandName",
                table: "Flights",
                type: "nvarchar(60)",
                maxLength: 60,
                nullable: false,
                oldClrType: typeof(TimeSpan),
                oldType: "time");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<TimeSpan>(
                name: "PilotInCommandName",
                table: "Flights",
                type: "time",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(60)",
                oldMaxLength: 60);
        }
    }
}
