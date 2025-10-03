using System.ComponentModel.DataAnnotations;

namespace PhamVanDai_Handmade.Models.ViewModels
{
    public class CheckoutViewModel
    {
        public int? CouponId { get; set; }
        public List<CartItemViewModel> CartItems { get; set; } = new List<CartItemViewModel>();

        [Required(ErrorMessage = "Vui lòng nhập số điện thoại")]
        //[Phone(ErrorMessage = "Số điện thoại không hợp lệ")]
        //public string? PhoneNumber { get; set; }

        //[Required(ErrorMessage = "Vui lòng nhập địa chỉ giao hàng")]
        //public string? Address { get; set; }

        public decimal ShippingFee { get; set; } 
        public decimal Discount { get; set; } 
        public string PaymentMethod { get; set; }

        public int checkCal { get; set; } 

        // Tổng tiền sản phẩm
        public decimal SubTotal { get; set; }

        // Tổng sau giảm giá + ship
        public decimal FinalTotal { get; set; }

        public void CalculateTotals()
        {
            this.SubTotal = this.CartItems?.Sum(i => i.Total) ?? 0;
            this.FinalTotal = this.SubTotal + this.ShippingFee - this.Discount;
        }

        // --- BỔ SUNG ---
        // Danh sách địa chỉ đã lưu của người dùng
        public List<DeliveryAddressModel> SavedAddresses { get; set; } = new List<DeliveryAddressModel>();

        // Dùng để nhận ID của địa chỉ được chọn từ radio button
        [Required(ErrorMessage = "Vui lòng chọn một địa chỉ giao hàng")]
        public int? SelectedAddressId { get; set; }

        // Dùng cho form thêm địa chỉ mới (nếu muốn tích hợp)
        public DeliveryAddressModel NewAddress { get; set; }
        public string? CouponCode { get; set; }
        //public string PaymentMethod { get; set; }
    }
}
