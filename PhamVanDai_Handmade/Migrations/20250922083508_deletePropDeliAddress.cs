using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PhamVanDai_Handmade.Migrations
{
    /// <inheritdoc />
    public partial class deletePropDeliAddress : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "IsDefault",
                table: "DeliveryAddresses");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName", "Status", "isDeleted" },
                values: new object[,]
                {
                    { "30f5421d-cb14-4542-bf31-4f53ead40110", null, "Admin", "ADMIN", 1, false },
                    { "bfd1338a-88de-465b-a35a-5703c6d29ba7", null, "User", "USER", 1, false }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "IsDeleted", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "Status", "TwoFactorEnabled", "UserName" },
                values: new object[] { "8ae9e8f7-4704-4d0d-aa35-44308a501f53", 0, "06befb57-e608-498e-b33d-e9ed0159f4bd", "admin@local.com", true, false, false, null, "ADMIN@LOCAL.COM", "Admin", "AQAAAAIAAYagAAAAEG2A4ullJiyWFZ2Z+nBlpM2wzgZ1nBHSeQC8XtznKL/BE+yrxya85gUwxi/XvZwQeQ==", null, false, "423d3963-1d18-4723-9f8f-0bafd1a39917", 1, false, "Admin" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 1,
                column: "CreateAt",
                value: new DateTime(2025, 9, 22, 15, 35, 5, 516, DateTimeKind.Local).AddTicks(4300));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 2,
                column: "CreateAt",
                value: new DateTime(2025, 9, 22, 15, 35, 5, 516, DateTimeKind.Local).AddTicks(4325));

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "30f5421d-cb14-4542-bf31-4f53ead40110", "8ae9e8f7-4704-4d0d-aa35-44308a501f53" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bfd1338a-88de-465b-a35a-5703c6d29ba7");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "30f5421d-cb14-4542-bf31-4f53ead40110", "8ae9e8f7-4704-4d0d-aa35-44308a501f53" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "30f5421d-cb14-4542-bf31-4f53ead40110");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8ae9e8f7-4704-4d0d-aa35-44308a501f53");

            migrationBuilder.AddColumn<bool>(
                name: "IsDefault",
                table: "DeliveryAddresses",
                type: "bit",
                nullable: false,
                defaultValue: false);

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
    }
}
