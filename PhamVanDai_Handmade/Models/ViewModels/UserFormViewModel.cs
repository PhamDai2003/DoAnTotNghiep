using System.ComponentModel.DataAnnotations;

namespace PhamVanDai_Handmade.Models.ViewModels
{
    public class UserFormViewModel
    {
        public string? Id { get; set; }

        [Required(ErrorMessage = "Tên đăng nhập là bắt buộc")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Email là bắt buộc")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Số điện thoại là bắt buộc")]
        [Phone]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Mật khẩu là bắt buộc")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Mật khẩu xác nhận không khớp")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Phải chọn quyền")]
        public string RoleId { get; set; }   // giữ Id của RoleModel

        [Required]
        public int Status { get; set; } = 1;
    }
}

