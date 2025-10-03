using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhamVanDai_Handmade.Models
{
    public class DeliveryAddressModel
    {
        [Key]
        public int AddressID { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public bool IsDeteted { get; set; } = false; // Địa chỉ mặc định
        [ForeignKey("User")]
        public string? UserID { get; set; }
        public UserModel? User { get; set; }

        public ICollection<OrderModel>? Orders { get; set; } = new List<OrderModel>(); // Danh sách đơn hàng 


    }
}
