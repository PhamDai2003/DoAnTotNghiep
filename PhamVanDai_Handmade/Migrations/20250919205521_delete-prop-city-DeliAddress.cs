using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PhamVanDai_Handmade.Migrations
{
    /// <inheritdoc />
    public partial class deletepropcityDeliAddress : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "46e6b37d-5a5e-41c6-887d-c52d5a350209");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "7865009f-0029-4948-9307-5ae4e334ac49", "35c0c185-f473-402b-80c9-cb3c8b22263b" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7865009f-0029-4948-9307-5ae4e334ac49");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "35c0c185-f473-402b-80c9-cb3c8b22263b");

            migrationBuilder.DropColumn(
                name: "City",
                table: "DeliveryAddresses");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "DeliveryAddresses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName", "Status" },
                values: new object[,]
                {
                    { "46e6b37d-5a5e-41c6-887d-c52d5a350209", null, "User", "USER", 0 },
                    { "7865009f-0029-4948-9307-5ae4e334ac49", null, "Admin", "ADMIN", 0 }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "IsDeleted", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "Status", "TwoFactorEnabled", "UserName" },
                values: new object[] { "35c0c185-f473-402b-80c9-cb3c8b22263b", 0, "2cbbf118-a65c-4859-974e-44a1c8e353ed", "admin@local.com", true, false, false, null, "ADMIN@LOCAL.COM", "Admin", "AQAAAAIAAYagAAAAEFH5SZHeJJkgU40FjHAInJvYxr3SYioFe23MfPZTy8FfzPPoPN51ywDji5Ki32CsSA==", null, false, "9f7f21f2-8e63-46fa-90c7-3ae17b76dc4d", 1, false, "Admin" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 1,
                column: "CreateAt",
                value: new DateTime(2025, 9, 20, 1, 47, 27, 436, DateTimeKind.Local).AddTicks(716));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 2,
                column: "CreateAt",
                value: new DateTime(2025, 9, 20, 1, 47, 27, 436, DateTimeKind.Local).AddTicks(739));

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "7865009f-0029-4948-9307-5ae4e334ac49", "35c0c185-f473-402b-80c9-cb3c8b22263b" });
        }
    }
}
