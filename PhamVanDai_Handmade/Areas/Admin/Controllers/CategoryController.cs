using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PhamVanDai_Handmade.Models;
using PhamVanDai_Handmade.Repository;
using System.Globalization;
using System.Drawing.Printing;
using X.PagedList.Extensions;

namespace PhamVanDai_Handmade.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly DataContext _context;

        public CategoryController(DataContext context)
        {
            _context = context;
        }

        // GET: Admin/Category
        public async Task<IActionResult> Index(string? categoryName, int? status, int page = 1)
        {
            int pageSize = 10;
            var query = _context.Categories.Where(c => !c.IsDeleted).AsQueryable();
            if (!string.IsNullOrEmpty(categoryName))
            {
                query = query.Where(c => c.CategoryName.Contains(categoryName));
            }
            if (status.HasValue)
            {
                query = query.Where(c => c.Status == status.Value);
            }
            var categories = query
                .OrderByDescending(c => c.CategoryID) // sắp xếp mới nhất
                .ToPagedList(page, pageSize);
            return View(categories);
        }

        // GET: Admin/Category/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoryModel = await _context.Categories
                .FirstOrDefaultAsync(m => m.CategoryID == id);
            if (categoryModel == null)
            {
                return NotFound();
            }

            return View(categoryModel);
        }

        // GET: Admin/Category/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Category/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CategoryID,CategoryName,Slug,Description,Status,IsDeleted")] CategoryModel category)
        {
            if (ModelState.IsValid)
            {
                category.Slug = category.CategoryName.Replace(" ", "-");

                var slugExists = await _context.Categories.AnyAsync(p => p.Slug == category.Slug);
                if (slugExists)
                {
                    ModelState.AddModelError("", "Danh mục đã tồn tại");
                    return View(category);
                }

                _context.Add(category);
                await _context.SaveChangesAsync();

                TempData["success"] = "Thêm danh mục thành công";
                return RedirectToAction("Index");
            }

            return View(category);
        }
 


        // GET: Admin/Category/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoryModel = await _context.Categories.FindAsync(id);
            if (categoryModel == null)
            {
                return NotFound();
            }
            return View(categoryModel);
        }

        // POST: Admin/Category/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Edit(int id, CategoryModel category)
        {
            if (id != category.CategoryID) return NotFound();

            if (ModelState.IsValid)
            {
                // Lấy category hiện tại trong DB
                var existingCategory = await _context.Categories.AsNoTracking().FirstOrDefaultAsync(c => c.CategoryID == id);

                if (existingCategory == null)
                    return NotFound();

                // Nếu Name bị thay đổi
                if (!string.Equals(existingCategory.CategoryName, category.CategoryName, StringComparison.OrdinalIgnoreCase))
                {
                    category.Slug = category.CategoryName.Replace(" ", "-");

                    // Kiểm tra slug có trùng không (ngoại trừ chính nó)
                    var slugExists = await _context.Categories
                        .AnyAsync(c => c.Slug == category.Slug && c.CategoryID != id);

                    if (slugExists)
                    {
                        ModelState.AddModelError("", "Tên danh mục đã tồn tại");
                        return View(category);
                    }
                }
                else
                {
                    // Giữ nguyên slug cũ nếu tên không đổi
                    category.Slug = existingCategory.Slug;
                }

                try
                {
                    _context.Update(category);
                    await _context.SaveChangesAsync();
                    TempData["success"] = "Cập nhật danh mục thành công";
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Categories.Any(e => e.CategoryID == category.CategoryID))
                        return NotFound();
                    else
                        throw;
                }
            }

            TempData["error"] = "Có lỗi khi cập nhật danh mục";
            return View(category);
        }

        // GET: Admin/Category/Trash
        [HttpGet]
        public async Task<IActionResult> Trash()
        {
            return View( await _context.Categories.Where(c => c.IsDeleted).ToListAsync());
        }

        // POST: Admin/Category/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var categoryModel = await _context.Categories.FindAsync(id);
            if (categoryModel != null)
            {
                categoryModel.IsDeleted = true; // Xóa mềm
                _context.Update(categoryModel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        //Xóa vĩnh viễn
        [HttpPost]
        public async Task<IActionResult> DeletePermen(int id)
        {
            var delete = await _context.Categories.FindAsync(id);

            if (delete == null)
            {
                TempData["error"] = "Danh mục không tồn tại";
                return RedirectToAction(nameof(Trash));
            }

            _context.Categories.Remove(delete);
            await _context.SaveChangesAsync();

            TempData["success"] = "Xóa vĩnh viễn thành công";
            return RedirectToAction(nameof(Trash));
        }

        // Khôi phục
        [HttpPost]
        public async Task<IActionResult> Restore(int id)
        {
            var categoryModel = await _context.Categories.FindAsync(id);
            if (categoryModel != null)
            {
                categoryModel.IsDeleted = false; // Xóa mềm
                _context.Update(categoryModel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Trash));
        }

        private bool CategoryModelExists(int id)
        {
            return _context.Categories.Any(e => e.CategoryID == id);
        }
    }
}
