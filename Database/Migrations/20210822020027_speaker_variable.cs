using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Migrations
{
    public partial class speaker_variable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Local",
                table: "Speaker",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Local",
                table: "Speaker");
        }
    }
}
