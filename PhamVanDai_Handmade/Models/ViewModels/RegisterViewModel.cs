using System.ComponentModel.DataAnnotations;

namespace PhamVanDai_Handmade.Models.ViewModels
{
    public class RegisterViewModel
    {
        public string UserName { get; set; }
        [EmailAddress(ErrorMessage = "Địa chỉ email không hợp lệ.")]
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
