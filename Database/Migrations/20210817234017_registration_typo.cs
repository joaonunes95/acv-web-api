using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Migrations
{
    public partial class registration_typo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Regsitration",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "Registration",
                table: "AspNetUsers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Registration",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "Regsitration",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
