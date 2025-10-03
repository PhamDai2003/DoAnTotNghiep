//using PhamVanDai_Handmade.Repository.PageList;

namespace PhamVanDai_Handmade.Models.ViewModels
{
    public class ProductIndexViewModel
    {
        public List<int> WishlistedProductIds { get; set; } = new List<int>();

        // Dữ liệu cho sidebar filter
        public IEnumerable<CategoryModel> Categories { get; set; }
        public IEnumerable<string> AllColors { get; set; }
        public IEnumerable<string> AllSizes { get; set; }
        public decimal MinPrice { get; set; }
        public decimal MaxPrice { get; set; }

        // Dữ liệu lưu lại lựa chọn của người dùng
        public int? CurrentCategoryId { get; set; }
        public decimal? SelectedMinPrice { get; set; }
        public decimal? SelectedMaxPrice { get; set; }
        public List<string> SelectedColors { get; set; } = new List<string>();
        public List<string> SelectedSizes { get; set; } = new List<string>();
        public string CurrentSortOrder { get; set; }
    }
}
