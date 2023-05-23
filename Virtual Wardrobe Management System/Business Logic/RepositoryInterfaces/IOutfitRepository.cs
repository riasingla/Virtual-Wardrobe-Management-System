using Virtual_Wardrobe_Management_System.Data_Layer.Entities;

namespace Virtual_Wardrobe_Management_System.Business_Logic.RepositoryInterfaces
{
    public interface IOutfitRepository
    {

        void CreateOutfit(Outfit outfit);
        IEnumerable<Outfit> GetOutfits();
        Outfit GetOutfitById(int id);
        void UpdateOutfit(int id, Outfit outfit);
        void DeleteOutfit(int id);

    }
}
