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

            order.OrderDetails = new List<OrderDetail>();
            
            foreach (var item in items)
            {
                var orderDetails = new OrderDetail
                {
                    PieId = item.Pie.PieId,
                    Amount = item.Amount,
                    Price = item.Pie?.Price
                };
                order.OrderDetails.Add(orderDetails);
            }

            _bethanysPieShopDbContext.Orders.Add(order);
            _bethanysPieShopDbContext.SaveChanges();
            
        }
    }
}
