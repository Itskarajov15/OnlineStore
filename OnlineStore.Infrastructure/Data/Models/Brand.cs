using System.ComponentModel.DataAnnotations;

namespace OnlineStore.Infrastructure.Data.Models
{
    public class Brand
    {
        public Brand()
        {
            this.Products = new HashSet<Product>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; } = null!;

        public ICollection<Product> Products { get; set; }
    }
}