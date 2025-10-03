using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhamVanDai_Handmade.Models
{
    public class CartItemModel
    {

        [ForeignKey("User")]
        public string UserID { get; set; }
        public UserModel User { get; set; }

        [ForeignKey("ProductVariant")]
        public int VariantID { get; set; }
        public ProductVariant ProductVariant { get; set; }

        public int Quantity { get; set; }
    }
}
