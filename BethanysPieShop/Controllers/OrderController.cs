using BethanysPieShop.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BethanysPieShop.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private readonly ShoppingCart  _shoppingCart;
        private readonly IOrderRepository _orderRepository;

        public OrderController(ShoppingCart shoppingCart, IOrderRepository orderRepository)
        {
            _shoppingCart = shoppingCart;
            _orderRepository = orderRepository;
        }

        public IActionResult Checkout()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Checkout(Order order) 
        {
           var items = _shoppingCart.GetshoppingCartitems();
            _shoppingCart.ShoppingCartItems = items;

            if(_shoppingCart.ShoppingCartItems.Count == 0)
            {
                ModelState.AddModelError("", "Add Pies in Cart");
            }
            if(ModelState.IsValid)
            {
                _orderRepository.CreateOrder(order);
                RedirectToAction("CheckoutComplete");
            }
            return View(order);

        }

        public IActionResult CheckoutComplete()
        {
            return View();
        }
    }
}
