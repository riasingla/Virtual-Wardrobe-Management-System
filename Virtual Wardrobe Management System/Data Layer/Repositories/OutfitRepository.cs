using DataAccessLayer.Data;
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
        public void CreateOutfit(Outfit outfit)
        {
            context.Outfits.Add(outfit);
            context.SaveChanges();
        }
        public IEnumerable<Outfit> GetOutfits()
        {
            return context.Outfits.ToList();
        }

        public Outfit GetOutfitById(int id)
        {
            var result = context.Outfits.FirstOrDefault(c => c.OutfitId == id);
            if (result == null)
            {
                throw new ArgumentException("Id does not exist");
            }
            return result;
        }
        public void UpdateOutfit(int id, Outfit outfit)
        {
            var outfitResult = context.Outfits.FirstOrDefault(c => c.OutfitId == id);
            if (outfitResult != null)
            {
                outfitResult.OutfitName = outfit.OutfitName;
                outfitResult.OutfitImageUrl = outfit.OutfitImageUrl;
                outfitResult.UpdatedAt = DateTime.Now;
                context.SaveChanges();
            }
        }
        public void DeleteOutfit(int OutfitId)
        {
            var outfit = context.Outfits.Find(OutfitId);
            if (outfit != null)
            {
                context.Outfits.Remove(outfit);
                context.SaveChanges();
            }
        }
    }
}
