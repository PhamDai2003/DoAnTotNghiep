using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace PhamVanDai_Handmade.Repository.Components
{
    public class WishlistCountViewComponent : ViewComponent
    {
        private readonly DataContext _context;
        public WishlistCountViewComponent(DataContext context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            int count = 0;
            if (!string.IsNullOrEmpty(userId))
            {
                count = await _context.WishlistItems
                                      .CountAsync(w => w.UserID == userId);
            }
            return View(count);
        }
    }
}
