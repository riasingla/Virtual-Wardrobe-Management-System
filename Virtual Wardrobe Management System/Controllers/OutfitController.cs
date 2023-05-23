using Microsoft.AspNetCore.Mvc;
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

        [HttpGet]
        public ActionResult<IEnumerable<Outfit>> GetOutfits()
        {
            var outfits = _context.GetOutfits();
            return Ok(outfits);
            //return _context.Outfits.ToList();
        }
        [HttpGet("{id}")]
        public ActionResult<Outfit> GetOutfitById(int id)
        {
            var outfit = _context.GetOutfitById(id);

            if (outfit == null)
            {
                return NotFound();
            }
            return Ok(outfit);
        }

        [HttpPost]
        public ActionResult<Outfit> CreateOutfit(Outfit outfit)
        {
            _context.CreateOutfit(outfit);
            return CreatedAtAction(nameof(GetOutfitById), new { id = outfit.OutfitId }, outfit);

        }

        [HttpPut("{id}")]
        public IActionResult UpdateOutfit(int id, Outfit outfit)
        {
            if (id != outfit.OutfitId)
            {
                return BadRequest();
            }

            var existingOutfit = _context.GetOutfitById(id);
            if (existingOutfit == null)
            {
                return NotFound();
            }
            _context.UpdateOutfit(id, outfit);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteOutfit(int id)
        {
            var existingOutfit = _context.GetOutfitById(id);
            if (existingOutfit == null)
            {
                return NotFound();
            }

            _context.DeleteOutfit(id);

            return NoContent();
        }
    }
}
