using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhamVanDai_Handmade.Models;
using PhamVanDai_Handmade.Models.ViewModels;
using PhamVanDai_Handmade.Repository;
using System.Security.Claims;

namespace PhamVanDai_Handmade.Controllers
{
    [Authorize]
    public class CartItemController : Controller
    {
        private readonly DataContext _context;
        public CartItemController(DataContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var cartItems = await _context.CartItems
                .Where(c => c.UserID == userId)
                .Include(c => c.ProductVariant)
                .ThenInclude(pv => pv.Product)
                .ToListAsync();
            var viewModel = cartItems.Select(c => new CartItemViewModel
            {
                VariantID = c.VariantID,
                ProductName = c.ProductVariant.Product.ProductName,
                Image = c.ProductVariant.Image,
                Color = c.ProductVariant.Color,
                Size = c.ProductVariant.Size,
                Price = c.ProductVariant.Price,
                Quantity = c.Quantity,
                MaxQuantity = c.ProductVariant.Quantity
            }).ToList();
            return View(viewModel);
        }
            

        [HttpPost]
        public async Task<IActionResult> AddToCart(int productVariantId, int quantity)
        {
            // lấy thông tin biến thể sản phẩm và người dùng
            var variant = await _context.ProductVariants.FindAsync(productVariantId);
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (variant == null || userId == null)
                return NotFound();

            //Kiểm tra tồn kho
            if (quantity > variant.Quantity)
            {
                TempData["Error"] = "Số lượng yêu cầu vượt quá số lượng tồn kho";
                return RedirectToAction("Detail", "Product", new { id = variant.ProductID });
            }

            //Tìm xem biến thể đã có trong giỏ hàng chưa
            var cartItem = await _context.CartItems.FirstOrDefaultAsync(c => c.VariantID == productVariantId && c.UserID == userId);
            if (cartItem == null)
            {
                cartItem = new CartItemModel()
                {
                    UserID = userId,
                    VariantID = productVariantId,
                    Quantity = quantity
                };
                _context.CartItems.Add(cartItem);
            }
            else
            {
                // Nếu đã có, kiểm tra thêm tồn kho trước khi cộng 
                if (cartItem.Quantity + quantity > variant.Quantity)
                {
                    TempData["Error"] = "Tổng số lượng trong giỏ vượt quá số lượng tồn kho";
                    return RedirectToAction("Detail", "Product", new { id = variant.ProductID });
                }
                cartItem.Quantity += quantity;
            }
            await _context.SaveChangesAsync();
            TempData["Success"] = "Đã thêm sản phẩm vào giỏ";
            return RedirectToAction("Detail", "Product", new { id = variant.ProductID });
        }

        // Action để cập nhật số lượng
        [HttpPost]
        public async Task<IActionResult> UpdateQuantity(int variantId, int newQuantity)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var cartItem = await _context.CartItems
                .Include(c => c.ProductVariant)
                .FirstOrDefaultAsync(c => c.VariantID == variantId && c.UserID == userId);

            if (cartItem == null)
            {
                return Json(new CartUpdateResult { Success = false, Message = "Không tìm thấy sản phẩm trong giỏ." });
            }

            if (newQuantity <= 0)
            {
                _context.CartItems.Remove(cartItem);
                await _context.SaveChangesAsync();

                var cartTotal = await _context.CartItems
                    .Where(c => c.UserID == userId)
                    .SumAsync(c => c.Quantity * c.ProductVariant.Price);

                return Json(new CartUpdateResult
                {
                    Success = true,
                    Removed = true,
                    VariantID = variantId,
                    CartTotal = cartTotal
                });
            }

            if (newQuantity > cartItem.ProductVariant.Quantity)
            {
                cartItem.Quantity = cartItem.ProductVariant.Quantity;
                await _context.SaveChangesAsync();

                var cartTotal = await _context.CartItems
                    .Where(c => c.UserID == userId)
                    .SumAsync(c => c.Quantity * c.ProductVariant.Price);

                return Json(new CartUpdateResult
                {
                    Success = false,
                    VariantID = cartItem.VariantID,
                    Quantity = cartItem.Quantity,
                    Total = cartItem.Quantity * cartItem.ProductVariant.Price,
                    Message = "Số lượng vượt quá tồn kho.",
                    CartTotal = cartTotal
                });
            }

            cartItem.Quantity = newQuantity;
            await _context.SaveChangesAsync();

            var totalCart = await _context.CartItems
                .Where(c => c.UserID == userId)
                .SumAsync(c => c.Quantity * c.ProductVariant.Price);

            return Json(new CartUpdateResult
            {
                Success = true,
                VariantID = cartItem.VariantID,
                Quantity = cartItem.Quantity,
                Total = cartItem.Quantity * cartItem.ProductVariant.Price,
                CartTotal = totalCart
            });
        }

        // Action để xóa
        [HttpPost]
        public async Task<IActionResult> Remove(int variantId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var cartItem = await _context.CartItems
                .FirstOrDefaultAsync(c => c.VariantID == variantId && c.UserID == userId);

            if (cartItem != null)
            {
                _context.CartItems.Remove(cartItem);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }
    }
}
