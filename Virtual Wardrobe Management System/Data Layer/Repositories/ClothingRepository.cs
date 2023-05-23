using DataAccessLayer.Data;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class ClothingRepository : IClothingRepository
    {
        private readonly ApplicationDbContext context;
        public ClothingRepository(ApplicationDbContext context)
        {
            this.context = context;
        }
        public void AddClothingItem(ClothingItem item)
        {
            //var isData = context.ClothingItems.Where(x => x.Id == item.Id).Any();
            //if(isData)
            //{
            //}
            context.ClothingItems.Add(item);
            context.SaveChanges();
        }
        public IEnumerable<ClothingItem> GetAllClothingItems()
        {
            return context.ClothingItems;
        }
        public void DeleteClothingItem(int Id)
        {
            var ClothingItem = context.ClothingItems.Find(Id);
            if (ClothingItem != null)
            {
                context.ClothingItems.Remove(ClothingItem);
                context.SaveChanges();
            }
        }

        public ClothingItem GetById(int Id)
        {
            var getbyid = context.ClothingItems.FirstOrDefault(x => x.Id == Id);
            if (getbyid == null)
            {
                throw new ArgumentException("no clothingitem found with this id");
            }
            return getbyid;
        }

        public void UpdateClothingItem(int id, ClothingItem item)
        {
            var result = context.ClothingItems.FirstOrDefault(x => x.Id == id);
            if (result == null)
            {
                throw new ArgumentException("Id not found");
            }
            result.Name = item.Name;
            result.Type = item.Type;
            result.Colour = item.Colour;
            result.Size = item.Size;
            result.ImageUrl = item.ImageUrl;
            context.SaveChanges();

        }

    }
}
