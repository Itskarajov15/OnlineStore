namespace OnlineStore.Core.Models
{
    public class CartViewModel
    {
        public decimal TotalPrice { get; set; }

        public List<CartProductViewModel> Products { get; set; }
    }
}