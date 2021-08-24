using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Migrations
{
    public partial class add_reliability : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Reliability",
                table: "Audio",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Reliability",
                table: "Audio");
        }
    }
}
