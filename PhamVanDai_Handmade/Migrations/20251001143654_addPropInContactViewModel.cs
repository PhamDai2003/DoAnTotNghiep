using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PhamVanDai_Handmade.Migrations
{
    /// <inheritdoc />
    public partial class addPropInContactViewModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2acc7782-d357-4873-b614-9a979f3047d8");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "c0b9086a-7a05-43eb-9fa9-e31a61646aec", "35c9c701-5b67-48e4-9824-af9d7bb6ea60" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c0b9086a-7a05-43eb-9fa9-e31a61646aec");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "35c9c701-5b67-48e4-9824-af9d7bb6ea60");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Contacts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Contacts");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName", "Status", "isDeleted" },
                values: new object[,]
                {
                    { "2acc7782-d357-4873-b614-9a979f3047d8", null, "User", "USER", 1, false },
                    { "c0b9086a-7a05-43eb-9fa9-e31a61646aec", null, "Admin", "ADMIN", 1, false }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "CreatedDate", "Email", "EmailConfirmed", "IsDeleted", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "Status", "TwoFactorEnabled", "UserName" },
                values: new object[] { "35c9c701-5b67-48e4-9824-af9d7bb6ea60", 0, "a94904ec-08e1-4b48-86a4-a597505bac74", new DateTime(2025, 10, 1, 20, 28, 4, 283, DateTimeKind.Local).AddTicks(689), "admin@local.com", true, false, false, null, "ADMIN@LOCAL.COM", "Admin", "AQAAAAIAAYagAAAAEIeKMQ92cz4AmU5DGwHyPaniw1iTHRjVLAO9nsi31EFLLj2vz0OaFJw8LduZrJs0KQ==", null, false, "e872551e-96a5-4ff6-a986-fcbcac629504", 1, false, "Admin" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 1,
                column: "CreateAt",
                value: new DateTime(2025, 10, 1, 20, 28, 4, 283, DateTimeKind.Local).AddTicks(448));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 2,
                column: "CreateAt",
                value: new DateTime(2025, 10, 1, 20, 28, 4, 283, DateTimeKind.Local).AddTicks(463));

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "c0b9086a-7a05-43eb-9fa9-e31a61646aec", "35c9c701-5b67-48e4-9824-af9d7bb6ea60" });
        }
    }
}
