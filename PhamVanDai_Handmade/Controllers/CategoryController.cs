using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using PhamVanDai_Handmade.Repository;
using System.Security.Claims;

namespace PhamVanDai_Handmade.Controllers
{
    public class CategoryController : Controller
    {
        private readonly DataContext _context;
        public CategoryController(DataContext context)
        {
            _context = context;
        }

        [Route("caterogy/{slug}")]
        public async Task<IActionResult> Index(string slug)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(c => c.Slug == slug);
            ViewBag.categoryName = category.CategoryName;
            ViewBag.slug = slug;
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> getProductByCategory(string slug, int page = 1, int pageSize = 8)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(c => c.Slug == slug);
            if (category == null)
            {
                return NotFound();
            }
            var query = _context.Products
                        .Include(p => p.Category)
                        .Include(p => p.ProductVariants)
                        .Where(p => !p.isDeteled && p.Status == 1 && p.CategoryID == category.CategoryID);
            // Lấy danh sách ID sản phẩm đã yêu thích
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            List<int> wishlistedIds = new List<int>();
            if (!string.IsNullOrEmpty(userId))
            {
                wishlistedIds = await _context.WishlistItems
                                            .Where(w => w.UserID == userId)
                                            .Select(w => w.ProductID)
                                            .ToListAsync();
            }

            var totalItems = await query.CountAsync();
            var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);
            var products = await query
                            .Skip((page - 1) * pageSize)
                            .Take(pageSize)
                            .Select(p => new
                            {
                                p.ProductID,
                                p.ProductName,
                                CategoryName = p.Category.CategoryName,
                                p.Image,
                                IsWishlisted = wishlistedIds.Contains(p.ProductID),
                                Variants = p.ProductVariants.Select(v => new { v.Price })
                            }).ToListAsync();
            return Json(new
            {
                totalItems,
                page,
                totalPages = (int)Math.Ceiling((double)totalItems / pageSize),
                products
            });
        }
   
    }
}
