using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Migrations
{
    public partial class updating_variables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Filepath",
                table: "Audio");

            migrationBuilder.AddColumn<int>(
                name: "ChannelCode",
                table: "Channel",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<double>(
                name: "Duration",
                table: "Audio",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "Audio",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ChannelCode",
                table: "Channel");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "Audio");

            migrationBuilder.AlterColumn<float>(
                name: "Duration",
                table: "Audio",
                type: "real",
                nullable: false,
                oldClrType: typeof(double));

            migrationBuilder.AddColumn<string>(
                name: "Filepath",
                table: "Audio",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
