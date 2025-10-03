using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PhamVanDai_Handmade.Models;
using System.Data;
using System.Reflection.Emit;

namespace PhamVanDai_Handmade.Repository
{
    public static class SeedData
    {
        public static void SeedingData(this ModelBuilder modelBuilder)
        {
            // Danh mục Handmade
            modelBuilder.Entity<CategoryModel>().HasData(
                new CategoryModel { CategoryID = 1, CategoryName = "Trang sức handmade", Slug = "trang-suc-handmade", Description = "Vòng tay, dây chuyền, nhẫn làm thủ công", Status = 1, IsDeleted = false },
                new CategoryModel { CategoryID = 2, CategoryName = "Đồ trang trí", Slug = "do-trang-tri", Description = "Các sản phẩm decor thủ công", Status = 1, IsDeleted = false },
                new CategoryModel { CategoryID = 3, CategoryName = "Phụ kiện cá nhân", Slug = "phu-kien-ca-nhan", Description = "Móc khóa, ví vải, túi handmade", Status = 1, IsDeleted = false }
            );

            // Seed Product
            modelBuilder.Entity<ProductModel>().HasData(
                new ProductModel
                {
                    ProductID = 1,
                    ProductName = "Túi Handmade Vải Bố",
                    slug = "tui-handmade-vai-bo",
                    ShortDescription = "Túi vải bố handmade phong cách vintage",
                    Description = "Túi handmade được may từ vải bố cao cấp, bền đẹp và thân thiện môi trường.",
                    CreateAt = DateTime.Now,
                    Status = 1,
                    isDeteled = false,
                    CategoryID = 1
                },
                new ProductModel
                {
                    ProductID = 2,
                    ProductName = "Ví Handmade Da Bò",
                    slug = "vi-handmade-da-bo",
                    ShortDescription = "Ví da bò handmade nhỏ gọn",
                    Description = "Ví handmade da bò thật 100%, đường may tỉ mỉ, phù hợp làm quà tặng.",
                    CreateAt = DateTime.Now,
                    Status = 1,
                    isDeteled = false,
                    CategoryID = 1
                }
            );

            // Seed Product Variants
            modelBuilder.Entity<ProductVariant>().HasData(
                new ProductVariant
                {
                    VariantID = 1,
                    ProductID = 1,
                    Color = "Be",
                    Size = "S",
                    Price = 150000,
                    Quantity = 20,
                    SoldCount = 5,
                    Image = "/uploads/products/tui-handmade-be-s.jpg"
                },
                new ProductVariant
                {
                    VariantID = 2,
                    ProductID = 1,
                    Color = "Nâu",
                    Size = "M",
                    Price = 180000,
                    Quantity = 15,
                    SoldCount = 3,
                    Image = "/uploads/products/tui-handmade-nau-m.jpg"
                },
                new ProductVariant
                {
                    VariantID = 3,
                    ProductID = 2,
                    Color = "Đen",
                    Size = "Nhỏ",
                    Price = 250000,
                    Quantity = 10,
                    SoldCount = 7,
                    Image = "/uploads/products/vi-handmade-den-nho.jpg"
                },
                new ProductVariant
                {
                    VariantID = 4,
                    ProductID = 2,
                    Color = "Nâu",
                    Size = "Vừa",
                    Price = 280000,
                    Quantity = 8,
                    SoldCount = 2,
                    Image = "/uploads/products/vi-handmade-nau-vua.jpg"
                }
            );

            // Tạo role 
            // 1. Tạo các ID duy nhất cho mỗi Role 🔑
            var adminRoleId = Guid.NewGuid().ToString();
            var userRoleId = Guid.NewGuid().ToString();

            // 2. Dùng HasData để thêm các Role vào database ⚙️
            modelBuilder.Entity<RoleModel>().HasData(
                new RoleModel
                {
                    Id = adminRoleId,
                    Name = "Admin",
                    NormalizedName = "ADMIN", // Tên chuẩn hóa (viết hoa) để Identity so sánh             
                },
                new RoleModel
                {
                    Id = userRoleId,
                    Name = "User",
                    NormalizedName = "USER",
                }
            );

            // Tạo user Admin
            string userId = Guid.NewGuid().ToString();
            var hasher = new PasswordHasher<UserModel>();
            var userToSeed = new UserModel
            {
                Id = userId,
                UserName = "Admin",
                NormalizedUserName = "Admin",
                Email = "admin@local.com",
                NormalizedEmail = "ADMIN@LOCAL.COM",
                EmailConfirmed = true,
                Status = 1
                // Chưa gán PasswordHash ở đây
            };

            // 2. Dùng chính đối tượng user đó để tạo PasswordHash
            userToSeed.PasswordHash = hasher.HashPassword(userToSeed, "Admin@123");

            // 3. Seed toàn bộ đối tượng user đã hoàn chỉnh vào database
            modelBuilder.Entity<UserModel>().HasData(userToSeed);

            // Gán user vào role
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                UserId = userId,
                RoleId = adminRoleId
            });

            modelBuilder.Entity<CouponModel>().HasData(
            new CouponModel
            {
                CouponID = 1,
                Code = "HANDMADE10",
                Description = "Giảm 10K cho đơn hàng từ 100K",
                DiscountAmount = 10000,
                MinOrderValue = 100000,
                quantity = 3,
                StartDate = new DateTime(2025, 9, 1),
                EndDate = new DateTime(2025, 12, 31),
                status = 1,
                IsDeleted = false
            },
            new CouponModel
            {
                CouponID = 2,
                Code = "HANDMADE100",
                Description = "Giảm 100k cho đơn 1 triệu",
                DiscountAmount = 100000,
                MinOrderValue = 1000000,
                quantity = 2,
                StartDate = new DateTime(2025, 9, 1),
                EndDate = new DateTime(2025, 12, 31),
                status = 1,
                IsDeleted = false
            }
        );

        }
    }
}
