using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        //public int UserId { get;set; }

    }
}
