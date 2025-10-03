using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhamVanDai_Handmade.Models
{
    public class ReviewModel
    {
        [Key]
        public int ReviewID { get; set; } // Khóa chính
        [ForeignKey("Product")]
        public int ProductID { get; set; } // Khóa ngoại đến sản phẩm
        public ProductModel Product { get; set; } // Sản phẩm liên kết với đánh giá
        [ForeignKey("User")]
        public string UserID { get; set; } // Khóa ngoại đến người dùng
        public UserModel User { get; set; } // Người dùng liên kết với đánh giá
        [Required, StringLength(1000)]
        public string Content { get; set; } // Nội dung đánh giá
        [Required, Range(1, 5)]
        public int Rating { get; set; } // Đánh giá (ví dụ: 1-5 sao)
        public DateTime CreatedAt { get; set; } // Ngày tạo đánh giá
        public bool IsDeleted { get; set; } = false;

    }
}