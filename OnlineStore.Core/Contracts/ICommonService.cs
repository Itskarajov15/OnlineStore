using OnlineStore.Core.Models;

namespace OnlineStore.Core.Contracts
{
    public interface ICommonService
    {
        List<CategoryViewModel> GetCategories();

        Task<List<BrandViewModel>> GetBrands();

        Task<List<CategoryCardViewModel>> GetAllCategories();
    }
}