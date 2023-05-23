using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public interface IClothingRepository
    {
        void AddClothingItem(ClothingItem item);
        void DeleteClothingItem(int Id);
        IEnumerable<ClothingItem> GetAllClothingItems();
        ClothingItem GetById(int Id);
        void UpdateClothingItem(int id, ClothingItem item);

    }

}
