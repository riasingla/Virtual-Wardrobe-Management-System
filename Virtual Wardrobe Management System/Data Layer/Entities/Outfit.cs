using DataAccessLayer.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Virtual_Wardrobe_Management_System.Data_Layer.Entities.Authentication___Authorization;

namespace Virtual_Wardrobe_Management_System.Data_Layer.Entities
{
    public class Outfit
    {
        [Key]
        public int OutfitId { get; set; }
        [Required]
        public string OutfitName { get; set; } = null!;
        public string Item1 { get; set; } = null!;
        public string Item2 { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int UserId { get; set; }
        public virtual ICollection<ClothingItem> ClothingItems { get; set; } = new List<ClothingItem>();
    }
}
