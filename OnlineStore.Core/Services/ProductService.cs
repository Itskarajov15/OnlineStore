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
                CategoryId = model.CategoryId
            };

            foreach (var image in model.ProductImages)
            {
                product.ProductImages.Add(new ProductImages
                {
                    Url = await this.cloudinaryService.UploadPicture(image)
                });
            }

            try
            {
                await this.context.Products.AddAsync(product);
                await this.context.SaveChangesAsync();
                isAdded = true;
            }
            catch (Exception)
            {
                isAdded = false;
            }

            return isAdded;
        }
    }
}