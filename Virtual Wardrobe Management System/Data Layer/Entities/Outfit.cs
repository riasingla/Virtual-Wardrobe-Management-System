using DataAccessLayer.Models;
using System.ComponentModel.DataAnnotations;
using Virtual_Wardrobe_Management_System.Data_Layer.Entities.Authentication___Authorization;

namespace Virtual_Wardrobe_Management_System.Data_Layer.Entities
{
    public class Outfit
    {
        [Key]
        public int OutfitId { get; set; }
        [Required]
        public string OutfitName { get; set; } = null!;
        [Required]
        public virtual ICollection<ClothingItem> Items { get; set; } = null!;
        public string OutfitImageUrl { get; set; } = null!;
        [Required]
        public DateTime CreatedAt { get; set; }
        [Required]
        public DateTime UpdatedAt { get; set; }
        public Users Users { get; internal set; } = null!;
    }
}
