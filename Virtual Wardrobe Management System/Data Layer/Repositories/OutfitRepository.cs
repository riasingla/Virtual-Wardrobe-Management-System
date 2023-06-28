using DataAccessLayer.Data;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using Virtual_Wardrobe_Management_System.Business_Logic.RepositoryInterfaces;
using Virtual_Wardrobe_Management_System.Data_Layer.Entities;

namespace Virtual_Wardrobe_Management_System.Data_Layer.Repositories
{
    public class OutfitRepository : IOutfitRepository
    {
        private readonly ApplicationDbContext context;

        public OutfitRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        // Retrieve clothing items associated with an outfit
        public IEnumerable<ClothingItem> ClothingItems(int outfitId)
        {
            var outfit = context.Outfits
                .Include(o => o.ClothingItems)
                .FirstOrDefault(o => o.OutfitId == outfitId);

            if (outfit != null)
            {
                return outfit.ClothingItems;
            }

            return Enumerable.Empty<ClothingItem>();
        }

        // Create a new outfit
        public void CreateOutfit(Outfit outfit)
        {
            // Add the outfit to the context and save changes
            context.Outfits.Add(outfit);
            context.SaveChanges();
        }

        // Retrieve all outfits
        public IEnumerable<Outfit> GetOutfits()
        {
            // Return all outfits from the context
            return context.Outfits.ToList();
        }

        public IEnumerable<Outfit> GetOutfitByUserId(int userId)
        {
            return context.Outfits.Where(o=>o.UserId == userId);
        }

        // Retrieve an outfit by ID
        public Outfit GetOutfitById(int id)
        {
            var result = context.Outfits.FirstOrDefault(o => o.OutfitId == id );
            if (result == null)
            {
                throw new ArgumentException("ID does not exist");
            }
            return result;
        }

        // Update an outfit
        public void UpdateOutfit(int id, Outfit outfit)
        {
            var outfitResult = context.Outfits.FirstOrDefault(c => c.OutfitId == id);
            if (outfitResult != null)
            {
                // Update the properties of the outfit
                outfitResult.OutfitName = outfit.OutfitName;
                outfitResult.UpdatedAt = DateTime.Now;
                context.SaveChanges();
            }
        }

        // Delete an outfit by ID
        public void DeleteOutfit(int OutfitId)
        {
            var outfit = context.Outfits.Find(OutfitId);
            if (outfit != null)
            {
                // Remove the outfit from the context and save changes
                context.Outfits.Remove(outfit);
                context.SaveChanges();
            }
        }

    }
}
