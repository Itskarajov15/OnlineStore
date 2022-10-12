namespace OnlineStore.Core.Models
{
    public class CartProductViewModel
    {
        public int Id { get; set; }

        public string ImageUrl { get; set; } = null!;

        public string Title { get; set; } = null!;

        public int Quantity { get; set; }

        public decimal Price { get; set; }
    }
}