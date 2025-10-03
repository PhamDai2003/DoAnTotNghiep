using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PhamVanDai_Handmade.Migrations
{
    /// <inheritdoc />
    public partial class nulluserInDeli : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "26d53005-4d99-4e44-9827-a0cb6c70d758");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "7fcf735b-494b-4323-8adc-771156ffc247", "74bccee8-5e91-4825-ad58-c763f2a8dacc" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7fcf735b-494b-4323-8adc-771156ffc247");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "74bccee8-5e91-4825-ad58-c763f2a8dacc");

            migrationBuilder.AlterColumn<string>(
                name: "UserID",
                table: "DeliveryAddresses",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<string>(
                name: "UserID",
                table: "DeliveryAddresses",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName", "Status" },
                values: new object[,]
                {
                    { "26d53005-4d99-4e44-9827-a0cb6c70d758", null, "User", "USER", 0 },
                    { "7fcf735b-494b-4323-8adc-771156ffc247", null, "Admin", "ADMIN", 0 }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "IsDeleted", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "Status", "TwoFactorEnabled", "UserName" },
                values: new object[] { "74bccee8-5e91-4825-ad58-c763f2a8dacc", 0, "7939866c-d6a8-48cd-8776-63613185c05f", "admin@local.com", true, false, false, null, "ADMIN@LOCAL.COM", "Admin", "AQAAAAIAAYagAAAAEJ93OM+KMnfp3UWHlMYBGlxASyriDIeXjic+LrCUlXELGcMyDoE5VSiktr7I8NdzYA==", null, false, "04ffed1b-b9c8-4756-8537-d5da7c502af7", 1, false, "Admin" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 1,
                column: "CreateAt",
                value: new DateTime(2025, 9, 20, 3, 55, 20, 386, DateTimeKind.Local).AddTicks(2239));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 2,
                column: "CreateAt",
                value: new DateTime(2025, 9, 20, 3, 55, 20, 386, DateTimeKind.Local).AddTicks(2257));

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "7fcf735b-494b-4323-8adc-771156ffc247", "74bccee8-5e91-4825-ad58-c763f2a8dacc" });
        }
    }
}
