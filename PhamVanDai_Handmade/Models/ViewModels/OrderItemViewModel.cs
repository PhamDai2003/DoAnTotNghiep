namespace PhamVanDai_Handmade.Models.ViewModels
{
    public class OrderItemViewModel
    {
        public int VariantId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public string Size { get; set; } = string.Empty;
        public string Color { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string Image { get; set; }
    }
}
