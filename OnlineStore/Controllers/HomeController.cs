using Microsoft.AspNetCore.Mvc;
using OnlineStore.Core.Contracts;
using OnlineStore.Core.Models;
using OnlineStore.Models;
using System.Diagnostics;

namespace OnlineStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductService productService;
        private readonly ICommonService commonService;

        public HomeController(
            ILogger<HomeController> logger,
            IProductService productService,
            ICommonService commonService)
        {
            _logger = logger;
            this.productService = productService;
            this.commonService = commonService;
        }

        public async Task<IActionResult> Index()
        {
            var carouselProducts = await this.productService.GetCarouselProducts();

            ViewBag.Categories = await this.commonService.GetAllCategories();

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