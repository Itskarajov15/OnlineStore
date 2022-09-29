using OnlineStore.Core.Models;

namespace OnlineStore.Core.Contracts
{
    public interface ICommonService
    {
        Task<List<CategoryViewModel>> GetCategories();

        Task<List<BrandViewModel>> GetBrands();
    }
}