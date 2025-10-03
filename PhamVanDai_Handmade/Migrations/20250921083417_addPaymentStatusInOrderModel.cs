using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PhamVanDai_Handmade.Migrations
{
    /// <inheritdoc />
    public partial class addPaymentStatusInOrderModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "af2e7afb-fd69-4681-bb0f-c65cdd0ec289");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "5e12d1b7-7a9d-474f-8e7d-c0a4fe8fb61b", "62c3053f-1d11-4f4f-b64d-5f78c30d9782" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5e12d1b7-7a9d-474f-8e7d-c0a4fe8fb61b");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "62c3053f-1d11-4f4f-b64d-5f78c30d9782");

            migrationBuilder.AddColumn<string>(
                name: "PaymentStatus",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "PaymentStatus",
                table: "Orders");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName", "Status", "isDeleted" },
                values: new object[,]
                {
                    { "5e12d1b7-7a9d-474f-8e7d-c0a4fe8fb61b", null, "Admin", "ADMIN", 1, false },
                    { "af2e7afb-fd69-4681-bb0f-c65cdd0ec289", null, "User", "USER", 1, false }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "IsDeleted", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "Status", "TwoFactorEnabled", "UserName" },
                values: new object[] { "62c3053f-1d11-4f4f-b64d-5f78c30d9782", 0, "380a9e5c-8612-44ed-8995-7846950b8554", "admin@local.com", true, false, false, null, "ADMIN@LOCAL.COM", "Admin", "AQAAAAIAAYagAAAAEGYAVwPTzwUK3XaeZvdtBhDt2qbqMJWpsShfd7RoMe57QjhHWFKCQmH6ZmpDyjlrZw==", null, false, "2dcb0903-07ae-4957-9723-445756331226", 1, false, "Admin" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 1,
                column: "CreateAt",
                value: new DateTime(2025, 9, 21, 15, 26, 42, 284, DateTimeKind.Local).AddTicks(4565));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 2,
                column: "CreateAt",
                value: new DateTime(2025, 9, 21, 15, 26, 42, 284, DateTimeKind.Local).AddTicks(4580));

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "5e12d1b7-7a9d-474f-8e7d-c0a4fe8fb61b", "62c3053f-1d11-4f4f-b64d-5f78c30d9782" });
        }
    }
}
