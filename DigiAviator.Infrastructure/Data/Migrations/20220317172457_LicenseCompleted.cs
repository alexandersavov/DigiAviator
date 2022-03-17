using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DigiAviator.Infrastructure.Data.Migrations
{
    public partial class LicenseCompleted : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Languages_Licenses_LicenseId",
                table: "Languages");

            migrationBuilder.DropForeignKey(
                name: "FK_Licenses_LicenseTypes_LicenseTypeId",
                table: "Licenses");

            migrationBuilder.DropForeignKey(
                name: "FK_Ratings_Licenses_LicenseId",
                table: "Ratings");

            migrationBuilder.DropTable(
                name: "LicenseTypes");

            migrationBuilder.DropIndex(
                name: "IX_Licenses_LicenseTypeId",
                table: "Licenses");

            migrationBuilder.DropColumn(
                name: "LicenseTypeId",
                table: "Licenses");

            migrationBuilder.RenameColumn(
                name: "IssueAuthorithy",
                table: "Licenses",
                newName: "IssuingAuthorithy");

            migrationBuilder.AlterColumn<Guid>(
                name: "LicenseId",
                table: "Ratings",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CountryCode",
                table: "Licenses",
                type: "nvarchar(6)",
                maxLength: 6,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfInitialIssue",
                table: "Licenses",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "TitleOfLicense",
                table: "Licenses",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<Guid>(
                name: "LicenseId",
                table: "Languages",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Languages_Licenses_LicenseId",
                table: "Languages",
                column: "LicenseId",
                principalTable: "Licenses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Ratings_Licenses_LicenseId",
                table: "Ratings",
                column: "LicenseId",
                principalTable: "Licenses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Languages_Licenses_LicenseId",
                table: "Languages");

            migrationBuilder.DropForeignKey(
                name: "FK_Ratings_Licenses_LicenseId",
                table: "Ratings");

            migrationBuilder.DropColumn(
                name: "CountryCode",
                table: "Licenses");

            migrationBuilder.DropColumn(
                name: "DateOfInitialIssue",
                table: "Licenses");

            migrationBuilder.DropColumn(
                name: "TitleOfLicense",
                table: "Licenses");

            migrationBuilder.RenameColumn(
                name: "IssuingAuthorithy",
                table: "Licenses",
                newName: "IssueAuthorithy");

            migrationBuilder.AlterColumn<Guid>(
                name: "LicenseId",
                table: "Ratings",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<Guid>(
                name: "LicenseTypeId",
                table: "Licenses",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<Guid>(
                name: "LicenseId",
                table: "Languages",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.CreateTable(
                name: "LicenseTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CountryCode = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: false),
                    DateOfInitialIssue = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TitleOfLicense = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LicenseTypes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Licenses_LicenseTypeId",
                table: "Licenses",
                column: "LicenseTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Languages_Licenses_LicenseId",
                table: "Languages",
                column: "LicenseId",
                principalTable: "Licenses",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Licenses_LicenseTypes_LicenseTypeId",
                table: "Licenses",
                column: "LicenseTypeId",
                principalTable: "LicenseTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Ratings_Licenses_LicenseId",
                table: "Ratings",
                column: "LicenseId",
                principalTable: "Licenses",
                principalColumn: "Id");
        }
    }
}
