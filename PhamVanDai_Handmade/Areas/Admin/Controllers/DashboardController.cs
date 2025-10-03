using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhamVanDai_Handmade.Models;
using PhamVanDai_Handmade.Models.ViewModels;
using System;
using System.Linq;
using System.Threading.Tasks;
using PhamVanDai_Handmade.Repository;

namespace PhamVanDai_Handmade.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DashboardController : Controller
    {
        private readonly DataContext _context;

        public DashboardController(DataContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(int? month, int? year)
        {
            var now = DateTime.Now;
            int selectedMonth = month ?? now.Month;
            int selectedYear = year ?? now.Year;

            // lọc theo tháng & năm
            var orders = await _context.Orders
                .Include(o => o.OrderDetails)
                    .ThenInclude(d => d.ProductVariant)
                        .ThenInclude(v => v.Product)
                .Where(o => o.OrderDate.Month == selectedMonth && o.OrderDate.Year == selectedYear)
                .ToListAsync();
            // Doanh thu kỳ vọng: tất cả đơn không bị hủy hoặc hoàn trả
            decimal expectedRevenue = orders
                .Where(o => o.Status != 5 && o.Status != 4)
                .Sum(o => o.TotalAmount);

            // Doanh thu thực nhận: chỉ đơn đã hoàn thành & đã thanh toán
            decimal actualRevenue = orders
                .Where(o => o.Status == 3 && o.PaymentStatus == 2)
                .Sum(o => o.TotalAmount);

            // Số lượng đơn hàng
            int totalOrders = orders.Count();

            // Số khách hàng đăng ký trong tháng
            int newCustomers = await _context.Users
                .CountAsync(u => u.CreatedDate.Month == selectedMonth && u.CreatedDate.Year == selectedYear);

            // Trạng thái đơn hàng
            var orderStatusStats = orders
                .GroupBy(o => o.Status)
                .Select(g => new { Status = g.Key, Count = g.Count() })
                .ToList();

            // đảm bảo có đủ 0–5
            var statusList = Enumerable.Range(0, 6)
                .Select(i => new
                {
                    Status = i,
                    Count = orderStatusStats.FirstOrDefault(x => x.Status == i)?.Count ?? 0
                })
                .ToList();

            // Top 5 sản phẩm bán chạy
            var topProducts = orders
                .SelectMany(o => o.OrderDetails)
                .GroupBy(d => d.ProductVariant.Product.ProductName)
                .Select(g => new
                {
                    ProductName = g.Key,
                    QuantitySold = g.Sum(x => x.Quantity)
                })
                .OrderByDescending(x => x.QuantitySold)
                .Take(5)
                .ToList();

            ViewBag.Month = selectedMonth;
            ViewBag.Year = selectedYear;
            ViewBag.ExpectedRevenue = expectedRevenue;
            ViewBag.ActualRevenue = actualRevenue;
            ViewBag.TotalOrders = totalOrders;
            ViewBag.NewCustomers = newCustomers;
            ViewBag.StatusList = statusList;
            ViewBag.TopProducts = topProducts;
            ViewBag.FileName = $"baocao-thongke-thang-{selectedMonth}-nam-{selectedYear}.pdf";

            return View();
        }
    }
}
