using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PhamVanDai_Handmade.Models
{
    public class ProductVariant
    {
        [Key] 
        public int VariantID { get; set; }
        [ForeignKey("Product")]
        public int? ProductID { get; set; }
        public ProductModel? Product { get; set; }
        public string? Color { get; set; }
        public string? Size { get; set; }
        [Required, Column(TypeName = "decimal(18,2)"), Range(0, double.MaxValue, ErrorMessage = "Giá sản phẩm phải >= 0")]
        public decimal Price { get; set; }
        [Required(ErrorMessage = "Số lượng là bắt buộc"), Range(0, int.MaxValue)]
        public int Quantity { get; set; }
        public int SoldCount { get; set; } = 0;
        public string? Image { get; set; }
        public ICollection<CartItemModel> CartItems { get; set; } = new List<CartItemModel>();
        public ICollection<OrderDetailModel> OrderDetails { get; set; } = new List<OrderDetailModel>();

        [NotMapped] // Không map vào DB, chỉ để nhận file upload
        public IFormFile? ImageUpload { get; set; }
    }
}
