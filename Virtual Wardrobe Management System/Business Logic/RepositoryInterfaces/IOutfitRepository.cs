using DataAccessLayer.Models;
using Virtual_Wardrobe_Management_System.Data_Layer.Entities;

namespace Virtual_Wardrobe_Management_System.Business_Logic.RepositoryInterfaces
{
    public interface IOutfitRepository
    {
        IEnumerable<ClothingItem> ClothingItems(int outfitId);
        void CreateOutfit(Outfit outfit);
        IEnumerable<Outfit> GetOutfits();
        Outfit GetOutfitById(int id);
        IEnumerable<Outfit> GetOutfitByUserId(int userId);
        void UpdateOutfit(int id, Outfit outfit);
        void DeleteOutfit(int id);

    }
}
