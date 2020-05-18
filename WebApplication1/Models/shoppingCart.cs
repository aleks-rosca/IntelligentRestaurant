using System.Web;
using System.Web.Mvc;

namespace WebApplication1.Models
{
    public partial class shoppingCart
    {
        private string ShoppingCartId { get; set; }
        public const string CartSessionKey = "CartId";
        public static shoppingCart GetItem(HttpContextBase context)
        {
            var cart = new shoppingCart();
            return cart;
        }

        public static shoppingCart GetItem(Controller controller)
        {
            return GetItem(controller.HttpContext);
        }

        public void AddToCart(Item item)
        {
            
        }
    }
}