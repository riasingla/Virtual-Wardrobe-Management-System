using DataAccessLayer.Data;
using DataAccessLayer.Models;
using DataAccessLayer.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using Virtual_Wardrobe_Management_System.Data_Layer.Entities;

namespace Virtual_Wardrobe_Management_System.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class ClothingItemsController : Controller
    {
        private readonly IClothingRepository _dbContext;
        public ClothingItemsController(IClothingRepository dbContext)
        {
            _dbContext = dbContext;
        }
        [HttpGet]
        public IEnumerable<ClothingItem> GetAllClothingItems()
        {
            return _dbContext.GetAllClothingItems();
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var item = _dbContext.GetById(id);
            if (item == null)
            {
                return NotFound();
            }
            return Ok(item);
        }

        [HttpPost]
        public IActionResult AddClothingItem(ClothingItem item)
        {
            _dbContext.AddClothingItem(item);
            return CreatedAtAction(nameof(GetById), new { id = item.Id }, item);
        }

        [HttpPut("{id}")]

        public IActionResult UpdateClothingItem(int id, ClothingItem item)
        {
            if (id != item.Id)
            {
                return BadRequest();
            }

            var existingOutfit = _dbContext.GetById(id);
            if (existingOutfit == null)
            {
                return NotFound();
            }
            _dbContext.UpdateClothingItem(id, item);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var existingOutfit = _dbContext.GetById(id);
            if (existingOutfit == null)
            {
                return NotFound();
            }

            _dbContext.DeleteClothingItem(id);

            return NoContent();
        }
    }
}