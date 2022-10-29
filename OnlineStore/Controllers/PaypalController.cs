using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.Core.Contracts;
using OnlineStore.Core.Services;

namespace OnlineStore.Controllers
{
    [Authorize]
    public class PaypalController : BaseController
    {
        private readonly ICommonService commonService;
        private readonly PaypalService paypalService;
        private readonly CartService cartService;

        public PaypalController(
            PaypalService paypalService,
            CartService cartService,
            ICommonService commonService)
            :base (commonService)
        {
            this.paypalService = paypalService;
            this.cartService = cartService;
        }

        public async Task<IActionResult> CreatePayment()
        {
            var total = this.cartService.GetProducts().Sum(p => p.Price * p.Quantity);

            if (total <= 0)
            {
                return RedirectToAction("All", "Products");
            }

            var result = await this.paypalService.CreatePayment(total);

            if (result == null)
            {
                return RedirectToAction("Index", "Home");
            }

            foreach (var link in result.links)
            {
                if (link.rel.Equals("approval_url"))
                {
                    return this.Redirect(link.href);
                }
            }

            return this.NotFound();
        }

        public async Task<IActionResult> SuccessedPayment(string paymentId, string token, string payerId, [FromQuery] decimal total)
        {
            HttpContext.Session.Remove("cart");
            var result = await this.paypalService.ExecutePayment(payerId, paymentId, token);
            return View(total);
        }
    }
}