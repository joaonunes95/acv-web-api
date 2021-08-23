using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Migrations
{
    public partial class class_to_speaker : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Section_Class_ClassId",
                table: "Section");

            migrationBuilder.DropTable(
                name: "Class");

            migrationBuilder.DropIndex(
                name: "IX_Section_ClassId",
                table: "Section");

            migrationBuilder.DropColumn(
                name: "ClassId",
                table: "Section");

            migrationBuilder.AddColumn<Guid>(
                name: "SpeakerId",
                table: "Section",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "Speaker",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Speaker", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Section_SpeakerId",
                table: "Section",
                column: "SpeakerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Section_Speaker_SpeakerId",
                table: "Section",
                column: "SpeakerId",
                principalTable: "Speaker",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Section_Speaker_SpeakerId",
                table: "Section");

            migrationBuilder.DropTable(
                name: "Speaker");

            migrationBuilder.DropIndex(
                name: "IX_Section_SpeakerId",
                table: "Section");

            migrationBuilder.DropColumn(
                name: "SpeakerId",
                table: "Section");

            migrationBuilder.AddColumn<Guid>(
                name: "ClassId",
                table: "Section",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "Class",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Class", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Section_ClassId",
                table: "Section",
                column: "ClassId");

            migrationBuilder.AddForeignKey(
                name: "FK_Section_Class_ClassId",
                table: "Section",
                column: "ClassId",
                principalTable: "Class",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
