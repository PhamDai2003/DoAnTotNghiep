using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhamVanDai_Handmade.Models
{
    public class OrderDetailModel
    {
        // khóa chính kép

        [ForeignKey("Order")]
        public int OrderID { get; set; } // Khóa ngoại đến đơn hàng
        public OrderModel Order { get; set; } // Đơn hàng liên kết với chi tiết đơn hàng

        [ForeignKey("ProductVariant")]
        public int VariantID { get; set; } // Khóa ngoại đến biến thể sản phẩm
        public ProductVariant ProductVariant { get; set; } // biến thể Sản phẩm liên kết với chi tiết đơn hàng
        [Range(1, int.MaxValue)]
        public int Quantity { get; set; } // Số lượng sản phẩm trong đơn hàng
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; } // Giá của sản phẩm tại thời điểm đặt hàng
        [NotMapped]
        public decimal TotalPrice => Quantity * Price;
    }
}