using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Text.Json.Serialization;

namespace Virtual_Wardrobe_Management_System.Data_Layer.Entities.Authentication___Authorization
{
    public class Users
    {
        [Key]
        public int UserId { get; set; } 
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;
        [Required]
        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; } = string.Empty;
        public RoleType Role { get; set; }

    }
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum RoleType
    {
        User,
        Admin
    }
}
