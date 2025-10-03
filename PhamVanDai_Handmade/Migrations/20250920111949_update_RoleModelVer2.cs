using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PhamVanDai_Handmade.Migrations
{
    /// <inheritdoc />
    public partial class update_RoleModelVer2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0e2ec770-e8df-4d19-b4c4-5cd58e35a73b");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "7168fa33-bc86-470f-92fb-81ce8909b1a0", "e01b7879-712d-4ac5-9173-fb95085f90ca" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7168fa33-bc86-470f-92fb-81ce8909b1a0");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "e01b7879-712d-4ac5-9173-fb95085f90ca");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName", "Status", "isDeleted" },
                values: new object[,]
                {
                    { "0e2ec770-e8df-4d19-b4c4-5cd58e35a73b", null, "User", "USER", 0, false },
                    { "7168fa33-bc86-470f-92fb-81ce8909b1a0", null, "Admin", "ADMIN", 0, false }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "IsDeleted", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "Status", "TwoFactorEnabled", "UserName" },
                values: new object[] { "e01b7879-712d-4ac5-9173-fb95085f90ca", 0, "19bf95a6-6451-4b61-b666-bcb077d49bfa", "admin@local.com", true, false, false, null, "ADMIN@LOCAL.COM", "Admin", "AQAAAAIAAYagAAAAEFl3sRqv6l9oS3XedU700G+IXUlH0QFCX2iGIS073UxHD8rn3qVPjqRr63QjSwJvrw==", null, false, "56fecb24-2a2f-4aa4-ad7a-9d48a7819671", 1, false, "Admin" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 1,
                column: "CreateAt",
                value: new DateTime(2025, 9, 20, 18, 9, 40, 895, DateTimeKind.Local).AddTicks(7863));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 2,
                column: "CreateAt",
                value: new DateTime(2025, 9, 20, 18, 9, 40, 895, DateTimeKind.Local).AddTicks(7880));

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "7168fa33-bc86-470f-92fb-81ce8909b1a0", "e01b7879-712d-4ac5-9173-fb95085f90ca" });
        }
    }
}
