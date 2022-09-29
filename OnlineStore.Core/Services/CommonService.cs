using Microsoft.EntityFrameworkCore;
using OnlineStore.Core.Contracts;
using OnlineStore.Core.Models;
using OnlineStore.Infrastructure.Data;

namespace OnlineStore.Core.Services
{
    public class CommonService : ICommonService
    {
        private readonly ApplicationDbContext context;

        public CommonService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<List<BrandViewModel>> GetBrands()
            => await this.context
                   .Brands
                   .Select(b => new BrandViewModel
                   {
                       Id = b.Id,
                       Name = b.Name
                   })
                   .ToListAsync();

        public async Task<List<CategoryViewModel>> GetCategories()
            => await this.context
                   .Categories
                   .Select(c => new CategoryViewModel
                   {
                       Id = c.Id,
                       Name = c.Name
                   })
                   .ToListAsync();
    }
}