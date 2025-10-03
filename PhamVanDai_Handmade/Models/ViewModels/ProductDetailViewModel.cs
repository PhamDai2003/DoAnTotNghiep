namespace PhamVanDai_Handmade.Models.ViewModels
{
    public class ProductDetailViewModel
    {
        public ProductModel Product { get; set; }
        public List<ProductModel> RelatedProducts { get; set; }
        public List<ReviewModel> ReviewProducts { get; set; }
        public List<ProductVariant> Variants { get; set; }
        public List<string> AvailableColors { get; set; }
        public List<string> AvailableSizes { get; set; }
        public object VariantsForJson { get; set; } // Dùng object hoặc List<any>
    }
}
