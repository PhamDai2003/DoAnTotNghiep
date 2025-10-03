using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhamVanDai_Handmade.Models;
using PhamVanDai_Handmade.Repository;
using System.Security.Claims;

namespace PhamVanDai_Handmade.Controllers
{
    [Authorize]
    public class WishlistItemController : Controller
    {
        private readonly DataContext _context;
        public WishlistItemController(DataContext context) 
        {
            _context = context;
        }

        // Action để hiển thị trang danh sách yêu thích
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var wishlistItems = await _context.WishlistItems
                .Where(w => w.UserID == userId)
                .Include(w => w.Product) // Tải kèm thông tin sản phẩm
                .ThenInclude(c => c.Category) // Tải kèm thông tin danh mục
                .ToListAsync();

            return View(wishlistItems);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Toggle(int productId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                return Json(new { success = false, message = "Vui lòng đăng nhập." });
            }

            // Tìm xem người dùng đã thích sản phẩm này chưa
            var existingItem = await _context.WishlistItems
                .FirstOrDefaultAsync(w => w.UserID == userId && w.ProductID == productId);

            if (existingItem != null)
            {
                // Nếu đã thích -> Bỏ thích
                _context.WishlistItems.Remove(existingItem);
                await _context.SaveChangesAsync();
                var newCount = await _context.WishlistItems.CountAsync(w => w.UserID == userId);
                return Json(new { success = true, added = false, count = newCount }); // Trả về trạng thái "đã xóa"
            }
            else
            {
                // Nếu chưa thích -> Thêm vào danh sách
                var newItem = new WishlistItemModel()
                {
                    UserID = userId,
                    ProductID = productId
                };
                _context.WishlistItems.Add(newItem);
                
                await _context.SaveChangesAsync();
                // Đếm lại số lượng mới và gửi về cho client
                var newCount = await _context.WishlistItems.CountAsync(w => w.UserID == userId);
                return Json(new { success = true, added = true, count = newCount }); // Trả về trạng thái "đã thêm"
            }
        }
    }
}
