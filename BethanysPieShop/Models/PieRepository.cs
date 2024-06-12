using Microsoft.EntityFrameworkCore;

namespace BethanysPieShop.Models
{
    public class PieRepository : IPieRepository
    {
        private readonly BethanysPieShopDbContext _bethanysPieShopDbContext;

        public PieRepository(BethanysPieShopDbContext bethanysPieShopDbContext)
        {
            _bethanysPieShopDbContext = bethanysPieShopDbContext;
        }

        public IEnumerable<Pie> AllPies 
        {
            get
            {
                return _bethanysPieShopDbContext.Pies.Include(p=> p.Category).ToList();
            }
        }

        public IEnumerable<Pie> PiesOfTheWeek 
        {
            get
            {
                return _bethanysPieShopDbContext.Pies.Include(p => p.Category).Where(p=> p.IsPieOfTheWeek).ToList();
            }
        }

        public Pie? GetPieById(int id)
        {
            return _bethanysPieShopDbContext.Pies.FirstOrDefault(p=> p.PieId == id);
        }

        public IEnumerable<Pie> SearchPies(string searchQuery)
        {
            var pies = _bethanysPieShopDbContext.Pies.
                Where(p=> p.Name.Contains(searchQuery));
            return pies;
        }
    }
}
