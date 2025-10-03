using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PhamVanDai_Handmade.Models;
using PhamVanDai_Handmade.Models.ViewModels;

namespace PhamVanDai_Handmade.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<UserModel> _userManager;
        private readonly SignInManager<UserModel> _signInManager;
        public AccountController(UserManager<UserModel> userManager, SignInManager<UserModel> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (string.IsNullOrEmpty(model.Email) || string.IsNullOrEmpty(model.Password))
            {
                return Json(new { success = false, message = "Email và mật khẩu không được để trống." });
            }

            // Bước 1: Tìm người dùng bằng Email trước
            var user = await _userManager.FindByEmailAsync(model.Email);

            // Nếu không tìm thấy user, trả về lỗi
            if (user == null)
            {
                return Json(new { success = false, message = "Email hoặc mật khẩu không chính xác." });
            }

            if (user.Status != 1) // chỉ cho phép 1 = hoạt động
            {
                return Json(new { success = false, message = "Tài khoản của bạn đã bị khóa. Vui lòng liên hệ qua email này để xác nhận." });
            }

            // Bước 2: Dùng đối tượng user tìm được để kiểm tra mật khẩu
            var result = await _signInManager.PasswordSignInAsync(user, model.Password, isPersistent: false, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                // Đăng nhập thành công, chỉ trả về success = true
                return Json(new { success = true });
            }
            else
            {
                // Mật khẩu sai, trả về lỗi
                return Json(new { success = false, message = "Email hoặc mật khẩu không chính xác." });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            // Kiểm tra xem dữ liệu gửi lên có hợp lệ không (dựa trên các [Required], [Compare]...)
            if (ModelState.IsValid)
            {
                // Tạo một đối tượng user mới
                var user = new UserModel { UserName = model.UserName, Email = model.Email, PhoneNumber = model.Phone };

                // Dùng UserManager để tạo user với mật khẩu đã cho
                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    // Gán người dùng mới vào role "Customer"
                    await _userManager.AddToRoleAsync(user, "User");
                    // Nếu tạo user thành công, tự động đăng nhập cho họ
                    await _signInManager.SignInAsync(user, isPersistent: false);

                    TempData["Success"] = "Đăng ký tài khoản thành công";
                    // Trả về JSON báo thành công
                    return Json(new { success = true });
                }
                else
                {
                    // Nếu có lỗi (VD: username/email đã tồn tại, mật khẩu quá yếu),
                    // gom các lỗi lại và trả về cho client
                    var errors = string.Join("; ", result.Errors.Select(e => e.Description));
                    return Json(new { success = false, message = errors });
                }
            }

            // Nếu ModelState không hợp lệ, trả về lỗi
            var modelStateErrors = string.Join("; ", ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage));
            return Json(new { success = false, message = "Dữ liệu không hợp lệ: " + modelStateErrors });
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
