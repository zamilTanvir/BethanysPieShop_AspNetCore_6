using BethanysPieShop.Models;

namespace BethanysPieShop.ViewModels
{
    public class PiesListViewModel
    {
        public IEnumerable<Pie> Pies { get; set; }
        public string? CurrentCategory { get; set; }

        public PiesListViewModel(IEnumerable<Pie> pies, string currentCategory)
        {
            Pies = pies;
            CurrentCategory = currentCategory;
        }
    }
}
