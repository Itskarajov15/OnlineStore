using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.Core.Contracts;
using OnlineStore.Models;
using System.Diagnostics;

namespace OnlineStore.Controllers
{
    [Authorize]
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductService productService;
        private readonly ICommonService commonService;

        public HomeController(
            ILogger<HomeController> logger,
            IProductService productService,
            ICommonService commonService)
            : base(commonService)
        {
            _logger = logger;
            this.productService = productService;
            this.commonService = commonService;
        }

        public async Task<IActionResult> Index()
        {
            var carouselProducts = await this.productService.GetCarouselProducts();

            ViewBag.CardCategories = await this.commonService.GetAllCategories(); 

            return View(carouselProducts);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}