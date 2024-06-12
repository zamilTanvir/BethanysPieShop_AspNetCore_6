
using Microsoft.EntityFrameworkCore;

namespace BethanysPieShop.Models
{
    public class ShoppingCart
    {
        private readonly BethanysPieShopDbContext _bethanysPieShopDbContext;
        public string? ShoppingCartId { get; set; }
        public List<ShoppingCartItem> ShoppingCartItems { get; set; }

        public ShoppingCart(BethanysPieShopDbContext bethanysPieShopDbContext)
        {
            _bethanysPieShopDbContext = bethanysPieShopDbContext;
        }

         public static ShoppingCart GetCart(IServiceProvider services)
        {
            ISession? session = services.GetRequiredService<IHttpContextAccessor>()?.
                HttpContext?.Session;
            var context = services.GetService<BethanysPieShopDbContext>();
            var cartId = session?.GetString("CartId") ?? Guid.NewGuid().ToString();
            session?.SetString("CartId", cartId);
            return new ShoppingCart(context) { ShoppingCartId = cartId };
        }

        public void AddToCart(Pie pie)
        {
            var shoppingCartItem = _bethanysPieShopDbContext.ShoppingCartItems.
                FirstOrDefault(s => s.Pie == pie && ShoppingCartId == ShoppingCartId);

            if(shoppingCartItem == null)
            {
                shoppingCartItem = new ShoppingCartItem
                {
                    Pie = pie,
                    Amount = 1,
                    ShoppingCartId = ShoppingCartId
                };
                _bethanysPieShopDbContext.ShoppingCartItems.Add(shoppingCartItem);
            }
            else
            {
                shoppingCartItem.Amount++;
            }
            _bethanysPieShopDbContext.SaveChanges();
            
        }

        public int RemoveFromCart(Pie pie)
        {
            var shoppingCartItem = _bethanysPieShopDbContext.ShoppingCartItems.
                FirstOrDefault(s => s.Pie == pie && ShoppingCartId == ShoppingCartId);

            int localAmount = 0;

            if(shoppingCartItem != null)
            {
                if (shoppingCartItem?.Amount > 1)
                {
                    shoppingCartItem.Amount--;
                    localAmount = shoppingCartItem.Amount;
                }
                else
                {
                    _bethanysPieShopDbContext.ShoppingCartItems.Remove(shoppingCartItem);
                }
            }
            
            _bethanysPieShopDbContext.SaveChanges(true);
            return localAmount;
        }

        public List<ShoppingCartItem> GetshoppingCartitems()
        {
            return _bethanysPieShopDbContext.ShoppingCartItems.Where(
                s=> s.ShoppingCartId == ShoppingCartId).Include(s=> s.Pie).ToList();
            
        }

        public void ClearCart()
        {
            var cartItem = _bethanysPieShopDbContext.ShoppingCartItems.Where(
                s => s.ShoppingCartId == ShoppingCartId);

            _bethanysPieShopDbContext.ShoppingCartItems.RemoveRange(cartItem);
            _bethanysPieShopDbContext.SaveChanges();
        }

        public decimal? GetShoppingCartTotal()
        {
            var total = _bethanysPieShopDbContext.ShoppingCartItems.
                Where(s=> s.ShoppingCartId == ShoppingCartId).Select
                (s=> s.Amount * s.Pie.Price).Sum();
            return total;
        }
    }
}
