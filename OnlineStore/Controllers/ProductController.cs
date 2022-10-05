using Microsoft.AspNetCore.Mvc;
using OnlineStore.Core.Contracts;
using OnlineStore.Core.Models;

namespace OnlineStore.Controllers
{
    public class ProductController : Controller
    {
        private readonly ICommonService commonService;
        private readonly IProductService productService;

        public ProductController(ICommonService commonService,
            IProductService productService)
        {
            this.commonService = commonService;
            this.productService = productService;
        }

        public async Task<IActionResult> Add()
            => this.View(new AddProductViewModel
            {
                Categories = await this.commonService.GetCategories(),
                Brands = await this.commonService.GetBrands()
            });

        [HttpPost]
        public async Task<IActionResult> Add(AddProductViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Categories = await this.commonService.GetCategories();
                model.Brands = await this.commonService.GetBrands();
                return this.View(model);
            }

            var isAdded = await this.productService.Add(model);

            if (!isAdded)
            {
                return this.View(model);
            }

            return RedirectToAction("All"); ///////////////////////////////Change it when ready
        }

        public async Task<IActionResult> All()
        {
            var products = await this.productService.GetAllProducts();

            ViewBag.Categories = await this.commonService.GetCategories();
            ViewBag.Brands = await this.commonService.GetBrands();

            return View(products);
        }

        [HttpPost]
        public async Task<IActionResult> All([FromBody] SortingViewModel model)
        {
            var products = await this.productService.GetAllProducts(model);

            return Json(products);
        }
    }
}