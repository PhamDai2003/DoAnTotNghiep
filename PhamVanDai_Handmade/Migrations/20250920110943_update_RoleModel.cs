using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PhamVanDai_Handmade.Migrations
{
    /// <inheritdoc />
    public partial class update_RoleModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "592b11b7-0d63-4e92-861e-0821ed6c0939");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "39bb7b7e-fae1-4a90-a5fa-930d91ac0e7e", "9aebd9b5-e32f-4ecf-b7a2-ea1255fa60bc" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "39bb7b7e-fae1-4a90-a5fa-930d91ac0e7e");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9aebd9b5-e32f-4ecf-b7a2-ea1255fa60bc");

            migrationBuilder.AddColumn<bool>(
                name: "isDeleted",
                table: "AspNetRoles",
                type: "bit",
                nullable: false,
                defaultValue: false);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "isDeleted",
                table: "AspNetRoles");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName", "Status" },
                values: new object[,]
                {
                    { "39bb7b7e-fae1-4a90-a5fa-930d91ac0e7e", null, "Admin", "ADMIN", 0 },
                    { "592b11b7-0d63-4e92-861e-0821ed6c0939", null, "User", "USER", 0 }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "IsDeleted", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "Status", "TwoFactorEnabled", "UserName" },
                values: new object[] { "9aebd9b5-e32f-4ecf-b7a2-ea1255fa60bc", 0, "1ba7ff81-2983-46bb-90c2-9f9d00a3e852", "admin@local.com", true, false, false, null, "ADMIN@LOCAL.COM", "Admin", "AQAAAAIAAYagAAAAEFP15VmhyQvZlAUzuah/RUvmYJG9Mfd/aa9dmXTbISfMxcQRPRvfvTlBStXikJvuwQ==", null, false, "9637b4d7-6394-4075-8262-29ab2b15457b", 1, false, "Admin" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 1,
                column: "CreateAt",
                value: new DateTime(2025, 9, 20, 4, 18, 1, 380, DateTimeKind.Local).AddTicks(565));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 2,
                column: "CreateAt",
                value: new DateTime(2025, 9, 20, 4, 18, 1, 380, DateTimeKind.Local).AddTicks(592));

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "39bb7b7e-fae1-4a90-a5fa-930d91ac0e7e", "9aebd9b5-e32f-4ecf-b7a2-ea1255fa60bc" });
        }
    }
}
