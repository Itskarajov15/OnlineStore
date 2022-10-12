using Microsoft.AspNetCore.Mvc;
using OnlineStore.Core.Contracts;

namespace OnlineStore.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService cartService;

        public CartController(
            ICartService cartService)
        {
            this.cartService = cartService;
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] int id)
        {
            var cart = await this.cartService.AddToCart(id);

            return Json(cart);
        }

        [HttpGet]
        public IActionResult Get()
        {
            var cart = this.cartService.GetProducts();

            return Json(cart);
        }

        [HttpPost]
        public IActionResult Remove([FromBody] int id)
        {
            var cart = this.cartService.Remove(id);

            return Json(cart);
        }
    }
}