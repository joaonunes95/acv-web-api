using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Migrations
{
    public partial class SeedInitialData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "019f381f-cbd1-4b05-b3f5-00f1674ee5da");

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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Approved", "BirthDate", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "Registration", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "019f381f-cbd1-4b05-b3f5-00f1674ee5da", 0, false, new DateTime(1958, 3, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "761e6af4-085a-490c-80cf-e0bb560c578d", "admin@admin.com", true, "Admin", "", false, null, "ADMIN@ADMIN.COM", null, "AQAAAAEAACcQAAAAEBOrayTSMH/65RzJAL+sg8ODtyMnQW6MEFWMlw+w49zq9poA6SiQiugt4PKJSv4cgw==", null, false, "11111111", "", false, "admin@admin.com" });
        }
    }
}
