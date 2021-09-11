using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Migrations
{
    public partial class audio_filepath : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a3c082cd-80bd-4399-af34-38fff7bf104e");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "UserId", "RoleId" },
                keyValues: new object[] { "2566de9d-766c-4ec5-ba72-626acb6fa29e", "8ebcea5d-f33e-4326-a579-705e683b01c4" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8ebcea5d-f33e-4326-a579-705e683b01c4");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2566de9d-766c-4ec5-ba72-626acb6fa29e");

            migrationBuilder.AddColumn<string>(
                name: "FilePath",
                table: "Audio",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "9926d175-211b-4332-9e75-14c621651055", "a0255bb3-abf4-42b5-a69c-79f5f75ff54d", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "84a603c4-6f0d-48c6-8477-0d41f254f51b", "e937a69c-faa5-4ce0-89ad-cc31026fefe5", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Approved", "BirthDate", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "Registration", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "c31a4ae9-d554-430f-a941-6e142b1401e9", 0, true, new DateTime(1958, 3, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "d9a1b0b9-135d-4b5e-88d8-35cef0d09dce", "admin@admin.com", true, "Admin", "Admin", false, null, "ADMIN@ADMIN.COM", null, "AQAAAAEAACcQAAAAEFDj6rrxG4rudaEWyyedZQBfWMRgWhSFzLt8AATrQNChcaD7NzPy1dxsbhCjqAYTMA==", null, false, "11111111", "", false, "admin@admin.com" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "UserId", "RoleId" },
                values: new object[] { "c31a4ae9-d554-430f-a941-6e142b1401e9", "9926d175-211b-4332-9e75-14c621651055" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "84a603c4-6f0d-48c6-8477-0d41f254f51b");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "UserId", "RoleId" },
                keyValues: new object[] { "c31a4ae9-d554-430f-a941-6e142b1401e9", "9926d175-211b-4332-9e75-14c621651055" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9926d175-211b-4332-9e75-14c621651055");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "c31a4ae9-d554-430f-a941-6e142b1401e9");

            migrationBuilder.DropColumn(
                name: "FilePath",
                table: "Audio");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "8ebcea5d-f33e-4326-a579-705e683b01c4", "52c991f9-845c-456d-b3b8-6007a316a9a6", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "a3c082cd-80bd-4399-af34-38fff7bf104e", "659a9d22-7f95-4166-a0c8-0b95863e4ca7", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Approved", "BirthDate", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "Registration", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "2566de9d-766c-4ec5-ba72-626acb6fa29e", 0, true, new DateTime(1958, 3, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "ac3e16c3-e1cb-496c-9d3e-91b07118ec43", "admin@admin.com", true, "Admin", "Admin", false, null, "ADMIN@ADMIN.COM", null, "AQAAAAEAACcQAAAAENYxBusCGzg0NhwIRJWCKgDz4jqX6/cyzsRiZt1tj/sOfYxtuieMA+9bjHI3/SBGOw==", null, false, "11111111", "", false, "admin@admin.com" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "UserId", "RoleId" },
                values: new object[] { "2566de9d-766c-4ec5-ba72-626acb6fa29e", "8ebcea5d-f33e-4326-a579-705e683b01c4" });
        }
    }
}
