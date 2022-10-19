namespace OnlineStore.Core.Models
{
    public class ProductCardViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; } = null!;

        public string ImageUrl { get; set; } = null!;
        
        public string Category { get; set; } = null!;

        public int CategoryId { get; set; }

        public int BrandId { get; set; }

        public double Rating { get; set; }

        public decimal Price { get; set; }
    }
}