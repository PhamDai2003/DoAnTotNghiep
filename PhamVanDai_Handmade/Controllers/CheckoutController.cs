using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;
using PhamVanDai_Handmade.Models;
using PhamVanDai_Handmade.Models.ViewModels;
using PhamVanDai_Handmade.Repository;
using PhamVanDai_Handmade.Repository.Services.OpenStreetMap;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Net.payOS;
using Net.payOS.Types;
using System;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace PhamVanDai_Handmade.Controllers
{
    [Authorize]
    public class CheckoutController : Controller        
    {
        private readonly DataContext _context;
        private readonly OpenStreetMapService _osm;
        private readonly PayOS _payOS;
        private readonly ICompositeViewEngine _viewEngine;

        public CheckoutController(DataContext context, OpenStreetMapService osm, IConfiguration configuration, ICompositeViewEngine viewEngine)
        {
            _context = context;
            _osm = osm;
            _viewEngine = viewEngine;
            _payOS = new PayOS(
                configuration["PayOS:ClientId"],
                configuration["PayOS:ApiKey"],
                configuration["PayOS:ChecksumKey"]
            );
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateTotals(CheckoutViewModel model)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            model.CartItems = await _context.CartItems
                .Where(c => c.UserID == userId)
                .Select(c => new CartItemViewModel {
                    VariantID = c.VariantID,
                    ProductName = c.ProductVariant.Product.ProductName,
                    Color = c.ProductVariant.Color,
                    Size = c.ProductVariant.Size,
                    Quantity = c.Quantity,
                    Price = c.ProductVariant.Price
                })
                .ToListAsync();

            // 1. Tính phí ship nếu có chọn địa chỉ
            if (model.SelectedAddressId.HasValue)
            {
                var address = await _context.DeliveryAddresses.FindAsync(model.SelectedAddressId.Value);
                if (address != null && address.UserID == userId)
                {
                    model.ShippingFee = (address.Address.ToLower().Contains("hà nội")) ? 20000 : 50000;
                }
            }

            // 2. Áp dụng coupon nếu có
            if (!string.IsNullOrEmpty(model.CouponCode))
            {
                model.Discount = 0;
                model.CouponId = null;
                var coupon = await _context.Coupons.FirstOrDefaultAsync(c => c.Code == model.CouponCode && !c.IsDeleted && c.status == 1);
                if (coupon == null)
                {
                    model.CalculateTotals();
                    string view = await RenderPartialViewToString("_OrderSummaryPartial", model);
                    return Json(new { success = false, message = "Mã coupon không hợp lệ!", html = view });
                }
                else if (coupon.EndDate < DateTime.Now)
                {
                    model.CalculateTotals();
                    string view = await RenderPartialViewToString("_OrderSummaryPartial", model);
                    return Json(new { success = false, message = "Mã coupon đã hết hạn!",  html = view });
                }
                else if (coupon.MinOrderValue > model.SubTotal)
                {
                    model.CalculateTotals();
                    string view = await RenderPartialViewToString("_OrderSummaryPartial", model);
                    return Json(new { success = false, message = $"Đơn hàng tối thiểu để áp dụng mã này là {coupon.MinOrderValue:N0} VND!", html = view });
                }
                else if (coupon.quantity <= 0)
                {
                    model.CalculateTotals();
                    string view = await RenderPartialViewToString("_OrderSummaryPartial", model);
                    return Json(new { success = false, message = "Mã coupon đã hết lượt sử dụng!", html = view });
                }
                else
                {
                    model.Discount = coupon.DiscountAmount ?? 0;
                    model.CouponId = coupon.CouponID;
                }
            }

            // Luôn freeship nếu tổng tiền hàng > 500k
            if (model.CartItems.Sum(i => i.Total) >= 500000)
            {
                model.ShippingFee = 0;
            }

            // 3. Tính toán lại tổng cuối cùng
            model.CalculateTotals();

            // 4. Render partial view thành chuỗi và trả về JSON
            string html = await RenderPartialViewToString("_OrderSummaryPartial", model);
            return Json(new { success = true, html = html });
        }


        // Action này CHỈ để đặt hàng
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PlaceOrder(CheckoutViewModel model)
        {

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // 1. Lấy lại địa chỉ đã chọn từ DB để đảm bảo hợp lệ
            var shippingAddress = await _context.DeliveryAddresses.FirstOrDefaultAsync(a => a.AddressID == model.SelectedAddressId && a.UserID == userId);
            if (shippingAddress == null)
            {
                return Json(new { success = false, message = "Vui lòng chọn địa chỉ giao hàng hợp lệ." });
            }
            // ✅ QUAN TRỌNG: LẤY LẠI GIỎ HÀNG TRỰC TIẾP TỪ DATABASE
            var cartItemsFromDb = await _context.CartItems
                .Where(c => c.UserID == userId)
                .Include(c => c.ProductVariant)
                .ThenInclude(v => v.Product)
                .Select(c => new CartItemViewModel // Chuyển đổi sang ViewModel để tính toán
                {
                    VariantID = c.VariantID,
                    ProductName = c.ProductVariant.Product.ProductName,
                    Color = c.ProductVariant.Color,
                    Size = c.ProductVariant.Size,
                    Quantity = c.Quantity,
                    Price = c.ProductVariant.Price // Lấy giá mới nhất từ DB
                }).ToListAsync();
            // Kiểm tra nếu giỏ hàng rỗng
            if (!cartItemsFromDb.Any())
            {
                return Json(new { success = false, message = "Giỏ hàng của bạn đang trống. Không thể đặt hàng." });
            }
            // Gán giỏ hàng vừa lấy được vào model để xử lý tiếp
            model.CartItems = cartItemsFromDb;
            //if (model.Address == null || model.PhoneNumber == null)
            //{
            //    return View(model);
            //}
            


                var order = new OrderModel
                    {
                        UserID = userId,
                        OrderDate = DateTime.Now,
                        ShippingFree = model.ShippingFee,
                        AddressID = shippingAddress.AddressID,
                        Status = 0, // Đang xử lý
                        CouponID = model.CouponId,
                        TotalAmount = model.FinalTotal
                    };
            if (model.PaymentMethod == "PAYOS")
            {
                order.PaymentMethod = "Online";
                order.PaymentStatus = 1; // Chưa thanh toán
            }
            else
            {
                order.PaymentMethod = "COD";
                order.PaymentStatus = 1; // Chưa thanh toán
            }
                // Thêm vào DbContext
                _context.Orders.Add(order);
                    await _context.SaveChangesAsync(); // Lưu để lấy OrderID

                    // Tạo chi tiết đơn hàng từ CartItems
                    foreach (var item in model.CartItems)
                    {
                        var orderDetail = new OrderDetailModel
                        {
                            OrderID = order.OrderID,
                            VariantID = item.VariantID,
                            Quantity = item.Quantity,
                            Price = item.Price
                        };
                        _context.OrderDetails.Add(orderDetail);

                        //cập nhật lại số lượng sản phẩm tồn kho
                        var variant = await _context.ProductVariants.FindAsync(item.VariantID);
                        if (variant != null)
                        {

                            if (variant.Quantity >= item.Quantity)
                            {
                                variant.Quantity -= item.Quantity;
                                variant.SoldCount += item.Quantity;
                            }
                            else
                            {
                                TempData["Error"] = "Số lượng sản phẩm còn lại trong kho không đủ";
                                return View(model);
                            }
                        }
                    }
                    //cập nhật số lượng phiếu giảm giá
                    var coupon = await _context.Coupons.FirstOrDefaultAsync(c => c.CouponID == model.CouponId);
                    if (coupon != null)
                        coupon.quantity -= 1;

                    // Lưu tất cả
                    await _context.SaveChangesAsync();
                    // TODO: Xử lý thanh toán nếu chọn PayOS
                    if (model.PaymentMethod == "PAYOS")
                    {                     
                        // --- BẮT ĐẦU TÍCH HỢP PAYOS ---
                        List<ItemData> items = new List<ItemData>();
                        // 1. Chuẩn bị dữ liệu cho PayOS
                        var paymentData = new PaymentData(
                            order.OrderID, // Dùng ID đơn hàng vừa tạo làm orderCode
                            (int)order.TotalAmount, // Tổng tiền cuối cùng
                            "Thanh toan don hang #" + order.OrderID,
                            items,
                            // URL mà PayOS sẽ trả người dùng về sau khi thanh toán
                            $"{Request.Scheme}://{Request.Host}/Checkout/PaymentCancelled",
                            $"{Request.Scheme}://{Request.Host}/Checkout/PaymentSuccess"
                            
                        );
                        // 2. Gọi PayOS để tạo link thanh toán
                        CreatePaymentResult createPaymentResult = await _payOS.createPaymentLink(paymentData);

                        // 3. Trả về JSON chứa link thanh toán cho JavaScript xử lý
                        return Json(new { success = true, isRedirect = true, redirectUrl = createPaymentResult.checkoutUrl });
                    }
                    else
                    {
                        // Xóa giỏ hàng sau khi đặt hàng
                        var cartItems = _context.CartItems.Where(c => c.UserID == userId);
                        _context.CartItems.RemoveRange(cartItems);
                        await _context.SaveChangesAsync();
                        TempData["Success"] = "Đặt hàng thành công!";

                        return Json(new { success = true, isRedirect = false, redirectUrl = Url.Action("Detail", "Order", new { id = order.OrderID }) });
                    }
        }

        // Thêm action cho trang cảm ơn
        public IActionResult OrderSuccess(int id)
        {
            ViewBag.OrderId = id;
            return View();
        }



        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var cartItems = await _context.CartItems
                .Where(c => c.UserID == userId)
                .Include(c => c.ProductVariant)
                .ThenInclude(v => v.Product)
                .Select(c => new CartItemViewModel
                {
                    VariantID = c.VariantID,
                    ProductName = c.ProductVariant.Product.ProductName,
                    Color = c.ProductVariant.Color,
                    Size = c.ProductVariant.Size,
                    Quantity = c.Quantity,
                    Price = c.ProductVariant.Price
                }).ToListAsync();
            var subtotal = cartItems.Sum(c => c.Quantity * c.Price);

            // lấy danh sách địa chỉ đã lưu của người dùng
            var addressList = await _context.DeliveryAddresses.Where(da => da.UserID == userId).ToListAsync();

            
            var model = new CheckoutViewModel
            {
                CartItems = cartItems,
                SavedAddresses = addressList,
                ShippingFee = 0, // mặc định, sẽ tính sau khi nhập địa chỉ
                Discount = 0,
                SubTotal = subtotal,
                FinalTotal = subtotal,
            };

            return View(model);
        }

        // THÊM 2 ACTION MỚI ĐỂ XỬ LÝ KẾT QUẢ TỪ PAYOS
        [HttpGet]
        public async Task<IActionResult> PaymentSuccess(int orderCode) 
        {
            try
            {
                
                // 1. Lấy thông tin thanh toán từ PayOS
                PaymentLinkInformation paymentInfo = await _payOS.getPaymentLinkInformation(orderCode);

                // 2. Chỉ xử lý khi trạng thái là PAID
                if (paymentInfo != null && paymentInfo.status == "PAID")
                {
                    // 3. Tìm đơn hàng trong DB bằng chính orderCode đó
                    var order = await _context.Orders.FirstOrDefaultAsync(o => o.OrderID == orderCode);

                    // Chỉ cập nhật nếu đơn hàng tồn tại và chưa được xử lý
                    if (order != null && order.PaymentStatus != 2) // Giả sử 2 = Paid
                    {
                        order.PaymentStatus = 2; // Cập nhật trạng thái đã thanh toán

                        // Xóa giỏ hàng của người dùng đã đặt đơn hàng này
                        var cartItemsToRemove = await _context.CartItems
                                                              .Where(c => c.UserID == order.UserID)
                                                              .ToListAsync(); // Tải danh sách vào bộ nhớ

                        if (cartItemsToRemove.Any())
                        {
                            _context.CartItems.RemoveRange(cartItemsToRemove);
                        }

                        // Lưu tất cả thay đổi (cập nhật status và xóa giỏ hàng)
                        await _context.SaveChangesAsync();
                        TempData["Success"] = "Thanh toán và đặt hàng thành công!";
                        return RedirectToAction("Detail", "Order", new { id = order.OrderID });
                    }
                    order.PaymentStatus = 4; // thất bại
                    TempData["Error"] = "Thanh toán chưa được hoàn tất hoặc đang chờ xử lý.";
                    // Chuyển hướng về trang giỏ hàng để người dùng kiểm tra lại
                    return RedirectToAction("Index", "Cart");              
                }
                else
                {
                    // Nếu trạng thái không phải PAID (ví dụ: PROCESSING, PENDING)
                    TempData["Error"] = "Thanh toán chưa được hoàn tất hoặc đang chờ xử lý.";
                    // Chuyển hướng về trang giỏ hàng để người dùng kiểm tra lại
                    return RedirectToAction("Index", "Cart");
                }
            }
            catch (Exception ex)
            {
                // Ghi lại lỗi để bạn có thể kiểm tra
                Console.WriteLine($"Error processing payment success for orderCode {orderCode}: {ex.Message}");
                TempData["Error"] = "Có lỗi xảy ra trong quá trình xác thực thanh toán. Vui lòng liên hệ hỗ trợ.";
                return RedirectToAction("Index", "Cart");
            }
        }

        [HttpGet]
        public async Task<IActionResult> PaymentCancelled(int orderCode)
        {
            try
            {
                // 1. Tìm đơn hàng cần hủy trong database
                // Dùng .Include() để tải kèm các dữ liệu liên quan cần xử lý
                var orderToCancel = await _context.Orders
                    .Include(o => o.OrderDetails) // Lấy danh sách chi tiết đơn hàng
                        .ThenInclude(od => od.ProductVariant) // Từ chi tiết, lấy thông tin biến thể sản phẩm
                    .FirstOrDefaultAsync(o => o.OrderID == orderCode);

                // 2. Chỉ xử lý nếu đơn hàng tồn tại và đang ở trạng thái "Chờ thanh toán"
                if (orderToCancel != null && orderToCancel.PaymentStatus == 1) // Giả sử Status 1 là "PendingPayment"
                {
                    // 3. HOÀN LẠI SỐ LƯỢNG TỒN KHO
                    foreach (var detail in orderToCancel.OrderDetails)
                    {
                        // detail.ProductVariant đã được tải sẵn nhờ .ThenInclude()
                        var variant = detail.ProductVariant;
                        if (variant != null)
                        {
                            variant.Quantity += detail.Quantity; // Cộng trả lại số lượng đã trừ
                            variant.SoldCount -= detail.Quantity; // Trừ đi số lượng "đã bán" (nếu có)
                        }
                    }

                    // 4. HOÀN LẠI LƯỢT SỬ DỤNG COUPON (nếu có)
                    if (orderToCancel.CouponID.HasValue)
                    {
                        var coupon = await _context.Coupons.FindAsync(orderToCancel.CouponID.Value);
                        if (coupon != null)
                        {
                            coupon.quantity += 1;
                        }
                    }

                    // 5. XÓA CÁC BẢN GHI LIÊN QUAN
                    // Luôn xóa các bản ghi con (chi tiết) trước
                    _context.OrderDetails.RemoveRange(orderToCancel.OrderDetails);

                    // Sau đó mới xóa bản ghi cha (đơn hàng)
                    _context.Orders.Remove(orderToCancel);

                    // 6. LƯU TẤT CẢ THAY ĐỔI VÀO DATABASE
                    await _context.SaveChangesAsync();

                    TempData["Error"] = $"Đơn hàng #{orderCode} đã được hủy vì chưa hoàn tất thanh toán.";
                }
                else
                {
                    TempData["Error"] = $"Đơn hàng #{orderCode} đã được xử lý trước đó hoặc không tồn tại.";
                }
            }
            catch (Exception ex)
            {
                // Ghi lại lỗi để bạn có thể kiểm tra (quan trọng)
                Console.WriteLine($"Error cancelling order {orderCode}: {ex.Message}");
                TempData["Error"] = "Có lỗi xảy ra trong quá trình hủy đơn hàng.";
            }

            // Luôn chuyển hướng người dùng về trang giỏ hàng để họ có thể thử lại
            return RedirectToAction("Index", "CartItem");
        }

        // Thêm phương thức helper này vào cuối Controller của bạn
        private async Task<string> RenderPartialViewToString(string viewName, object model)
        {
            if (string.IsNullOrEmpty(viewName))
                viewName = ControllerContext.ActionDescriptor.ActionName;
            ViewData.Model = model;
            using (var sw = new StringWriter())
            {
                var viewResult = _viewEngine.FindView(ControllerContext, viewName, false);
                var viewContext = new ViewContext(ControllerContext, viewResult.View, ViewData, TempData, sw, new HtmlHelperOptions());
                await viewResult.View.RenderAsync(viewContext);
                return sw.ToString();
            }
        }
    }
}


