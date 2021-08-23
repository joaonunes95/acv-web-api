using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Migrations
{
    public partial class new_entities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Descricao",
                table: "Audio");

            migrationBuilder.DropColumn(
                name: "Falante",
                table: "Audio");

            migrationBuilder.DropColumn(
                name: "Role",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<Guid>(
                name: "ChannelId",
                table: "Audio",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<float>(
                name: "Duration",
                table: "Audio",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<string>(
                name: "Filepath",
                table: "Audio",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Audio",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Channel",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Channel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Section",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Transcription = table.Column<string>(nullable: true),
                    Duration = table.Column<float>(nullable: false),
                    Start = table.Column<float>(nullable: false),
                    AudioId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Section", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Section_Audio_AudioId",
                        column: x => x.AudioId,
                        principalTable: "Audio",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SectionRole",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    SectionId = table.Column<Guid>(nullable: false),
                    RoleId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SectionRole", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SectionRole_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SectionRole_Section_SectionId",
                        column: x => x.SectionId,
                        principalTable: "Section",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Audio_ChannelId",
                table: "Audio",
                column: "ChannelId");

            migrationBuilder.CreateIndex(
                name: "IX_Section_AudioId",
                table: "Section",
                column: "AudioId");

            migrationBuilder.CreateIndex(
                name: "IX_SectionRole_RoleId",
                table: "SectionRole",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_SectionRole_SectionId",
                table: "SectionRole",
                column: "SectionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Audio_Channel_ChannelId",
                table: "Audio",
                column: "ChannelId",
                principalTable: "Channel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Audio_Channel_ChannelId",
                table: "Audio");

            migrationBuilder.DropTable(
                name: "Channel");

            migrationBuilder.DropTable(
                name: "SectionRole");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropTable(
                name: "Section");

            migrationBuilder.DropIndex(
                name: "IX_Audio_ChannelId",
                table: "Audio");

            migrationBuilder.DropColumn(
                name: "ChannelId",
                table: "Audio");

            migrationBuilder.DropColumn(
                name: "Duration",
                table: "Audio");

            migrationBuilder.DropColumn(
                name: "Filepath",
                table: "Audio");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Audio");

            migrationBuilder.AddColumn<string>(
                name: "Descricao",
                table: "Audio",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Falante",
                table: "Audio",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Role",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
