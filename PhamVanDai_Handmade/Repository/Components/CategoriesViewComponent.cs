using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace PhamVanDai_Handmade.Repository.Components
{
    public class CategoriesViewComponent : ViewComponent
    {
        private readonly DataContext _context;
        public CategoriesViewComponent(DataContext context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var categories = await _context.Categories.Where(c => c.Status == 1).ToListAsync();
            return View(categories);
        }
    }
}
