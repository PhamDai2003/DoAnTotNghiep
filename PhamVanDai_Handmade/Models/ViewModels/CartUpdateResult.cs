namespace PhamVanDai_Handmade.Models.ViewModels
{
    public class CartUpdateResult
    {
        public bool Success { get; set; }
        public int VariantID { get; set; }
        public int Quantity { get; set; }
        public decimal Total { get; set; }
        public string? Message { get; set; }
        public bool Removed { get; set; }
        public decimal CartTotal { get; set; } // tổng tiền giỏ hàng
    }
}
