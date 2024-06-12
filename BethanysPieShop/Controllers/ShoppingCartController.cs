using BethanysPieShop.Models;
using BethanysPieShop.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BethanysPieShop.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly IPieRepository _pieRepository;
        private readonly ShoppingCart _shoppingCart;

        public ShoppingCartController(IPieRepository pieRepository, ShoppingCart shoppingCart)
        {
            _pieRepository = pieRepository;
            _shoppingCart = shoppingCart;
        }
        public IActionResult Index()
        {
            var items = _shoppingCart.GetshoppingCartitems();
            _shoppingCart.ShoppingCartItems = items;

            var shoppingCartViewModel = new ShoppingCartViewModel(_shoppingCart, _shoppingCart.GetShoppingCartTotal());
            return View(shoppingCartViewModel);
        }

        public RedirectToActionResult AddToCart(int pieId)
        {
            var pie = _pieRepository.AllPies.SingleOrDefault(p => p.PieId == pieId);
            if (pie != null) 
            {
                _shoppingCart.AddToCart(pie);
                
            }
            return RedirectToAction("Index");
        }

        public RedirectToActionResult RemoveFromCart(int pieId)
        {
            var pie = _pieRepository.AllPies.FirstOrDefault(p => p.PieId == pieId);
            if(pie != null)
            {
                _shoppingCart.RemoveFromCart(pie);
            }
            return RedirectToAction("Index");
        }
    }
}
