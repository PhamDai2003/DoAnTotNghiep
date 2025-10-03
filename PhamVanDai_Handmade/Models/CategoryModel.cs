using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace PhamVanDai_Handmade.Models
{
    public class CategoryModel
    {
        [Key]
        public int CategoryID { get; set; }

        [Required(ErrorMessage = "Tên danh mục không được để trống")]
        [StringLength(100)]
        public string CategoryName { get; set; }

        [StringLength(100)]
        public string? Slug { get; set; }

        [StringLength(500)]
        public string? Description { get; set; }   // Mô tả danh mục

        [Required]
        public int Status { get; set; } = 1; // 1: Hoạt động, 0: Không hoạt động

        [Required]
        public bool IsDeleted { get; set; } = false; // true: đã xóa mềm, false: chưa xóa

        public ICollection<ProductModel> Products { get; set; } = new List<ProductModel>();

    }
}
