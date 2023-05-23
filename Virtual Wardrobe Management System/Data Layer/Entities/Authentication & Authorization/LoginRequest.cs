using System.ComponentModel.DataAnnotations;

namespace Virtual_Wardrobe_Management_System.Data_Layer.Entities.Authentication___Authorization
{
    public class LoginRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;
        [Required]
        public string Password { get; set; } = null!;
    }
}
