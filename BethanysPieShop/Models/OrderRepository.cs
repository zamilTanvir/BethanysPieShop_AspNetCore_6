using Microsoft.AspNetCore;

namespace BethanysPieShop.Models
{
    public class OrderRepository : IOrderRepository
    {
        private readonly BethanysPieShopDbContext _bethanysPieShopDbContext;
        private readonly ShoppingCart _shoppingCart;

        public OrderRepository(BethanysPieShopDbContext bethanysPieShopDbContext, ShoppingCart shoppingCart)
        {
            _bethanysPieShopDbContext = bethanysPieShopDbContext;
            _shoppingCart = shoppingCart;
        }

        public void CreateOrder(Order order)
        {
            order.OrderPlaced = DateTime.Now;

            var items = _shoppingCart.GetshoppingCartitems();
            _shoppingCart.ShoppingCartItems = items;

            order.OrderTotal = _shoppingCart.GetShoppingCartTotal();

            _bethanysPieShopDbContext.Orders.Add(order);
            _bethanysPieShopDbContext.SaveChanges();

            

            foreach (var item in items)
            {
                var orderDetails = new OrderDetail
                {
                    OrderId = order.OrderId,
                    Amount = item.Amount,
                    Price = item.Pie?.Price
                };
                _bethanysPieShopDbContext.OrderDetails.Add(orderDetails);
            }

            _bethanysPieShopDbContext.SaveChanges(true);
            
        }
    }
}
