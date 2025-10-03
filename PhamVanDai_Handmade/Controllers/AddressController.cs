using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhamVanDai_Handmade.Models; // Namespace model của bạn
using PhamVanDai_Handmade.Repository;
using System.Security.Claims;
using System.Threading.Tasks;


namespace PhamVanDai_Handmade.Controllers
{
    [Authorize]
    public class AddressController : Controller
    {
        private readonly DataContext _context;

        public AddressController(DataContext context)
        {
            _context = context;
        }

        // GET: /Address/Create
        // Action này chỉ để hiển thị form trống
        public IActionResult Create()
        {
            return View();
        }

        // POST: /Address/Create
        // Action này nhận dữ liệu từ form và lưu vào database
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DeliveryAddressModel model)
        {
            // Lấy ID của người dùng đang đăng nhập
            model.UserID = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // ModelState.IsValid sẽ kiểm tra các yêu cầu [Required] trong model
            if (ModelState.IsValid)
            {
                _context.DeliveryAddresses.Add(model);
                await _context.SaveChangesAsync();

                TempData["Success"] = "Đã thêm địa chỉ mới thành công!";
                // Chuyển hướng về trang danh sách địa chỉ (sẽ tạo ở bước sau)
                // hoặc trang checkout
                return RedirectToAction("Index", "Checkout");
            }

            // Nếu có lỗi, hiển thị lại form với các thông báo lỗi
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var address = await _context.DeliveryAddresses
                .FirstOrDefaultAsync(a => a.AddressID == id && a.UserID == userId);

            if (address == null) return NotFound();

            return View(address);
        }

        // --- EDIT (POST) ---
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit( DeliveryAddressModel model)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                    // Chỉ cho phép sửa địa chỉ thuộc về user hiện tại
                    var address = await _context.DeliveryAddresses
                        .FirstOrDefaultAsync(a => a.AddressID == model.AddressID && a.UserID == userId);

                    if (address == null) return NotFound();

                    address.FullName = model.FullName;
                    address.PhoneNumber = model.PhoneNumber;
                    address.Address = model.Address;

                    _context.Update(address);
                    await _context.SaveChangesAsync();
                    TempData["Success"] = "Đã cập nhật địa chỉ thành công!";
                    return RedirectToAction("Index", "Checkout"); // hoặc trang danh sách địa chỉ
                }
                catch (DbUpdateConcurrencyException)
                {
                    return BadRequest("Có lỗi khi cập nhật địa chỉ.");
                }
            }
            return View(model);
        }

        // --- DELETE (POST) ---
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var address = await _context.DeliveryAddresses
                .FirstOrDefaultAsync(a => a.AddressID == id && a.UserID == userId);

            if (address == null) return NotFound();
            address.IsDeteted = true;
            _context.DeliveryAddresses.Update(address);
            await _context.SaveChangesAsync();
            TempData["Success"] = "Đã xóa địa chỉ thành công!";
            return RedirectToAction("Index", "Checkout"); // hoặc trang danh sách địa chỉ
        }
    }
}
