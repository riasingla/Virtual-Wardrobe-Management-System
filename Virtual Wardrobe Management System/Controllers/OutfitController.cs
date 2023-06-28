
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Virtual_Wardrobe_Management_System.Business_Logic.RepositoryInterfaces;
using Virtual_Wardrobe_Management_System.Data_Layer.Entities;

namespace Virtual_Wardrobe_Management_System.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class OutfitController : Controller
    {
        private readonly IOutfitRepository _context;

        public OutfitController(IOutfitRepository context)
        {
            _context = context;
        }

        // Retrieve all outfits
        [HttpGet]
        [EnableCors("AllowLocalhost")]
        public ActionResult<IEnumerable<Outfit>> GetOutfits()
        {
            // Call the repository to get all outfits
            var outfits = _context.GetOutfits();
            return Ok(outfits);
        }

        [HttpGet("user/{userId}")]
        [EnableCors("AllowLocalhost")]
        public ActionResult<IEnumerable<Outfit>> GetOutfitsByUserId(int userId)
        {
            var outfits = _context.GetOutfitByUserId(userId);

            if (outfits == null || !outfits.Any())
            {
                return NotFound();
            }

            return Ok(outfits);
        }


        // Retrieve an outfit by ID
        [HttpGet("{id}")]
        [EnableCors("AllowLocalhost")]
        public ActionResult<Outfit> GetOutfitById(int id)
        {
            // Call the repository to get the outfit by ID
            var outfit = _context.GetOutfitById(id);

            if (outfit == null)
            {
                return NotFound();
            }
            return Ok(outfit);
        }

        // Create a new outfit
        [HttpPost]
        [EnableCors("AllowLocalhost")]
        public ActionResult<Outfit> CreateOutfit(Outfit outfit)
        {
            // Retrieve selected clothing item IDs
            var selectedIds = outfit.ClothingItems.Select(item => item.Id).ToList();

            // Get the selected clothing items from the context
            var selectedItems = _context.ClothingItems(outfit.OutfitId)
                .Where(item => selectedIds.Contains(item.Id))
                .ToList();

            // Set creation and update timestamps, and assign the selected clothing items to the outfit
            outfit.CreatedAt = DateTime.Now;
            outfit.UpdatedAt = DateTime.Now;
            outfit.ClothingItems = selectedItems;

            // Call the repository to create the outfit
            _context.CreateOutfit(outfit);

            return Ok();
        }

        // Update an existing outfit
        [HttpPut("{id}")]
        [EnableCors("AllowLocalhost")]
        public IActionResult UpdateOutfit(int id, Outfit outfit)
        {
            if (id != outfit.OutfitId)
            {
                return BadRequest();
            }

            // Check if the outfit exists
            var existingOutfit = _context.GetOutfitById(id);
            if (existingOutfit == null)
            {
                return NotFound();
            }

            // Call the repository to update the outfit
            _context.UpdateOutfit(id, outfit);

            return NoContent();
        }

        // Delete an outfit
        [HttpDelete("{id}")]
        [EnableCors("AllowLocalhost")]
        public IActionResult DeleteOutfit(int id)
        {
            // Check if the outfit exists
            var existingOutfit = _context.GetOutfitById(id);
            if (existingOutfit == null)
            {
                return NotFound();
            }

            // Call the repository to delete the outfit
            _context.DeleteOutfit(id);

            return NoContent();
        }

    }
}