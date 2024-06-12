using BethanysPieShop.Models;
using BethanysPieShop.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Xml;

namespace BethanysPieShop.Controllers
{
    public class PieController : Controller
    {
        private readonly IPieRepository _pieRepository;
        private readonly ICategoryRepository _categoryRepository;

        public PieController(IPieRepository pieRepository, ICategoryRepository categoryRepository)
        {
            _pieRepository = pieRepository;
            _categoryRepository = categoryRepository;
        }
        public IActionResult List(string category)
        {
            IEnumerable<Pie> pies;
            string? currentCategory;

            if (string.IsNullOrEmpty(category))
            {
                 pies = _pieRepository.AllPies.OrderBy(p=> p.PieId);
                currentCategory = "All Pies";               
            }
            else
            {
                currentCategory = _categoryRepository.AllCategories.
                FirstOrDefault(c => c.CategoryName == category)?.CategoryName.ToString();

                pies = _pieRepository.AllPies.Where(p => p.Category.CategoryName == currentCategory);         
            }

            var piesListViewModel = new PiesListViewModel(pies, currentCategory);

            return View(piesListViewModel);
        }

        public IActionResult Details(int id)
        {
            var pie = _pieRepository.GetPieById(id);
            if (pie == null) 
            {
                RedirectToAction("List");
            }            
            return View(pie);
        }

        public IActionResult Search()
        {
            return View();
        }
    }
}
