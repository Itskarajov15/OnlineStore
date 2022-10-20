using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.Core.Contracts;
using OnlineStore.Core.Models;

namespace OnlineStore.Controllers
{
    [Authorize]
    public class ProductController : BaseController
    {
        private readonly ICommonService commonService;
        private readonly IProductService productService;

        public ProductController(ICommonService commonService,
            IProductService productService)
            : base(commonService)
        {
            this.commonService = commonService;
            this.productService = productService;
        }

        public async Task<IActionResult> Add()
            => this.View(new AddProductViewModel
            {
                Categories = this.commonService.GetCategories(),
                Brands = await this.commonService.GetBrands()
            });

        [HttpPost]
        public async Task<IActionResult> Add(AddProductViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Categories = this.commonService.GetCategories();
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

        public async Task<IActionResult> All(int categoryId = -1)
        {
            var products = new List<ProductCardViewModel>();

            if (categoryId != -1)
            {
                products = await this.productService.GetProductsByCategory(categoryId);
            }
            else
            {
                products = await this.productService.GetAllProducts();
            }

            ViewBag.ModelCategories = this.commonService.GetCategories();
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