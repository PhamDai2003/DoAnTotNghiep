using System.ComponentModel.DataAnnotations;

namespace PhamVanDai_Handmade.Models.ViewModels
{
    public class UserEditViewModel
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "Tên tài khoản bắt buộc")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Email bắt buộc")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Số điện thoại bắt buộc")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Quyền bắt buộc")]
        public string RoleId { get; set; }

        [Required]
        public int Status { get; set; } // 1: Hoạt động, 2: Khóa
    }
}
