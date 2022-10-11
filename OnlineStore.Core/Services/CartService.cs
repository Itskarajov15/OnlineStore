using Microsoft.AspNetCore.Http;
using OnlineStore.Core.Extensions;
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

        public async Task AddToCart(int id)
        {
            Product? product = await context.Products.FindAsync(id);

            if (product != null)
            {
                if (SessionHelper.GetObjectFromJson<List<CartItem>>(httpContextAccessor.HttpContext!.Session, "cart") == null)
                {
                    List<CartItem> cart = new List<CartItem>();
                    cart.Add(new CartItem { Product = product, Quantity = 1 });
                    SessionHelper.SetObjectAsJson(httpContextAccessor.HttpContext.Session, "cart", cart);
                }
                else
                {
                    var cart = SessionHelper.GetObjectFromJson<List<CartItem>>(httpContextAccessor.HttpContext.Session, "cart");
                    int index = Exists(id);

                    if (index != -1)
                    {
                        cart[index].Quantity++;
                    }
                    else
                    {
                        cart.Add(new CartItem { Product = product, Quantity = 1 });
                    }

                    SessionHelper.SetObjectAsJson(httpContextAccessor.HttpContext.Session, "cart", cart);
                }
            }
        }

        public void Remove(int id)
        {
            var cart = SessionHelper.GetObjectFromJson<List<CartItem>>(httpContextAccessor.HttpContext.Session, "cart");
            int index = Exists(id);
            cart.RemoveAt(index);
            SessionHelper.SetObjectAsJson(httpContextAccessor.HttpContext.Session, "cart", cart);
        }

        private int Exists(int id)
        {
            var cart = SessionHelper.GetObjectFromJson<List<CartItem>>(httpContextAccessor.HttpContext.Session, "cart");

            for (int i = 0; i < cart.Count; i++)
            {
                if (cart[i].Product.Id.Equals(id))
                {
                    return i;
                }
            }

            return -1;
        }
    }
}