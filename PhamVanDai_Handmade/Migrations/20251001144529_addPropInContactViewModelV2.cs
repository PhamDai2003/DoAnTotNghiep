using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PhamVanDai_Handmade.Migrations
{
    /// <inheritdoc />
    public partial class addPropInContactViewModelV2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3aeffbd1-8d34-4dd0-85cd-53ca40368d40");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "e588de28-2c75-4e75-bacc-c6fd34cc7958", "fef50050-e481-44f8-820b-697379a1519e" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e588de28-2c75-4e75-bacc-c6fd34cc7958");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "fef50050-e481-44f8-820b-697379a1519e");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Contacts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "isDeleted",
                table: "Contacts",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName", "Status", "isDeleted" },
                values: new object[,]
                {
                    { "3a7eaa01-35e6-41a6-8b8b-3ff18815be59", null, "Admin", "ADMIN", 1, false },
                    { "c8b0618a-9e84-45dd-8b7f-04db723f8ba9", null, "User", "USER", 1, false }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "CreatedDate", "Email", "EmailConfirmed", "IsDeleted", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "Status", "TwoFactorEnabled", "UserName" },
                values: new object[] { "cea5e838-f272-43f4-b1ce-214589fe6cfa", 0, "b406f074-b1aa-415d-bf97-be8317f1f84d", new DateTime(2025, 10, 1, 21, 45, 28, 786, DateTimeKind.Local).AddTicks(436), "admin@local.com", true, false, false, null, "ADMIN@LOCAL.COM", "Admin", "AQAAAAIAAYagAAAAEDnQCP2eaxJtBxwxZ8h4WA1NrTa7emu35I0v1UuPr09J4FsHjkd9gxQU/u3nUWQzbA==", null, false, "7ef4267a-0ad9-440a-8691-7017d817d74b", 1, false, "Admin" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 1,
                column: "CreateAt",
                value: new DateTime(2025, 10, 1, 21, 45, 28, 786, DateTimeKind.Local).AddTicks(199));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 2,
                column: "CreateAt",
                value: new DateTime(2025, 10, 1, 21, 45, 28, 786, DateTimeKind.Local).AddTicks(215));

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "3a7eaa01-35e6-41a6-8b8b-3ff18815be59", "cea5e838-f272-43f4-b1ce-214589fe6cfa" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c8b0618a-9e84-45dd-8b7f-04db723f8ba9");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "3a7eaa01-35e6-41a6-8b8b-3ff18815be59", "cea5e838-f272-43f4-b1ce-214589fe6cfa" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3a7eaa01-35e6-41a6-8b8b-3ff18815be59");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "cea5e838-f272-43f4-b1ce-214589fe6cfa");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Contacts");

            migrationBuilder.DropColumn(
                name: "isDeleted",
                table: "Contacts");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName", "Status", "isDeleted" },
                values: new object[,]
                {
                    { "3aeffbd1-8d34-4dd0-85cd-53ca40368d40", null, "User", "USER", 1, false },
                    { "e588de28-2c75-4e75-bacc-c6fd34cc7958", null, "Admin", "ADMIN", 1, false }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "CreatedDate", "Email", "EmailConfirmed", "IsDeleted", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "Status", "TwoFactorEnabled", "UserName" },
                values: new object[] { "fef50050-e481-44f8-820b-697379a1519e", 0, "dd749238-8682-416b-9083-3c82237e79a5", new DateTime(2025, 10, 1, 21, 36, 51, 987, DateTimeKind.Local).AddTicks(2636), "admin@local.com", true, false, false, null, "ADMIN@LOCAL.COM", "Admin", "AQAAAAIAAYagAAAAEMV3/P3wioKHMkMk16yWWy6kthFPOlwV8q1Ast1QRuZWhPRw6XqbeXy09s7Lva3Big==", null, false, "b664f568-4339-4eb8-afef-7f4ef04c650e", 1, false, "Admin" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 1,
                column: "CreateAt",
                value: new DateTime(2025, 10, 1, 21, 36, 51, 987, DateTimeKind.Local).AddTicks(2376));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 2,
                column: "CreateAt",
                value: new DateTime(2025, 10, 1, 21, 36, 51, 987, DateTimeKind.Local).AddTicks(2399));

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "e588de28-2c75-4e75-bacc-c6fd34cc7958", "fef50050-e481-44f8-820b-697379a1519e" });
        }
    }
}
