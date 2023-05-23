using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Virtual_Wardrobe_Management_System.Data_Layer.Entities;

namespace DataAccessLayer.Repositories
{
    public interface ISearchFilterRepository
    {
        List<SearchAndFilter> SearchByColor(string color);
        List<SearchAndFilter> SearchByType(string type);
        List<SearchAndFilter> FilterBySize(string size);
        List<SearchAndFilter> FilterByType(string type);
    }
}
