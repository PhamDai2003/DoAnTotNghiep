using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhamVanDai_Handmade.Models.ViewModels;
using System.Security.Claims;

namespace PhamVanDai_Handmade.Repository.Components
{
    public class CartViewComponent : ViewComponent
    {
        private readonly DataContext _context;
        public CartViewComponent(DataContext context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            // Lấy thông tin người dùng đang đăng nhập
            var userPrincipal = HttpContext.User;
            var userId = userPrincipal.FindFirstValue(ClaimTypes.NameIdentifier);

            // Khởi tạo viewModel với giá trị mặc định
            var viewModel = new CartViewModel { TotalItems = 0, TotalPrice = 0 };

            // Nếu user đã đăng nhập
            if (!string.IsNullOrEmpty(userId))
            {
                // Lấy danh sách các sản phẩm trong giỏ hàng
                var cartItems = await _context.CartItems
                                        .Include(c => c.ProductVariant) // Join với bảng Product để lấy giá
                                        .Where(c => c.UserID == userId)
                                        .ToListAsync();
                if (cartItems.Any())
                {
                    // Tính tổng sản phẩm có trong giỏ hàng
                    viewModel.TotalItems = cartItems.Count();
                    // Tính tổng tiền = SUM(số lượng * đơn giá)
                    viewModel.TotalPrice = cartItems.Sum(c => c.Quantity * c.ProductVariant.Price);
                }
            }

            // Trả về view của component với model là số lượng đếm được
            return View(viewModel);
        }
    }
}
