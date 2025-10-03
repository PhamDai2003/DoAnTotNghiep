using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PhamVanDai_Handmade.Migrations
{
    /// <inheritdoc />
    public partial class seedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryID", "CategoryName", "Description", "IsDeleted", "Slug", "Status" },
                values: new object[,]
                {
                    { 1, "Trang sức handmade", "Vòng tay, dây chuyền, nhẫn làm thủ công", false, "trang-suc-handmade", 1 },
                    { 2, "Đồ trang trí", "Các sản phẩm decor thủ công", false, "do-trang-tri", 1 },
                    { 3, "Phụ kiện cá nhân", "Móc khóa, ví vải, túi handmade", false, "phu-kien-ca-nhan", 1 }
                });

            migrationBuilder.InsertData(
                table: "Coupons",
                columns: new[] { "CouponID", "Code", "Description", "DiscountAmount", "EndDate", "IsDeleted", "MinOrderValue", "StartDate", "quantity", "status" },
                values: new object[,]
                {
                    { 1, "HANDMADE10", "Giảm 10K cho đơn hàng từ 100K", 10000m, new DateTime(2025, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 100000m, new DateTime(2025, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 1 },
                    { 2, "HANDMADE100", "Giảm 100k cho đơn 1 triệu", 100000m, new DateTime(2025, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 1000000m, new DateTime(2025, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 1 }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "7865009f-0029-4948-9307-5ae4e334ac49", "35c0c185-f473-402b-80c9-cb3c8b22263b" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductID", "CategoryID", "CreateAt", "Description", "Image", "ProductName", "ShortDescription", "Status", "isDeteled", "slug" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2025, 9, 20, 1, 47, 27, 436, DateTimeKind.Local).AddTicks(716), "Túi handmade được may từ vải bố cao cấp, bền đẹp và thân thiện môi trường.", null, "Túi Handmade Vải Bố", "Túi vải bố handmade phong cách vintage", 1, false, "tui-handmade-vai-bo" },
                    { 2, 1, new DateTime(2025, 9, 20, 1, 47, 27, 436, DateTimeKind.Local).AddTicks(739), "Ví handmade da bò thật 100%, đường may tỉ mỉ, phù hợp làm quà tặng.", null, "Ví Handmade Da Bò", "Ví da bò handmade nhỏ gọn", 1, false, "vi-handmade-da-bo" }
                });

            migrationBuilder.InsertData(
                table: "ProductVariants",
                columns: new[] { "VariantID", "Color", "Image", "Price", "ProductID", "Quantity", "Size", "SoldCount" },
                values: new object[,]
                {
                    { 1, "Be", "/uploads/products/tui-handmade-be-s.jpg", 150000m, 1, 20, "S", 5 },
                    { 2, "Nâu", "/uploads/products/tui-handmade-nau-m.jpg", 180000m, 1, 15, "M", 3 },
                    { 3, "Đen", "/uploads/products/vi-handmade-den-nho.jpg", 250000m, 2, 10, "Nhỏ", 7 },
                    { 4, "Nâu", "/uploads/products/vi-handmade-nau-vua.jpg", 280000m, 2, 8, "Vừa", 2 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Coupons",
                keyColumn: "CouponID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Coupons",
                keyColumn: "CouponID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "VariantID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "VariantID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "VariantID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "VariantID",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7865009f-0029-4948-9307-5ae4e334ac49");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "35c0c185-f473-402b-80c9-cb3c8b22263b");

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryID",
                keyValue: 1);
        }
    }
}
