namespace PhamVanDai_Handmade.Models.ViewModels
{
    public class OrderDetailViewModel
    {
        public int OrderID { get; set; }
        public DateTime OrderDate { get; set; }

        // Trạng thái đơn hàng
        public int Status { get; set; }
        public string StatusText { get; set; } = string.Empty;

        // Thanh toán
        public int PaymentStatus { get; set; } // 1: Chưa thanh toán, 2: Đã thanh toán
        public string PaymentStatusText { get; set; } = string.Empty;

        // Phí ship & mã giảm giá
        public decimal ShippingFee { get; set; }
        public int? CouponId { get; set; }
        public string? CouponCode { get; set; }

        // Người đặt (từ bảng User)
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        // Người nhận (từ bảng DeliveryAddress)
        public DeliveryAddressViewModel DeliveryAddress { get; set; } = new DeliveryAddressViewModel();

        // Danh sách sản phẩm
        public List<OrderItemViewModel> OrderDetails { get; set; } = new List<OrderItemViewModel>();

        // Tổng tiền
        public decimal TotalAmount { get; set; }
    }
}
