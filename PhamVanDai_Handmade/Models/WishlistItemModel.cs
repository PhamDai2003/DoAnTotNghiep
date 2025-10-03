namespace PhamVanDai_Handmade.Models
{
    public class WishlistItemModel
    {
        public int Id { get; set; }

        // Khóa ngoại tới người dùng
        public string UserID { get; set; }
        public virtual UserModel User { get; set; }

        // Khóa ngoại tới sản phẩm (bản gốc, không phải biến thể)
        public int ProductID { get; set; }
        public virtual ProductModel Product { get; set; }

    }
}
