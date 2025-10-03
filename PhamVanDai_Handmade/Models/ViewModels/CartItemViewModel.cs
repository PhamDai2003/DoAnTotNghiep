namespace PhamVanDai_Handmade.Models.ViewModels
{
    public class CartItemViewModel
    {
        public int VariantID { get; set; }
        public string ProductName { get; set; }
        public string Image { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public int MaxQuantity { get; set; } 
        public decimal Total => Price * Quantity;   

        // Thêm thông tin biến thể
        public string Size { get; set; }
        public string Color { get; set; }

    }
}
