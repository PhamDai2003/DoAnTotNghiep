using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhamVanDai_Handmade.Repository;

namespace PhamVanDai_Handmade.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ContactController : Controller
    {
        private readonly DataContext _context;

        public ContactController(DataContext context)
        {
            _context = context;
        }

        // GET: Admin/Contact
        public async Task<IActionResult> Index()
        {
            var contacts = await _context.Contacts
                .Include(c => c.User) // lấy cả thông tin user
                .Where(c => !c.isDeleted) // bỏ những tin nhắn đã xóa
                .OrderByDescending(c => c.CreatedAt)
                .ToListAsync();

            return View(contacts);
        }

        // GET: Admin/Contact/Details/5
        public async Task<IActionResult> Detail(int id)
        {
            var contact = await _context.Contacts
                .Include(c => c.User)
                .FirstOrDefaultAsync(m => m.ContactID == id);

            if (contact == null)
            {
                return NotFound();
            }

            // Đánh dấu đã đọc (Status = 1)
            if (contact.Status == 0)
            {
                contact.Status = 1;
                _context.Update(contact);
                await _context.SaveChangesAsync();
            }

            return View(contact);
        }

        // POST: Admin/Contact/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var contact = await _context.Contacts.FindAsync(id);
            if (contact == null)
            {
                return NotFound();
            }

            // Xóa mềm (soft delete)
            contact.isDeleted = true;
            _context.Update(contact);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}