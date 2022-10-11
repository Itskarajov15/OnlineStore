using Microsoft.AspNetCore.Mvc;
using OnlineStore.Core.Services;

namespace OnlineStore.Controllers
{
    public class CartController : Controller
    {
        private readonly CartService cartService;

        public CartController(CartService cartService)
        {
            this.cartService = cartService;
        }

        public async Task<IActionResult> Add(int id)
        {
            await this.cartService.AddToCart(id);

            return RedirectToAction("All", "Product");
        }
    }
}