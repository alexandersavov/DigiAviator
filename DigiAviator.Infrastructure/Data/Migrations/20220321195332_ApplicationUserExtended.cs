using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DigiAviator.Infrastructure.Data.Migrations
{
    public partial class ApplicationUserExtended : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "HolderId",
                table: "Medicals",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "HolderId",
                table: "Licenses",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Medicals_HolderId",
                table: "Medicals",
                column: "HolderId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Licenses_HolderId",
                table: "Licenses",
                column: "HolderId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Licenses_AspNetUsers_HolderId",
                table: "Licenses",
                column: "HolderId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Medicals_AspNetUsers_HolderId",
                table: "Medicals",
                column: "HolderId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Licenses_AspNetUsers_HolderId",
                table: "Licenses");

            migrationBuilder.DropForeignKey(
                name: "FK_Medicals_AspNetUsers_HolderId",
                table: "Medicals");

            migrationBuilder.DropIndex(
                name: "IX_Medicals_HolderId",
                table: "Medicals");

            migrationBuilder.DropIndex(
                name: "IX_Licenses_HolderId",
                table: "Licenses");

            migrationBuilder.DropColumn(
                name: "HolderId",
                table: "Medicals");

            migrationBuilder.DropColumn(
                name: "HolderId",
                table: "Licenses");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "AspNetUsers");
        }
    }
}
