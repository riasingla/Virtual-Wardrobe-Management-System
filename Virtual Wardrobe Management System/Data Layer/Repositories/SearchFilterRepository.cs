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
    public class SearchFilterRepository : ISearchFilterRepository
    {
        private readonly ApplicationDbContext _context;
        public SearchFilterRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public List<SearchAndFilter> SearchByColor(string color)
        {
            return _context.SearchandFilter.Where(c => c.Colour == color).ToList();
        }
        public List<SearchAndFilter> SearchByType(string type)
        {
            return _context.SearchandFilter.Where(c => c.Type == type).ToList();
        }
        public List<SearchAndFilter> FilterBySize(string size)
        {
            return _context.SearchandFilter.Where(c => c.Size.Contains(size)).ToList();
        }
        public List<SearchAndFilter> FilterByType(string type)
        {
            return _context.SearchandFilter.Where(c => c.Type.Contains(type)).ToList();
        }
    }
}

