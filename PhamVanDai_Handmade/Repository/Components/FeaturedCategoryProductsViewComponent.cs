using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhamVanDai_Handmade.Models;
using PhamVanDai_Handmade.Models.ViewModels;
using System.Security.Claims;

namespace PhamVanDai_Handmade.Repository.Components
{
    public class FeaturedCategoryProductsViewComponent : ViewComponent
    {
        private readonly DataContext _context;
        public FeaturedCategoryProductsViewComponent(DataContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            List<int> wishlistedIds = new List<int>();
            if (!string.IsNullOrEmpty(userId))
            {
                wishlistedIds = await _context.WishlistItems
                                            .Where(w => w.UserID == userId)
                                            .Select(w => w.ProductID)
                                            .ToListAsync();
            }
            // Truyền danh sách này xuống View qua ViewData
            ViewData["WishlistedIds"] = wishlistedIds;
            // 1. Lấy ra top category bán chạy dựa vào OrderDetails -> ProductVariant -> Product
            var topCategoryIds = await _context.OrderDetails
                .Include(od => od.ProductVariant)
                    .ThenInclude(pv => pv.Product)
                .GroupBy(od => od.ProductVariant.Product.CategoryID)
                .Select(g => new {
                    CategoryId = g.Key,
                    TotalSold = g.Sum(od => od.Quantity)
                })
                .OrderByDescending(x => x.TotalSold)
                .Take(3)
                .Select(x => x.CategoryId)
                .ToListAsync();

            var result = new List<FeaturedCategoryViewModel>();

            // 2. Nếu có category bán chạy
            if (topCategoryIds.Any())
            {
                foreach (var categoryId in topCategoryIds)
                {
                    var categoryInfo = await _context.Categories.FindAsync(categoryId);

                    // Lấy top sản phẩm bán chạy trong category này
                    var topProducts = await _context.Products
                        .Where(p => p.CategoryID == categoryId && !p.isDeteled && p.Status == 1)
                        .Include(P => P.Category)
                        .OrderByDescending(p => p.ProductVariants
                            .SelectMany(v => v.OrderDetails)
                            .Sum(od => od.Quantity))
                        .Take(8)
                        .ToListAsync();

                    result.Add(new FeaturedCategoryViewModel
                    {
                        featuredCategory = categoryInfo,
                        FeaturedProducts = topProducts
                    });
                }
            }
            else
            {
                // 3. Nếu chưa có đơn hàng => fallback: lấy 8 sản phẩm mới nhất
                var latestProducts = await _context.Products
                    .Where(p => !p.isDeteled && p.Status == 1)
                    .Include(P => P.Category)
                    .OrderByDescending(p => p.ProductID)
                    .Take(8)
                    .ToListAsync();

                if (latestProducts.Any())
                {
                    result.Add(new FeaturedCategoryViewModel
                    {
                        featuredCategory = new CategoryModel
                        {
                            CategoryID = 0,
                            CategoryName = "Hàng Mới Về"
                        },
                        FeaturedProducts = latestProducts
                    });
                }
            }

            return View(result);
        }
    }
}
