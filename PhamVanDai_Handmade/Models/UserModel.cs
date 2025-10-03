using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhamVanDai_Handmade.Models
{
    public class UserModel : IdentityUser
    {
        public int Status { get; set; } = 1;
        public bool IsDeleted { get; set; } = false; // true: đã xóa mềm, false: chưa xóa
        public DateTime CreatedDate { get; set; } = DateTime.Now; // ngày đăng ký
        public ICollection<CartItemModel> CartItems { get; set; } = new List<CartItemModel>(); // Giỏ hàng của người dùng
        public ICollection<OrderModel> Orders { get; set; } = new List<OrderModel>(); // Danh sách đơn hàng của người dùng
        public ICollection<ReviewModel> Reviews { get; set; } = new List<ReviewModel>(); // Danh sách đánh giá của người dùng
        public ICollection<DeliveryAddressModel> DeliveryAddresses { get; set; } = new List<DeliveryAddressModel>(); // Danh sách đánh giá của người dùng
        public ICollection<ContactModel> Contacts { get; set; } = new List<ContactModel>(); // Danh sách liên hệ của người dùng)


    }
}