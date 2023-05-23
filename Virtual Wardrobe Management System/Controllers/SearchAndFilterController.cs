using DataAccessLayer.Models;
using DataAccessLayer.Repositories;
using Microsoft.AspNetCore.Mvc;
using Virtual_Wardrobe_Management_System.Data_Layer.Entities;

namespace Virtual_Wardrobe_Management_System.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]

    public class SearchAndFilterController : Controller
    {
        private readonly ISearchFilterRepository _context;

        public SearchAndFilterController(ISearchFilterRepository context)
        {
            _context = context;
        }

        [HttpGet("seacrh-by-color/{color}")]
        public ActionResult<List<SearchAndFilter>> SearchByColor(string color)
        {
            var result = _context.SearchByColor(color);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpGet("seacrh-by-type/{type}")]
        public ActionResult<List<SearchAndFilter>> SearchByType(string type)
        {
            var result = _context.SearchByType(type);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
        [HttpGet("filter-by-size/{size}")]
        public ActionResult<List<SearchAndFilter>> FilterBySize(string size)
        {
            var result = _context.FilterBySize(size);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
        [HttpGet("filter-by-type/{type}")]
        public ActionResult<List<SearchAndFilter>> FilterByType(string type)
        {
            var result = _context.FilterByType(type);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

    }
}
