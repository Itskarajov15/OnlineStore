namespace OnlineStore.Core.Models
{
    public class SortingViewModel
    {
        public int[] BrandsIds { get; set; } = null!;

        public string SortingValue { get; set; } = null!;

        public decimal MaxPrice { get; set; }

        public int? CategoryId { get; set; }
    }
}