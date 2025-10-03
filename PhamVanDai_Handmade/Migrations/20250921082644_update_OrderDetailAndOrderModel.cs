using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PhamVanDai_Handmade.Migrations
{
    /// <inheritdoc />
    public partial class update_OrderDetailAndOrderModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d5b4d3bb-71df-4cca-a527-760de8275c55");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "16ec1dfd-5e36-4991-ab39-7db151689ad5", "9fb5886c-08c2-4c6e-8454-87e70ab9862a" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "16ec1dfd-5e36-4991-ab39-7db151689ad5");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9fb5886c-08c2-4c6e-8454-87e70ab9862a");

            migrationBuilder.AddColumn<string>(
                name: "PaymentMethod",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "PaymentMethod",
                table: "Orders");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName", "Status", "isDeleted" },
                values: new object[,]
                {
                    { "16ec1dfd-5e36-4991-ab39-7db151689ad5", null, "Admin", "ADMIN", 1, false },
                    { "d5b4d3bb-71df-4cca-a527-760de8275c55", null, "User", "USER", 1, false }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "IsDeleted", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "Status", "TwoFactorEnabled", "UserName" },
                values: new object[] { "9fb5886c-08c2-4c6e-8454-87e70ab9862a", 0, "115b9e26-4d70-49e5-b8da-163c6700fc00", "admin@local.com", true, false, false, null, "ADMIN@LOCAL.COM", "Admin", "AQAAAAIAAYagAAAAEDaX4nBHPUS6nJrPb9l0351Fau5gZoGboq3Uc4jCNCpC6irWN7W3kPG+1GpkCYDBbg==", null, false, "baef25a2-69ae-4b43-a506-5ebc0d9b9fd8", 1, false, "Admin" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 1,
                column: "CreateAt",
                value: new DateTime(2025, 9, 20, 18, 19, 47, 961, DateTimeKind.Local).AddTicks(600));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 2,
                column: "CreateAt",
                value: new DateTime(2025, 9, 20, 18, 19, 47, 961, DateTimeKind.Local).AddTicks(617));

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "16ec1dfd-5e36-4991-ab39-7db151689ad5", "9fb5886c-08c2-4c6e-8454-87e70ab9862a" });
        }
    }
}
