using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DigiAviator.Infrastructure.Data.Migrations
{
    public partial class MedicalAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Nationality",
                table: "Licenses",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(60)",
                oldMaxLength: 60);

            migrationBuilder.AddColumn<DateTime>(
                name: "BirthDate",
                table: "Licenses",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "Medicals",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IssuingAuthorithy = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    LicenseNumber = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    MedicalNumber = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    MiddleName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Nationality = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IssuedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medicals", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FitnessTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FitnessClass = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    ValidUntil = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MedicalId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FitnessTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FitnessTypes_Medicals_MedicalId",
                        column: x => x.MedicalId,
                        principalTable: "Medicals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Limitations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LimitationCode = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    MedicalId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Limitations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Limitations_Medicals_MedicalId",
                        column: x => x.MedicalId,
                        principalTable: "Medicals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FitnessTypes_MedicalId",
                table: "FitnessTypes",
                column: "MedicalId");

            migrationBuilder.CreateIndex(
                name: "IX_Limitations_MedicalId",
                table: "Limitations",
                column: "MedicalId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FitnessTypes");

            migrationBuilder.DropTable(
                name: "Limitations");

            migrationBuilder.DropTable(
                name: "Medicals");

            migrationBuilder.DropColumn(
                name: "BirthDate",
                table: "Licenses");

            migrationBuilder.AlterColumn<string>(
                name: "Nationality",
                table: "Licenses",
                type: "nvarchar(60)",
                maxLength: 60,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30);
        }
    }
}
