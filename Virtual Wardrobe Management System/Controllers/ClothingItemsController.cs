using DataAccessLayer.Data;
using DataAccessLayer.Models;
using DataAccessLayer.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Net;
using System.Security.Claims;
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

        // Retrieve all clothing items
        [HttpGet]
        [EnableCors("AllowLocalhost")]
        public IEnumerable<ClothingItem> GetAllClothingItems()
        {
            // Call the repository to get all clothing items
            return _dbContext.GetAllClothingItems();
        }
        [HttpGet("Items/{userId}")]
        [EnableCors("AllowLocalhost")]
        public ActionResult<IEnumerable<Outfit>> GetItemsByUserId(int userId)
        {
            var Items = _dbContext.GetItemsByUserId(userId);

            if (Items == null || !Items.Any())
            {
                return NotFound();
            }

            return Ok(Items);
        }
        // Retrieve a clothing item by ID
        [HttpGet("{id}")]
        [EnableCors("AllowLocalhost")]
        public IActionResult GetById(int id)
        {
            // Call the repository to get the clothing item by ID
            var item = _dbContext.GetById(id);
            if (item == null)
            {
                return NotFound();
            }
            return Ok(item);
        }

        // Add a new clothing item
        [HttpPost]
        [EnableCors("AllowLocalhost")]
        
        public IActionResult AddClothingItem(ClothingItem item)
        {
            // Call the repository to add the clothing item
            _dbContext.AddClothingItem(item);
            return CreatedAtAction(nameof(GetById), new { id = item.Id }, item);
        }

        // Update an existing clothing item
        [HttpPut("{id}")]
        [EnableCors("AllowLocalhost")]
        public IActionResult UpdateClothingItem(int id, ClothingItem item)
        {
            if (id != item.Id)
            {
                return BadRequest();
            }

            // Check if the clothing item exists
            var existingItem = _dbContext.GetById(id);
            if (existingItem == null)
            {
                return NotFound();
            }

            // Call the repository to update the clothing item
            _dbContext.UpdateClothingItem(id, item);

            return NoContent();
        }

        // Delete a clothing item
        [HttpDelete("{id}")]
        [EnableCors("AllowLocalhost")]
        public IActionResult Delete(int id)
        {
            // Check if the clothing item exists
            var existingItem = _dbContext.GetById(id);
            if (existingItem == null)
            {
                return NotFound();
            }

            // Call the repository to delete the clothing item
            _dbContext.DeleteClothingItem(id);

            return NoContent();
        }

    }
}