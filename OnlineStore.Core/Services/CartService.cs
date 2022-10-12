using Microsoft.AspNetCore.Http;
using OnlineStore.Core.Extensions;
using OnlineStore.Core.Models;
using OnlineStore.Infrastructure.Data;
using OnlineStore.Infrastructure.Data.Models;

namespace OnlineStore.Core.Services
{
    public class CartService
    {
        private readonly ApplicationDbContext context;
        private readonly IHttpContextAccessor httpContextAccessor;

        public CartService(
            ApplicationDbContext context,
            IHttpContextAccessor httpContextAccessor)
        {
            this.context = context;
            this.httpContextAccessor = httpContextAccessor;
        }

        public async Task<List<CartProductViewModel>> AddToCart(int id)
        {
            var product = await context.Products.FindAsync(id);
            List<CartProductViewModel> cart = new List<CartProductViewModel>();

            if (product != null)
            {
                if (SessionHelper.GetObjectFromJson<List<CartProductViewModel>>(httpContextAccessor.HttpContext!.Session, "cart") == null)
                {
                    cart.Add(new CartProductViewModel 
                    {
                        Id = product.Id,
                        ImageUrl = product.ProductImages.Select(pi => pi.Url).FirstOrDefault(),
                        Price = product.Price,
                        Title = product.Title,
                        Quantity = 1
                    });
                    SessionHelper.SetObjectAsJson(httpContextAccessor.HttpContext.Session, "cart", cart);
                }
                else
                {
                    cart = SessionHelper.GetObjectFromJson<List<CartProductViewModel>>(httpContextAccessor.HttpContext.Session, "cart");
                    int index = Exists(id);

                    if (index != -1)
                    {
                        cart[index].Quantity++;
                    }
                    else
                    {
                        cart.Add(new CartProductViewModel 
                        {
                            Id = product.Id,
                            ImageUrl = product.ProductImages.Select(pi => pi.Url).FirstOrDefault(),
                            Price = product.Price,
                            Title = product.Title,
                            Quantity = 1 
                        });
                    }

                    SessionHelper.SetObjectAsJson(httpContextAccessor.HttpContext.Session, "cart", cart);
                }
            }

            return cart;
        }

        public List<CartProductViewModel> GetProducts()
        {
            var cart = new List<CartProductViewModel>();

            if (SessionHelper.GetObjectFromJson<List<CartProductViewModel>>(httpContextAccessor.HttpContext!.Session, "cart") != null)
            {
                cart = SessionHelper.GetObjectFromJson<List<CartProductViewModel>>(httpContextAccessor.HttpContext.Session, "cart");
            }

            return cart;
        }

        public List<CartProductViewModel> Remove(int id)
        {
            var cart = SessionHelper.GetObjectFromJson<List<CartProductViewModel>>(httpContextAccessor.HttpContext.Session, "cart");
            int index = Exists(id);
            cart.RemoveAt(index);
            SessionHelper.SetObjectAsJson(httpContextAccessor.HttpContext.Session, "cart", cart);

            return cart;
        }

        private int Exists(int id)
        {
            var cart = SessionHelper.GetObjectFromJson<List<CartProductViewModel>>(httpContextAccessor.HttpContext.Session, "cart");

            for (int i = 0; i < cart.Count; i++)
            {
                if (cart[i].Id.Equals(id))
                {
                    return i;
                }
            }

            return -1;
        }
    }
}