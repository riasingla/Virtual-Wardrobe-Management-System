using System.ComponentModel.DataAnnotations;
using System.Drawing;

namespace Virtual_Wardrobe_Management_System.Data_Layer.Entities.Authentication___Authorization
{
    public class Users
    {
        [Key]
        public int UserId { get; set; } 
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public DateTime DateOfBirth { get; set; }
        [Required]
        public string Password { get; set; } = string.Empty;
        [Required]
        public string ConfirmPassword { get; set; } = string.Empty;
        public string Email { get; set; } = null!;
        public RoleType Role { get; set; }

    }
    public enum RoleType
    {
        User,
        Admin
    }
}
