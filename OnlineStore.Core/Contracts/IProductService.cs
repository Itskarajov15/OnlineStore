using OnlineStore.Core.Models;

namespace OnlineStore.Core.Contracts
{
    public interface IProductService
    {
        Task<bool> Add(AddProductViewModel model);

        Task<List<ProductCarouselViewModel>> GetCarouselProducts();
    }
}