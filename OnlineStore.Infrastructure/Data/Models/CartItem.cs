using System.ComponentModel.DataAnnotations;

namespace OnlineStore.Infrastructure.Data.Models
{
    public class CartItem
    {
        [Key]
        public string ItemId { get; set; } = null!;

        public int Quantity { get; set; }

        public int ProductId { get; set; }

        public virtual Product Product { get; set; } = null!;

    }
}