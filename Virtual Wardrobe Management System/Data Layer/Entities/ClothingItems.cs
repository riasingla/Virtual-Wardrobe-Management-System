using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Virtual_Wardrobe_Management_System.Data_Layer.Entities;

namespace DataAccessLayer.Models
{
    public class ClothingItem
    {
        [Key]
        public int Id { get; set; }
        public string Type { get; set; } = null!;
        public string Colour { get; set; } = null!;
        public string Size { get; set; } = null!;
        public string ImageUrl { get; set; } = null!;
        
    }
}
