using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PhamVanDai_Handmade.Models;
namespace PhamVanDai_Handmade.Repository
{
    public class DataContext : IdentityDbContext<UserModel, RoleModel, string>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }
        public DbSet<ProductModel> Products { get; set; }
        public DbSet<ProductVariant> ProductVariants { get; set; }

        public DbSet<CategoryModel> Categories { get; set; }
        public DbSet<CartItemModel> CartItems { get; set; }
        public DbSet<OrderModel> Orders { get; set; }
        public DbSet<OrderDetailModel> OrderDetails { get; set; }
        public DbSet<ReviewModel> Reviews { get; set; }
        public DbSet<CouponModel> Coupons { get; set; }
        public DbSet<WishlistItemModel> WishlistItems { get; set; }
        public DbSet<DeliveryAddressModel> DeliveryAddresses { get; set; }
        public DbSet<ContactModel> Contacts { get; set; }

        // Define DbSets for your entities here
        // public DbSet<YourEntity> YourEntities { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); // Luôn giữ dòng này ở đầu

            // --- Cấu hình cho các bảng có Khóa Composite ---
            modelBuilder.Entity<CartItemModel>()
                .HasKey(c => new { c.UserID, c.VariantID });

            modelBuilder.Entity<OrderDetailModel>()
                .HasKey(od => new { od.OrderID, od.VariantID });

            // --- Cấu hình các mối quan hệ để giải quyết xung đột ---

            // 1. Mối quan hệ User -> Order: Khi xóa User, các Order liên quan sẽ bị xóa.
            modelBuilder.Entity<OrderModel>()
                .HasOne(o => o.User)
                .WithMany(u => u.Orders)
                .HasForeignKey(o => o.UserID)
                .OnDelete(DeleteBehavior.Cascade); // Mặc định là Cascade, nhưng ghi rõ ra

            // 2. Mối quan hệ User -> Address: Khi xóa User, các Address liên quan sẽ bị xóa.
            modelBuilder.Entity<DeliveryAddressModel>()
                .HasOne(a => a.User)
                .WithMany(u => u.DeliveryAddresses)
                .HasForeignKey(a => a.UserID)
                .OnDelete(DeleteBehavior.Cascade); // Mặc định là Cascade, nhưng ghi rõ ra

            // 3. Mối quan hệ Address -> Order (QUAN TRỌNG NHẤT)
            // Khi xóa Address, KHÔNG được phép xóa các Order đã dùng địa chỉ này.
            modelBuilder.Entity<OrderModel>()
                .HasOne(o => o.DeliveryAddress)
                .WithMany(a => a.Orders)
                .HasForeignKey(o => o.AddressID)
                .OnDelete(DeleteBehavior.Restrict); // Dòng này ngắt chu trình xung đột

            // --- Các mối quan hệ khác (nên định nghĩa rõ ràng) ---
            // Ví dụ: ProductVariant -> OrderDetail
            modelBuilder.Entity<OrderDetailModel>()
                .HasOne(od => od.ProductVariant)
                .WithMany(pv => pv.OrderDetails)
                .HasForeignKey(od => od.VariantID)
                .OnDelete(DeleteBehavior.Restrict); // Khi xóa 1 biến thể, không nên xóa lịch sử đơn hàng
            modelBuilder.SeedingData();
            // ... bạn có thể thêm các cấu hình khác ở đây nếu cần ...
        }
    }
}
