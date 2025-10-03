namespace PhamVanDai_Handmade.Models.ViewModels
{
    public class ProductFormViewModel
    {
        public ProductModel Product { get; set; } = new ProductModel();

        // Ảnh đại diện 
        public  IFormFile? ImageUpload { get; set; }

        // Danh sách biến thể
        public List<ProductVariant> Variants { get; set; } = new List<ProductVariant>();
    }
}
