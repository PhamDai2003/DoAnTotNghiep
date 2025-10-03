using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PhamVanDai_Handmade.Migrations
{
    /// <inheritdoc />
    public partial class addPropCreateDateInUserModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName", "Status", "isDeleted" },
                values: new object[,]
                {
                    { "03a32bec-2265-4f88-8a10-48f1c3e1932c", null, "User", "USER", 1, false },
                    { "d598f408-bd7d-4bfb-9004-0c4f9288acab", null, "Admin", "ADMIN", 1, false }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "CreatedDate", "Email", "EmailConfirmed", "IsDeleted", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "Status", "TwoFactorEnabled", "UserName" },
                values: new object[] { "8b544af8-aa4c-4819-a404-b777072fd9dd", 0, "0a21c9db-46b0-4aef-b91a-b7afa0d2239b", new DateTime(2025, 9, 24, 20, 54, 15, 5, DateTimeKind.Local).AddTicks(6799), "admin@local.com", true, false, false, null, "ADMIN@LOCAL.COM", "Admin", "AQAAAAIAAYagAAAAEFCGV+Ff0PczS/f0Y1DRMxd4nw7e/JkJpyC7fcSonFXodGEy3ELjqwxFidprF43+Yw==", null, false, "6e2025b5-3010-40dc-ab61-247cbca87be2", 1, false, "Admin" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 1,
                column: "CreateAt",
                value: new DateTime(2025, 9, 24, 20, 54, 15, 5, DateTimeKind.Local).AddTicks(6541));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 2,
                column: "CreateAt",
                value: new DateTime(2025, 9, 24, 20, 54, 15, 5, DateTimeKind.Local).AddTicks(6561));

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "d598f408-bd7d-4bfb-9004-0c4f9288acab", "8b544af8-aa4c-4819-a404-b777072fd9dd" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "03a32bec-2265-4f88-8a10-48f1c3e1932c");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "d598f408-bd7d-4bfb-9004-0c4f9288acab", "8b544af8-aa4c-4819-a404-b777072fd9dd" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d598f408-bd7d-4bfb-9004-0c4f9288acab");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8b544af8-aa4c-4819-a404-b777072fd9dd");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "AspNetUsers");

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
    }
}
