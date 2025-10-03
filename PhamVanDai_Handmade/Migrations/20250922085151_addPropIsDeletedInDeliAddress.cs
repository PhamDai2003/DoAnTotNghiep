using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PhamVanDai_Handmade.Migrations
{
    /// <inheritdoc />
    public partial class addPropIsDeletedInDeliAddress : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                name: "IsDeteted",
                table: "DeliveryAddresses",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName", "Status", "isDeleted" },
                values: new object[,]
                {
                    { "85879ed2-761f-4a74-aa03-cc396b7fa8e4", null, "Admin", "ADMIN", 1, false },
                    { "94aeba25-c4c6-4e03-a568-ff526674168f", null, "User", "USER", 1, false }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "IsDeleted", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "Status", "TwoFactorEnabled", "UserName" },
                values: new object[] { "68dc0921-9168-4d7a-aef4-f87903f6585f", 0, "c3c2a8f1-3056-4b92-8d5a-32e7c865d119", "admin@local.com", true, false, false, null, "ADMIN@LOCAL.COM", "Admin", "AQAAAAIAAYagAAAAEOyfssU/diByC4mkNwEXH/VXeMEcrS5FjCsujRG0aHV5icPz39JpOrCfb+eniXYgtw==", null, false, "4ddc1bc6-039b-43ef-9716-7d385e0b03b5", 1, false, "Admin" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 1,
                column: "CreateAt",
                value: new DateTime(2025, 9, 22, 15, 51, 50, 236, DateTimeKind.Local).AddTicks(2926));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 2,
                column: "CreateAt",
                value: new DateTime(2025, 9, 22, 15, 51, 50, 236, DateTimeKind.Local).AddTicks(2942));

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "85879ed2-761f-4a74-aa03-cc396b7fa8e4", "68dc0921-9168-4d7a-aef4-f87903f6585f" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "94aeba25-c4c6-4e03-a568-ff526674168f");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "85879ed2-761f-4a74-aa03-cc396b7fa8e4", "68dc0921-9168-4d7a-aef4-f87903f6585f" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "85879ed2-761f-4a74-aa03-cc396b7fa8e4");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "68dc0921-9168-4d7a-aef4-f87903f6585f");

            migrationBuilder.DropColumn(
                name: "IsDeteted",
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
    }
}
