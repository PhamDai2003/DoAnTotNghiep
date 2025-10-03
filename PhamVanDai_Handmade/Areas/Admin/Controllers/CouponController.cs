using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhamVanDai_Handmade.Models;
using PhamVanDai_Handmade.Repository;
using X.PagedList.Extensions;
namespace PhamVanDai_Handmade.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CouponController : Controller
    {
        private readonly DataContext _context;

        public CouponController(DataContext context)
        {
            _context = context;
        }

        // GET: Admin/Coupon
        public ActionResult Index(string? code, DateTime? date, int? status, int? page)
        {
            int pageSize = 10; // số bản ghi trên 1 trang
            int pageNumber = page ?? 1;

            var query = _context.Coupons.Where(c => !c.IsDeleted);
            if (!string.IsNullOrEmpty(code))
            {
                query = query.Where(c => c.Code.Contains(code));
            }
            if (date.HasValue)
            {
                if(date < query.)
            }

            var coupons =  _context.Coupons
                .Where(c => !c.IsDeleted)
                .OrderByDescending(c => c.CouponID) // sắp xếp mới nhất lên trước
                .ToPagedList(pageNumber, pageSize);

            return View(coupons);
        }

        // GET: Admin/Coupon/Details/5
        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null) return NotFound();

            var coupon = await _context.Coupons.FirstOrDefaultAsync(m => m.CouponID == id);
            if (coupon == null) return NotFound();

            return View(coupon);
        }

        // GET: Admin/Coupon/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Coupon/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CouponModel coupon)
        {
            if (ModelState.IsValid)
            {
                // Check trùng code
                var exists = await _context.Coupons.AnyAsync(c => c.Code == coupon.Code);
                if (exists)
                {
                    ModelState.AddModelError("", "Mã giảm giá đã tồn tại");
                    return View(coupon);
                }

                _context.Add(coupon);
                await _context.SaveChangesAsync();

                TempData["success"] = "Thêm mã giảm giá thành công";
                return RedirectToAction(nameof(Index));
            }

            return View(coupon);
        }

        // GET: Admin/Coupon/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var coupon = await _context.Coupons.FindAsync(id);
            if (coupon == null) return NotFound();

            return View(coupon);
        }

        // POST: Admin/Coupon/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CouponModel coupon)
        {
            if (id != coupon.CouponID) return NotFound();

            if (ModelState.IsValid)
            {
                var existingCoupon = await _context.Coupons.AsNoTracking().FirstOrDefaultAsync(c => c.CouponID == id);
                if (existingCoupon == null) return NotFound();

                // Nếu code thay đổi thì check trùng
                if (!string.Equals(existingCoupon.Code, coupon.Code, StringComparison.OrdinalIgnoreCase))
                {
                    var exists = await _context.Coupons.AnyAsync(c => c.Code == coupon.Code && c.CouponID != id);
                    if (exists)
                    {
                        ModelState.AddModelError("", "Mã giảm giá đã tồn tại");
                        return View(coupon);
                    }
                }

                try
                {
                    _context.Update(coupon);
                    await _context.SaveChangesAsync();
                    TempData["success"] = "Cập nhật mã giảm giá thành công";
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CouponModelExists(coupon.CouponID))
                        return NotFound();
                    else
                        throw;
                }
            }

            TempData["error"] = "Có lỗi khi cập nhật mã giảm giá";
            return View(coupon);
        }

        // GET: Admin/Coupon/Trash
        [HttpGet]
        public async Task<IActionResult> Trash()
        {
            return View(await _context.Coupons.Where(c => c.IsDeleted).ToListAsync());
        }

        // POST: Admin/Coupon/Delete/5 (xóa mềm)
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var coupon = await _context.Coupons.FindAsync(id);
            if (coupon != null)
            {
                coupon.IsDeleted = true;
                _context.Update(coupon);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        // POST: Admin/Coupon/DeletePermen (xóa vĩnh viễn)
        [HttpPost]
        public async Task<IActionResult> DeletePermen(int id)
        {
            var coupon = await _context.Coupons.FindAsync(id);
            if (coupon == null)
            {
                TempData["error"] = "Mã giảm giá không tồn tại";
                return RedirectToAction(nameof(Trash));
            }

            _context.Coupons.Remove(coupon);
            await _context.SaveChangesAsync();

            TempData["success"] = "Xóa vĩnh viễn thành công";
            return RedirectToAction(nameof(Trash));
        }

        // POST: Admin/Coupon/Restore (khôi phục)
        [HttpPost]
        public async Task<IActionResult> Restore(int id)
        {
            var coupon = await _context.Coupons.FindAsync(id);
            if (coupon != null)
            {
                coupon.IsDeleted = false;
                _context.Update(coupon);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Trash));
        }

        private bool CouponModelExists(int id)
        {
            return _context.Coupons.Any(e => e.CouponID == id);
        }
    }
}
