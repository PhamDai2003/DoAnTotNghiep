using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhamVanDai_Handmade.Models
{
    public class ProductModel { 
        [Key] 
        public int ProductID { get; set; } 
        [Required(ErrorMessage = "Tên sản phẩm là bắt buộc"), StringLength(200)] 
        public string ProductName { get; set; } 
        public string? slug { get; set; }
        public string? ShortDescription { get; set; }
        [StringLength(1000)]
        public string? Description { get; set; }
        public DateTime CreateAt { get; set; }
        public int Status { get; set; } = 1;
        public bool isDeteled { get; set; } = false; //Xóa mềm
        [ForeignKey("Category")]
        public int? CategoryID { get; set; }
        public CategoryModel? Category { get; set; }
        public ICollection<ReviewModel> Reviews { get; set; } = new List<ReviewModel>();      
        public ICollection<ProductVariant> ProductVariants { get; set; } = new List<ProductVariant>();
        public string? Image { get; set; }

        [NotMapped]
        public IFormFile? ImageUpload { get; set; }
        [NotMapped]
        public double AverageRating => Reviews?.Any() == true ? Reviews.Average(r => r.Rating) : 0;
    }
}
