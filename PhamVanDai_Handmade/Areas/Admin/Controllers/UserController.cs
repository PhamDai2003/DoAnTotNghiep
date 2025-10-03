using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhamVanDai_Handmade.Models;
using PhamVanDai_Handmade.Models.ViewModels;
using X.PagedList.Extensions;

namespace PhamVanDai_Handmade.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserController : Controller
    {
        private readonly UserManager<UserModel> _userManager;
        private readonly RoleManager<RoleModel> _roleManager;

        public UserController(UserManager<UserModel> userManager, RoleManager<RoleModel> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        // GET: Admin/User
        public async Task<IActionResult> Index(string? userName, string? role, int? status, int? page)
        {
            int pageSize = 10; // số user trên 1 trang
            int pageNumber = page ?? 1;

            var users = _userManager.Users
                        .Where(u => !u.IsDeleted) // chỉ lấy user chưa bị xóa
                        .AsQueryable();
            // --- Lọc theo userName ---
            if (!string.IsNullOrEmpty(userName))
            {
                users = users.Where(u => u.UserName.Contains(userName));
            }

            // --- Lọc theo Status ---
            if (status.HasValue)
            {
                users = users.Where(u => u.Status == status.Value);
            }
            var userVMs = new List<UserViewModel>();
            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                // --- Lọc theo Role ---
                if (!string.IsNullOrEmpty(role))
                {
                    if (!roles.Contains(role)) continue; // bỏ qua user không thuộc role lọc
                }
                userVMs.Add(new UserViewModel
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                    RoleName = roles.FirstOrDefault() ?? "Chưa có",
                    Status = user.Status
                });
            }

            // trả về danh sách có phân trang
            return View(userVMs.ToPagedList(pageNumber, pageSize));
        }


        // GET: Admin/User/Create
        public async Task<IActionResult> Create()
        {
            ViewBag.Roles = await _roleManager.Roles.ToListAsync();
            return View(new UserFormViewModel());
        }

        // POST: Admin/User/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UserFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Roles = await _roleManager.Roles.ToListAsync();
                return View(model);
            }

            var user = new UserModel
            {
                UserName = model.UserName,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                Status = model.Status
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                var role = await _roleManager.FindByIdAsync(model.RoleId);
                if (role != null)
                {
                    await _userManager.AddToRoleAsync(user, role.Name);
                }
                TempData["Success"] = "Thêm tài khoản thành công";
                return RedirectToAction(nameof(Index));
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            ViewBag.Roles = await _roleManager.Roles.ToListAsync();
            return View(model);
        }


        // GET: Admin/User/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (string.IsNullOrEmpty(id)) return NotFound();

            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound();

            var roles = await _roleManager.Roles.ToListAsync();
            var userRoles = await _userManager.GetRolesAsync(user);
            var role = await _roleManager.Roles.FirstOrDefaultAsync(r => userRoles.Contains(r.Name));

            var model = new UserEditViewModel
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Status = user.Status,
                RoleId = role?.Id // gán roleId hiện tại
            };

            ViewBag.Roles = roles;
            return View(model);
        }

        // POST: Admin/User/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, UserEditViewModel model)
        {
            if (id != model.Id) return NotFound();

            if (!ModelState.IsValid)
            {
                ViewBag.Roles = await _roleManager.Roles.ToListAsync();
                return View(model);
            }

            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound();

            user.UserName = model.UserName;
            user.Email = model.Email;
            user.PhoneNumber = model.PhoneNumber;
            user.Status = model.Status;

            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                // cập nhật role
                var currentRoles = await _userManager.GetRolesAsync(user);
                if (currentRoles.Any())
                    await _userManager.RemoveFromRolesAsync(user, currentRoles);

                var newRole = await _roleManager.FindByIdAsync(model.RoleId);
                if (newRole != null)
                {
                    await _userManager.AddToRoleAsync(user, newRole.Name);
                }
                TempData["Success"] = "Sửa tài khoản thành công";
                return RedirectToAction(nameof(Index));
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            ViewBag.Roles = await _roleManager.Roles.ToListAsync();
            return View(model);
        }


        // POST: Delete (xóa mềm)
        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound();

            user.IsDeleted = true;
            await _userManager.UpdateAsync(user);

            return RedirectToAction("Index");
        }

        // POST: ResetPassword
        [HttpPost]
        public async Task<IActionResult> ResetPassword(string id, string newPassword)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound();

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var result = await _userManager.ResetPasswordAsync(user, token, newPassword);

            if (!result.Succeeded)
                TempData["Error"] = "Reset mật khẩu thất bại!";
            else
                TempData["Success"] = "Đã reset mật khẩu thành công!";

            return RedirectToAction("Index");
        }


        // Danh sách user đã xóa (Trash)
        public async Task<IActionResult> Trash()
        {
            var users = await _userManager.Users
                .Where(u => u.IsDeleted == true)
                .ToListAsync();

            var userVMs = new List<UserViewModel>();
            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                userVMs.Add(new UserViewModel
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                    Status = user.Status,
                    RoleName = roles.FirstOrDefault() ?? "Chưa gán"
                });
            }

            return View(userVMs);
        }

        // Khôi phục user
        [HttpPost]
        public async Task<IActionResult> Restore(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
                return NotFound();

            user.IsDeleted = false;
            await _userManager.UpdateAsync(user);

            TempData["Success"] = "Khôi phục tài khoản thành công!";
            return RedirectToAction(nameof(Trash));
        }

        // Xóa vĩnh viễn user
        [HttpPost]
        public async Task<IActionResult> DeletePermen(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
                return NotFound();

            var result = await _userManager.DeleteAsync(user);

            if (result.Succeeded)
            {
                TempData["Success"] = "Đã xóa vĩnh viễn tài khoản!";
            }
            else
            {
                TempData["Error"] = "Có lỗi xảy ra khi xóa vĩnh viễn.";
            }

            return RedirectToAction(nameof(Trash));
        }
    }
}


