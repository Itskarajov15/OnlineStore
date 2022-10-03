using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineStore.Core.Models
{
    public class AddProductViewModel
    {
        [Required]
        [StringLength(60, MinimumLength = 5, ErrorMessage = "{0} must be between {2} and {1} symbols.")]
        public string Title { get; set; } = null!;

        [Required]
        [StringLength(1000, MinimumLength = 10, ErrorMessage = "{0} must be between {2} and {1} symbols.")]
        public string Specifications { get; set; } = null!;

        [Required]
        [StringLength(100, MinimumLength = 10, ErrorMessage = "{0} must be between {2} and {1} symbols.")]
        public string Description { get; set; } = null!;

        [Column(TypeName = "decimal(18,2)")]
        [Range(typeof(decimal), "0.00", "79228162514264337593543950335")]
        public decimal Price { get; set; }

        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "Please select files")]
        [Display(Name = "Product Images")]
        public List<IFormFile> ProductImages { get; set; } = null!;

        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        public IEnumerable<CategoryViewModel>? Categories { get; set; }

        [Display(Name = "Brand")]
        public int BrandId { get; set; }

        public IEnumerable<BrandViewModel>? Brands { get; set; }
    }
}