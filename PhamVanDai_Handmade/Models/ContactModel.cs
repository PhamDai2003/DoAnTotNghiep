using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhamVanDai_Handmade.Models
{
    public class ContactModel
    {
        [Key]
        public int ContactID { get; set; }
        public string Message { get; set; } = string.Empty;
        [ForeignKey("User")]
        public string? UserID { get; set; }
        public UserModel? User { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public int Status { get; set; } = 0;
        public bool isDeleted { get; set; } = false;
    }
}
