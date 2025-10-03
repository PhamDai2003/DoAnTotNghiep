using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PhamVanDai_Handmade.Migrations
{
    /// <inheritdoc />
    public partial class addContactModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Coupons",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "Coupons",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateTable(
                name: "Contacts",
                columns: table => new
                {
                    ContactID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserID = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contacts", x => x.ContactID);
                    table.ForeignKey(
                        name: "FK_Contacts_AspNetUsers_UserID",
                        column: x => x.UserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_UserID",
                table: "Contacts",
                column: "UserID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Contacts");

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

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Coupons",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "Coupons",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

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
    }
}
