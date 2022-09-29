using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineStore.Infrastructure.Data.Models
{
    public class Product
    {
        public Product()
        {
            this.ProductImages = new List<ProductImages>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(60)]
        public string Title { get; set; } = null!;

        [Required]
        [StringLength(1000)]
        public string Specifications { get; set; } = null!;

        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        public int Quantity { get; set; }
        
        public bool IsActive { get; set; }

        public Category Category { get; set; } = null!;

        public int CategoryId { get; set; }

        public Brand Brand { get; set; } = null!;

        public int BrandId { get; set; }

        public ICollection<ProductImages> ProductImages { get; set; } = null!;
    }
}