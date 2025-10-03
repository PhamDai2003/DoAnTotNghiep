using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PhamVanDai_Handmade.Models;
using PhamVanDai_Handmade.Models.ViewModels;
using PhamVanDai_Handmade.Repository;

namespace PhamVanDai_Handmade.Controllers
{
    public class ContactController : Controller
    {
        private readonly DataContext _context;
        private readonly UserManager<UserModel> _userManager;

        public ContactController(DataContext context, UserManager<UserModel> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = await _userManager.GetUserAsync(User);

                // Tạo 1 model để gửi sang View
                var model = new ContactViewModel
                {
                    Name = user.UserName,        // nếu bạn có cột FullName trong ApplicationUser
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber
                };

                return View(model);
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SendMessage(ContactModel model)
        {
            if (!User.Identity.IsAuthenticated)
            {
                TempData["Error"] = "Bạn cần đăng nhập để gửi tin nhắn.";
                return RedirectToAction("Index", "Contact");
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                TempData["Error"] = "Không tìm thấy thông tin người dùng.";
                return RedirectToAction("Index", "Contact");
            }

            var contact = new ContactModel
            {
                UserID = user.Id,
                User = user,
                Message = model.Message,
                CreatedAt = DateTime.Now
            };

            _context.Contacts.Add(contact);
            await _context.SaveChangesAsync();

            TempData["Success"] = "Tin nhắn đã được gửi thành công!";
            return RedirectToAction("Index", "Contact");
        }

    }
}
