using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhamVanDai_Handmade.Models
{
    public class OrderModel
    {
        [Key]
        public int OrderID { get; set; } // Khóa chính
        [ForeignKey("User")]
        public string UserID { get; set; } // Khóa ngoại đến người dùng
        public UserModel User { get; set; } // Người dùng đã đặt hàng

        public DateTime OrderDate { get; set; } // Ngày đặt hàng
        public string PaymentMethod { get; set; } // COD, Online
        public int PaymentStatus { get; set; } // Trạng thái thanh toán (ví dụ: Đã thanh toán, Chưa thanh toán)
        public decimal ShippingFree { get; set; }
        public int Status { get; set; } = 1; // Trạng thái đơn hàng (ví dụ: Đang xử lý, Đã giao, Hủy)

        public int? CouponID { get; set; }
        public CouponModel? Coupon { get; set; }

        public decimal TotalAmount { get; set; }

        [ForeignKey("DeliveryAddress")]
        public int AddressID { get; set; }
        public DeliveryAddressModel DeliveryAddress { get; set; }


        public ICollection<OrderDetailModel> OrderDetails { get; set; } = new List<OrderDetailModel>(); // Danh sách chi tiết đơn hàng
    }
}