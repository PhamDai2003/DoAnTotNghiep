using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhamVanDai_Handmade.Models; // Namespace của bạn
using PhamVanDai_Handmade.Models.ViewModels;
using PhamVanDai_Handmade.Repository;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;


namespace PhamVanDai_Handmade.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private readonly DataContext _context;

        public OrderController(DataContext context)
        {
            _context = context;
        }

        // GET: /Order/Index
        // Action này để hiển thị danh sách tất cả đơn hàng của người dùng
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var orders = await _context.Orders
                .Include(o => o.DeliveryAddress)
                .Where(o => o.UserID == userId)
                .OrderByDescending(o => o.OrderDate) // Sắp xếp đơn hàng mới nhất lên đầu
                .ToListAsync();

            return View(orders);
        }

        // Xem chi tiết đơn hàng
        [HttpGet]
        public async Task<IActionResult> Detail(int id)
        {
            var order = await _context.Orders
                .Include(o => o.User) // Lấy thông tin User đặt hàng
                .Include(o => o.Coupon)
                .Include(o => o.DeliveryAddress) // Lấy thông tin người nhận
                .Include(o => o.OrderDetails)
                    .ThenInclude(od => od.ProductVariant) // Variant
                        .ThenInclude(v => v.Product) // Thông tin sản phẩm gốc
                .FirstOrDefaultAsync(o => o.OrderID == id);

            if (order == null)
            {
                return NotFound();
            }

            var vm = new OrderDetailViewModel
            {
                OrderID = order.OrderID,
                OrderDate = order.OrderDate,
                Status = order.Status,
                StatusText = GetOrderStatusText(order.Status),
                PaymentStatus = order.PaymentStatus,
                PaymentStatusText = order.PaymentStatus == 1 ? "Chưa thanh toán" : order.PaymentStatus == 2 ? "Đã thanh toán" : order.PaymentStatus == 3 ? "Đã hoàn tiền" : "Thất bại",
                ShippingFee = order.ShippingFree,
                CouponId = order.CouponID,
                CouponCode = order.Coupon?.Code ?? "Không có",

                UserName = order.User?.UserName ?? "Khách vãng lai",
                Email = order.User?.Email ?? string.Empty,

                DeliveryAddress = new DeliveryAddressViewModel
                {
                    Name = order.DeliveryAddress?.FullName ?? "",
                    PhoneNumber = order.DeliveryAddress?.PhoneNumber ?? "",
                    Address = order.DeliveryAddress?.Address ?? ""
                },

                OrderDetails = order.OrderDetails.Select(od => new OrderItemViewModel
                {
                    VariantId = od.VariantID,
                    ProductName = od.ProductVariant.Product.ProductName,
                    Size = od.ProductVariant.Size,
                    Color = od.ProductVariant.Color,
                    Quantity = od.Quantity,
                    Price = od.ProductVariant.Price,
                    Image = od.ProductVariant.Image
                }).ToList()
            };

            // Tính tổng tiền = sum(productVariant.Price * quantity) + phí ship - giảm giá
            vm.TotalAmount = vm.OrderDetails.Sum(x => x.Price * x.Quantity) + vm.ShippingFee;
            if (order.CouponID.HasValue)
            {
                vm.TotalAmount -= order.Coupon.DiscountAmount ?? 0;
            }
            return View(vm);
        }

        // Helper: Convert Status int -> text
        private string GetOrderStatusText(int status)
        {
            return status switch
            {
                0 => "Chờ xử lý",
                1 => "Đã xác nhận",
                2 => "Đang giao",
                3 => "Hoàn thành",
                4 => "Hoàn trả",
                5 => "Đã hủy",
                _ => "Không xác định"
            };
        }


        // POST: /Order/Cancel
        // Action này để xử lý việc hủy đơn hàng
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Cancel(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // Tìm đơn hàng, đảm bảo nó thuộc về đúng người dùng
            var order = await _context.Orders
                .Include(o => o.OrderDetails)
                    .ThenInclude(od => od.ProductVariant)
                .FirstOrDefaultAsync(o => o.OrderID == id && o.UserID == userId);

            if (order == null)
            {
                TempData["Error"] = "Không tìm thấy đơn hàng.";
                return RedirectToAction("Index");
            }

            // Chỉ cho phép hủy nếu trạng thái là "Đang xử lý" (giả sử Status = 1)
            if (order.Status >=2)
            {
                TempData["Error"] = "Không thể hủy đơn hàng ở trạng thái này.";
                return RedirectToAction("Index");
            }

            // Cập nhật trạng thái đơn hàng thành "Đã hủy" (giả sử Status = 0)
            order.Status = 5;

            // Hoàn lại số lượng tồn kho cho các sản phẩm trong đơn hàng
            foreach (var detail in order.OrderDetails)
            {
                var variant = detail.ProductVariant;
                if (variant != null)
                {
                    variant.Quantity += detail.Quantity;
                    variant.SoldCount -= detail.Quantity;
                    _context.ProductVariants.Update(variant);
                }
            }

            // (Tùy chọn) Hoàn lại lượt sử dụng coupon
            if (order.CouponID.HasValue)
            {
                var coupon = await _context.Coupons.FirstOrDefaultAsync(c => c.CouponID == order.CouponID);
                if (coupon != null)
                {
                    coupon.quantity += 1;
                    _context.Coupons.Update(coupon);
                }
            }
            _context.Update(order);
            await _context.SaveChangesAsync();

            TempData["Success"] = $"Đã hủy thành công đơn hàng #{order.OrderID}.";
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Return(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // Tìm đơn hàng, đảm bảo nó thuộc về đúng người dùng
            var order = await _context.Orders
                .Include(o => o.OrderDetails)
                    .ThenInclude(od => od.ProductVariant)
                .FirstOrDefaultAsync(o => o.OrderID == id && o.UserID == userId);
            if (order == null)
            {
                TempData["Error"] = "Không tìm thấy đơn hàng.";
                return RedirectToAction("Index");
            }
            order.Status = 6;
            _context.Update(order);
            await _context.SaveChangesAsync();
            TempData["Success"] = $"Yêu cầu hoàn trả đơn hàng #{order.OrderID}. của bạn đã được gửi đi";
            return RedirectToAction("Index");
        }
    }
}
