using Microsoft.AspNetCore.Http;
using OnlineStore.Core.Extensions;
using OnlineStore.Core.Models;

namespace OnlineStore.Core.Contracts
{
    public interface ICartService
    {
        Task<List<CartProductViewModel>> AddToCart(int id);

        List<CartProductViewModel> GetProducts();

        List<CartProductViewModel> Remove(int id);
    }
}