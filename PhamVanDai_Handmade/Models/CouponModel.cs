using System.ComponentModel.DataAnnotations;

namespace PhamVanDai_Handmade.Models
{
        public class CouponModel
        {
        [Key]
        public int CouponID { get; set; }

        [Required(ErrorMessage = "Mã giảm giá là bắt buộc")]
        [StringLength(50, ErrorMessage = "Mã giảm giá không được vượt quá 50 ký tự")]
        public string Code { get; set; }

        [StringLength(200, ErrorMessage = "Mô tả không được vượt quá 200 ký tự")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "Số tiền giảm giá là bắt buộc")]
        [Range(1, double.MaxValue, ErrorMessage = "Số tiền giảm giá phải lớn hơn 0")]
        public decimal? DiscountAmount { get; set; }

        [Required(ErrorMessage = "Giá trị đơn hàng tối thiểu là bắt buộc")]
        [Range(0, double.MaxValue, ErrorMessage = "Giá trị đơn hàng tối thiểu không hợp lệ")]
        public decimal? MinOrderValue { get; set; }

        [Required(ErrorMessage = "Số lượng là bắt buộc")]
        [Range(1, int.MaxValue, ErrorMessage = "Số lượng phải lớn hơn 0")]
        public int? quantity { get; set; }

        [Required(ErrorMessage = "Ngày bắt đầu là bắt buộc")]
        [DataType(DataType.Date, ErrorMessage = "Ngày bắt đầu không hợp lệ")]
        public DateTime? StartDate { get; set; }

        [Required(ErrorMessage = "Ngày hết hạn là bắt buộc")]
        [DataType(DataType.Date, ErrorMessage = "Ngày hết hạn không hợp lệ")]
        public DateTime? EndDate { get; set; }

        [Range(1, 2, ErrorMessage = "Trạng thái không hợp lệ (1: Hoạt động, 2: Hết hạn)")]
        public int status { get; set; } = 1;

        public bool IsDeleted { get; set; } = false;

        public ICollection<OrderModel> Orders { get; set; } = new List<OrderModel>();
    }
}
