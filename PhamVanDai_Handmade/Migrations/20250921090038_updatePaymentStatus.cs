using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PhamVanDai_Handmade.Migrations
{
    /// <inheritdoc />
    public partial class updatePaymentStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "dfc1791a-2028-49fd-8fe5-a7f8b3919c07");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "ce043beb-3e40-45ad-9688-2117bd6df5f7", "220dfdbb-df1e-4405-843d-0dc095824115" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ce043beb-3e40-45ad-9688-2117bd6df5f7");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "220dfdbb-df1e-4405-843d-0dc095824115");

            migrationBuilder.AlterColumn<int>(
                name: "PaymentStatus",
                table: "Orders",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName", "Status", "isDeleted" },
                values: new object[,]
                {
                    { "037bdb56-5fa1-43d2-98f0-0c76eb1dde0b", null, "Admin", "ADMIN", 1, false },
                    { "a67aee02-1726-4b3a-be04-0fd528164334", null, "User", "USER", 1, false }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "IsDeleted", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "Status", "TwoFactorEnabled", "UserName" },
                values: new object[] { "6c54c8ca-fe50-43c0-9dc2-9d6c13d809b9", 0, "7f598338-301b-4905-8712-ded7c0c353cc", "admin@local.com", true, false, false, null, "ADMIN@LOCAL.COM", "Admin", "AQAAAAIAAYagAAAAEAw1NauABSoRVoudABzDisMVKJWrHmdkt1v1bAe6oka2iXSCJYZ2thhZP5r93SsnwQ==", null, false, "e318f992-2a99-4c7a-9f0d-aa91ecda6045", 1, false, "Admin" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 1,
                column: "CreateAt",
                value: new DateTime(2025, 9, 21, 16, 0, 36, 899, DateTimeKind.Local).AddTicks(9390));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 2,
                column: "CreateAt",
                value: new DateTime(2025, 9, 21, 16, 0, 36, 899, DateTimeKind.Local).AddTicks(9406));

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "037bdb56-5fa1-43d2-98f0-0c76eb1dde0b", "6c54c8ca-fe50-43c0-9dc2-9d6c13d809b9" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a67aee02-1726-4b3a-be04-0fd528164334");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "037bdb56-5fa1-43d2-98f0-0c76eb1dde0b", "6c54c8ca-fe50-43c0-9dc2-9d6c13d809b9" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "037bdb56-5fa1-43d2-98f0-0c76eb1dde0b");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6c54c8ca-fe50-43c0-9dc2-9d6c13d809b9");

            migrationBuilder.AlterColumn<string>(
                name: "PaymentStatus",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName", "Status", "isDeleted" },
                values: new object[,]
                {
                    { "ce043beb-3e40-45ad-9688-2117bd6df5f7", null, "Admin", "ADMIN", 1, false },
                    { "dfc1791a-2028-49fd-8fe5-a7f8b3919c07", null, "User", "USER", 1, false }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "IsDeleted", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "Status", "TwoFactorEnabled", "UserName" },
                values: new object[] { "220dfdbb-df1e-4405-843d-0dc095824115", 0, "bc5ac0a6-86bb-4f62-b442-afb44ed7d8e2", "admin@local.com", true, false, false, null, "ADMIN@LOCAL.COM", "Admin", "AQAAAAIAAYagAAAAEKcbZAT2OqP/Z1XY0nPHAWWDMadrpOamuIdL0yNmx+ZWJeBJInpF57vlSbU60+YUeg==", null, false, "815db1b2-f30c-42fe-aec4-d525c47b034b", 1, false, "Admin" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 1,
                column: "CreateAt",
                value: new DateTime(2025, 9, 21, 15, 34, 15, 528, DateTimeKind.Local).AddTicks(1628));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 2,
                column: "CreateAt",
                value: new DateTime(2025, 9, 21, 15, 34, 15, 528, DateTimeKind.Local).AddTicks(1645));

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "ce043beb-3e40-45ad-9688-2117bd6df5f7", "220dfdbb-df1e-4405-843d-0dc095824115" });
        }
    }
}
