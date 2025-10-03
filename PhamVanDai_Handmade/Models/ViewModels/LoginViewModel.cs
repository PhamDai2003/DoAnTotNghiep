using System.ComponentModel.DataAnnotations;

namespace PhamVanDai_Handmade.Models.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Hãy nhập Email")]
        public string Email { get; set; }

        [DataType(DataType.Password), Required(ErrorMessage = "Hãy nhập mật khẩu")] // mã hóa password
        public string Password { get; set; }
        public string? ReturnUrl { get; set; } // Giữ lại đường dẫn gốc để chuyển hướng sau khi đăng nhập
    }
}
