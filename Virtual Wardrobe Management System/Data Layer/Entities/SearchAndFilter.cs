using System.ComponentModel.DataAnnotations;

namespace Virtual_Wardrobe_Management_System.Data_Layer.Entities
{
    public class SearchAndFilter
    {
        public string Name { get; set; } = null!;
        public string Type { get; set; } = null!;
        public string Colour { get; set; } = null!;
        public string Size { get; set; } = null!;
        public string OutfitName { get; set; } = null!;
        public int OutfitIfd { get; set; }
        [Key]
        public int Id { get; set; }

    }
}
