using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.Core.Contracts;
using OnlineStore.Core.Services;

namespace OnlineStore.Controllers
{
    [Authorize]
    public class CartController : BaseController
    {
        private readonly CartService cartService;
        private readonly ICommonService? commonService;

        public CartController(
            CartService cartService,
            ICommonService commonService)
            :base(commonService)
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

        [HttpGet]
        public IActionResult ShowCart()
        {
            var cart = this.cartService.ShowProductsInCart();

            return View(cart);
        }
    }
}