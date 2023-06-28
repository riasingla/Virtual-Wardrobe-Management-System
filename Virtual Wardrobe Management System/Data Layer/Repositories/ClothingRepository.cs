using DataAccessLayer.Data;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Virtual_Wardrobe_Management_System.Data_Layer.Entities;

namespace DataAccessLayer.Repositories
{
    public class ClothingRepository : IClothingRepository
    {
        private readonly ApplicationDbContext context;

        public ClothingRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        // Add a new clothing item
        public void AddClothingItem(ClothingItem item)
        {
            // Add the clothing item to the context and save changes
            context.ClothingItems.Add(item);
            context.SaveChanges();
        }

        // Retrieve all clothing items
        public IEnumerable<ClothingItem> GetAllClothingItems()
        {
            // Return all clothing items from the context
            return context.ClothingItems.ToList();
        }
        public IEnumerable<ClothingItem> GetItemsByUserId(int userId)
        {
            return context.ClothingItems.Where(o => o.UserId == userId);
        }
        // Delete a clothing item by ID
        public void DeleteClothingItem(int Id)
        {
            // Find the clothing item by ID
            var clothingItem = context.ClothingItems.Find(Id);
            if (clothingItem != null)
            {
                // Remove the clothing item from the context and save changes
                context.ClothingItems.Remove(clothingItem);
                context.SaveChanges();
            }
        }

        // Retrieve a clothing item by ID
        public ClothingItem GetById(int Id)
        {
            // Find the clothing item by ID
            var clothingItem = context.ClothingItems.FirstOrDefault(x => x.Id == Id);
            if (clothingItem == null)
            {
                throw new ArgumentException("No clothing item found with this ID");
            }
            return clothingItem;
        }

        // Update a clothing item
        public void UpdateClothingItem(int id, ClothingItem item)
        {
            // Find the clothing item by ID
            var result = context.ClothingItems.FirstOrDefault(x => x.Id == id);
            if (result == null)
            {
                throw new ArgumentException("ID not found");
            }

            // Update the properties of the clothing item
            result.Type = item.Type;
            result.Colour = item.Colour;
            result.Size = item.Size;
            result.ImageUrl = item.ImageUrl;

            // Save changes to the context
            context.SaveChanges();
        }


    }
}
