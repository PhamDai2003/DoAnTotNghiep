using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhamVanDai_Handmade.Models;
using PhamVanDai_Handmade.Models.ViewModels;
using PhamVanDai_Handmade.Repository;
using System.Net;
using System.Security.Claims;

namespace PhamVanDai_Handmade.Controllers
{
    public class ProductController : Controller
    {
        private readonly DataContext _context;
        public ProductController(DataContext context)
        {
            _context = context;
        }
        // Trang Index hiển thị view rỗng, dữ liệu sẽ load bằng Ajax
        public IActionResult Index()
        {
            ViewBag.Categories = _context.Categories.Where(c => !c.IsDeleted && c.Status == 1).ToList();
            return View();
        }

        // API trả về JSON danh sách sản phẩm (có lọc & sort)
        [HttpGet]
        public async Task<IActionResult> GetProducts(
            int? categoryId,
            string? color,
            string? size,
            string? sortOrder, // newest, oldest
            int page = 1,
            int pageSize = 8
        )
        {
            var query = _context.Products
                        .Include(p => p.Category)
                        .Include(p => p.ProductVariants)
                        .Where(p => !p.isDeteled && p.Status == 1)
                        .AsQueryable();

            // --- LOGIC LẤY WISHLIST ---
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            List<int> wishlistedIds = new List<int>();
            if (!string.IsNullOrEmpty(userId))
            {
                wishlistedIds = await _context.WishlistItems
                                            .Where(w => w.UserID == userId)
                                            .Select(w => w.ProductID)
                                            .ToListAsync();
            }
            // Lọc theo Category
            if (categoryId.HasValue)
            {
                query = query.Where(p => p.CategoryID == categoryId.Value);
            }

            // Lọc theo Color
            if (!string.IsNullOrEmpty(color))
            {
                query = query.Where(p => p.ProductVariants.Any(v => v.Color == color));
            }

            // Lọc theo Size
            if (!string.IsNullOrEmpty(size))
            {
                query = query.Where(p => p.ProductVariants.Any(v => v.Size == size));
            }

            // Sắp xếp
            query = sortOrder switch
            {
                "newest" => query.OrderByDescending(p => p.CreateAt),
                "oldest" => query.OrderBy(p => p.CreateAt),
                _ => query.OrderByDescending(p => p.CreateAt) // mặc định: mới nhất
            };

            // Tổng số sản phẩm
            var totalItems = await query.CountAsync();

            // Phân trang
            var products = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(p => new
                {
                    p.ProductID,
                    p.ProductName,
                    CategoryName = p.Category.CategoryName,
                    p.Image,
                    p.CreateAt,
                    IsWishlisted = wishlistedIds.Contains(p.ProductID),
                    Variants = p.ProductVariants.Select(v => new
                    {
                        v.VariantID,
                        v.Color,
                        v.Size,
                        v.Price,
                        v.Quantity
                    })
                }).ToListAsync();

            return Json(new
            {
                totalItems,
                page,
                pageSize,
                totalPages = (int)Math.Ceiling((double)totalItems / pageSize),
                products
            });
        }


        public async Task<IActionResult> Detail(int id)
        {
            // Dùng .Include() để tải thông tin Category của sản phẩm cha
            var product = await _context.Products
                                        .Include(p => p.Category)
                                        .FirstOrDefaultAsync(p => p.ProductID == id);

            if (product == null)
            {
                return NotFound();
            }

            // DÙNG .Include() ĐỂ LẤY CÁC BIẾN THỂ KÈM THEO
            // Đây là bước quan trọng nhất để Model.Variants không bị null
            var variants = await _context.ProductVariants
                                 .Where(v => v.ProductID == id)
                                 .ToListAsync();

            // --- THAY ĐỔI Ở ĐÂY ---
            // Tạo một danh sách các đối tượng "an toàn" để gửi xuống view
            var variantsForJson = variants.Select(v => new
            {
                // Chỉ chọn những trường cần thiết cho JavaScript
                v.VariantID,
                v.ProductID,
                v.Color,
                v.Size,
                v.Price,
                v.Quantity,
                v.Image
            }).ToList();



            // Lấy danh sách màu và size duy nhất từ các biến thể đã tải
            var availableColors = variants.Where(v => !string.IsNullOrEmpty(v.Color)).Select(v => v.Color).Distinct().ToList();
            var availableSizes = variants.Where(v => !string.IsNullOrEmpty(v.Size)).Select(v => v.Size).Distinct().ToList();

            // Lấy sản phẩm liên quan
            var relatedProducts = await _context.Products
                .Where(p => p.CategoryID == product.CategoryID && p.ProductID != id && !p.isDeteled && p.Status == 1)
                .Take(4)
                .ToListAsync();
            // Lấy đánh giá sản phẩm
            var reviews = await _context.Reviews
                                        .Include(r => r.User)
                                        .Where(r => r.ProductID == id && !r.IsDeleted)
                                        .OrderByDescending(r => r.CreatedAt)
                                        .ToListAsync();
            var viewModel = new ProductDetailViewModel
            {
                Product = product,
                Variants = variants,
                ReviewProducts = reviews,
                // Gán danh sách "an toàn" vào ViewModel
                VariantsForJson = variantsForJson, // Một thuộc tính mới
                AvailableColors = variants.Select(v => v.Color).Distinct().ToList(),
                AvailableSizes = variants.Select(v => v.Size).Distinct().ToList(),
                RelatedProducts = relatedProducts
            };


            return View(viewModel);
        }


        // Action 1: Trả về View "vỏ" cho trang kết quả tìm kiếm
        // URL: /Product/SearchResults?search=...
        public IActionResult Search(string search)
        {
            ViewBag.SearchKeyword = search;
            return View();
        }

        // Action 2: API trả về dữ liệu JSON, được gọi bằng AJAX
        // URL: /Product/SearchApi?search=...&page=...
        [HttpGet]
        public async Task<IActionResult> SearchApi(string search, int page = 1, int pageSize = 8)
        {
            string decodedSearch = !string.IsNullOrEmpty(search) ? WebUtility.HtmlDecode(search) : search;
            var query = _context.Products
                .Include(p => p.ProductVariants)
                .Where(p => p.Status == 1 && !p.isDeteled);

            if (!string.IsNullOrEmpty(decodedSearch))
            {
                query = query.Where(p => p.ProductName.Contains(decodedSearch));
            }

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
            var products = await query
                .OrderByDescending(p => p.CreateAt)
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

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddReview(int productId, int rating, string comment)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // Kiểm tra xem user đã mua sản phẩm chưa
            bool hasPurchased = await _context.Orders
                .Where(o => o.UserID == userId && o.Status == 3) // 4 = Đã giao thành công
                .AnyAsync(o => o.OrderDetails.Any(od => od.ProductVariant.ProductID == productId));

            if (!hasPurchased)
            {
                TempData["Error"] = "Bạn chỉ có thể đánh giá sản phẩm sau khi đã mua và hoàn thành đơn hàng.";
                return RedirectToAction("Detail", "Product", new { id = productId });
            }

            // Nếu đã mua thì cho đánh giá
            var review = new ReviewModel
            {
                ProductID = productId,
                UserID = userId,
                Rating = rating,
                Content = comment,
                CreatedAt = DateTime.Now
            };

            _context.Reviews.Add(review);
            await _context.SaveChangesAsync();

            return RedirectToAction("Detail", "Product", new { id = productId });
        }
    }
}

