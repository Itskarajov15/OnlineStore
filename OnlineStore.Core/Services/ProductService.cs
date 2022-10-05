using Microsoft.EntityFrameworkCore;
using OnlineStore.Core.Contracts;
using OnlineStore.Core.Models;
using OnlineStore.Infrastructure.Data;
using OnlineStore.Infrastructure.Data.Models;

namespace OnlineStore.Core.Services
{
    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext context;
        private readonly ICloudinaryService cloudinaryService;

        public ProductService(ApplicationDbContext context,
            ICloudinaryService cloudinaryService)
        {
            this.context = context;
            this.cloudinaryService = cloudinaryService;
        }

        public async Task<bool> Add(AddProductViewModel model)
        {
            var isAdded = false;

            var product = new Product()
            {
                Title = model.Title,
                Price = model.Price,
                Quantity = model.Quantity,
                Specifications = model.Specifications,
                BrandId = model.BrandId,
                CategoryId = model.CategoryId,
                Description = model.Description
            };

            try
            {
                await this.context.Products.AddAsync(product);
                await this.context.SaveChangesAsync();

                foreach (var image in model.ProductImages)
                {
                    product.ProductImages.Add(new ProductImages
                    {
                        Url = await this.cloudinaryService.UploadPicture(image)
                    });
                }

                await this.context.SaveChangesAsync();

                isAdded = true;
            }
            catch (Exception)
            {
                isAdded = false;
            }

            return isAdded;
        }

        public async Task<List<ProductCardViewModel>> GetAllProducts(SortingViewModel model)
        {
            var products = await this.context.Products
                    .Where(p => p.IsActive == true)
                    .Select(p => new ProductCardViewModel
                    {
                        Id = p.Id,
                        Title = p.Title,
                        ImageUrl = p.ProductImages.Select(pi => pi.Url).FirstOrDefault(),
                        //Rating = p.Reviews.Select(r => r.Rating).Average(),
                        Price = p.Price,
                        Category = p.Category.Name,
                        BrandId = p.BrandId
                    })
                    .OrderBy(p => p.Price)
                    .ToListAsync();

            if (model != null)
            {
                var sortedProducts = new List<ProductCardViewModel>();

                if (model.BrandsIds.Length > 0)
                {
                    sortedProducts = products
                        .Where(p => model.BrandsIds.Contains(p.BrandId))
                        .ToList();
                }

                if (model.SortingValue == "High - Low Price")
                {
                    sortedProducts = products
                        .OrderByDescending(p => p.Price)
                        .ToList();
                }
                else
                {
                    sortedProducts = products
                        .OrderBy(p => p.Price)
                        .ToList();
                }

                return sortedProducts;
            }

            return products;
        }

        public async Task<List<ProductCarouselViewModel>> GetCarouselProducts()
        {
            var products = await this.context.Products
                .Select(p => new ProductCarouselViewModel
                {
                    Id = p.Id,
                    Title = p.Title,
                    Description = p.Description,
                    Price = p.Price,
                    ImageUrl = p.ProductImages.Select(pi => pi.Url).FirstOrDefault()
                })
                .Take(2)
                .ToListAsync();

            return products;
        }
    }
}