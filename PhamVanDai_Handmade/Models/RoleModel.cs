using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace PhamVanDai_Handmade.Models
{
    public class RoleModel : IdentityRole
    {
        public int Status { get; set; } = 1; // Trạng thái vai trò (1: active, 0: inactive)
        public bool isDeleted { get; set; } = false; // true: đã xóa mềm, false: chưa xóa
    }
}