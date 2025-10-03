using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhamVanDai_Handmade.Models;
using PhamVanDai_Handmade.Models.ViewModels;
using PhamVanDai_Handmade.Repository;
using X.PagedList;
using X.PagedList.Extensions;
namespace PhamVanDai_Handmade.Areas.Admin.Controllers
{
    [Area("Admin")]
   // [Authorize(Roles = "Admin")]
    public class OrderController : Controller
    {
        private readonly DataContext _context;

        public OrderController(DataContext context)
        {
            _context = context;
        }

        // Index + Lọc
        public ActionResult Index(int? orderId, string? userName, int? status, int? paymentStatus, DateTime? orderDate, int page = 1)
        {
            int pageSize = 10;
            var query = _context.Orders
                .Include(o => o.User)
                .Include(o => o.DeliveryAddress)
                .AsQueryable();

            if (orderId.HasValue)
            {
                query = query.Where(o => o.OrderID == orderId.Value);
            }

            if (!string.IsNullOrEmpty(userName))
            {
                query = query.Where(o => o.User.UserName.Contains(userName));
            }
            

            if (status.HasValue)
            {
                query = query.Where(o => o.Status == status.Value);
            }

            if (paymentStatus.HasValue)
            {
                query = query.Where(o => o.PaymentStatus == paymentStatus.Value);
            }

            if (orderDate.HasValue)
            {
                query = query.Where(o => o.OrderDate.Date == orderDate.Value.Date);
            }

            var orders =  query
                .OrderByDescending(o => o.OrderDate)
                .ToPagedList(page, pageSize);

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


        // Cập nhật trạng thái (tăng lên 1 bước)
        [HttpPost]
        public async Task<IActionResult> UpdateStatus(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null) return NotFound();

            if (order.Status >= 0 && order.Status <= 3) // 0=Chờ xử lý, 1=Đã xác nhận, 2=Đang giao 3=Hoàn thành 4= hoàn trả
            {
                order.Status += 1;
                _context.Update(order);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        // Cập nhật tình trạng thanh toán
        [HttpPost]
        public async Task<IActionResult> UpdatePaymentStatus(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null) return NotFound();

            // 1 = Chưa thanh toán, 2 = Đã thanh toán
            if (order.PaymentStatus == 1 || order.PaymentStatus == 2)
            {
                order.PaymentStatus = 2; // đã thanh toán
            }
            else
            {
                order.PaymentStatus = 0;
            }

            if(order.PaymentStatus == 2 && order.Status == 4 || order.PaymentStatus == 2 && order.Status == 5)
            {
                order.PaymentStatus = 3; // đã hoàn tiền
            }

            _context.Update(order);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // Hủy đơn hàng
        [HttpPost]
        public async Task<IActionResult> Cancel(int id)
        {
            var order = await _context.Orders
                .Include(o => o.OrderDetails)
                .FirstOrDefaultAsync(o => o.OrderID == id);
            if (order == null) return NotFound();

            if (order.Status == 0 || order.Status == 1) // chỉ hủy khi còn trong trạng thái 0-2
            {
                order.Status = 5; // 5 = Đã hủy
                foreach (var detail in order.OrderDetails)
                {
                    var variant = await _context.ProductVariants.FirstOrDefaultAsync(v => v.VariantID == detail.VariantID);
                    if(variant != null)
                    {
                        variant.Quantity += detail.Quantity;
                        variant.SoldCount -= detail.Quantity;
                        _context.ProductVariants.Update(variant);
                    }
                }

                if (order.CouponID.HasValue)
                {
                    var coupon = await _context.Coupons.FirstOrDefaultAsync(c => c.CouponID == order.CouponID);
                    coupon.quantity += 1;
                    _context.Coupons.Update(coupon);
                }
                _context.Update(order);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
