using Microsoft.EntityFrameworkCore.Metadata.Internal;
using OnlineStore.Infrastructure.Data.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineStore.Infrastructure.Data.Models
{
    public class Order
    {
        public int Id { get; set; }

        public string UserId { get; set; } = null!;

        public User User { get; set; } = null!;

        public int ProductId { get; set; }

        public Product Product { get; set; } = null!;

        public int Quantity { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        public int CityId { get; set; }

        public City City { get; set; } = null!;

        public int CountryId { get; set; }

        public Country Country { get; set; } = null!;

        [Required]
        [StringLength(50)]
        public string Address { get; set; } = null!;

        [Required]
        [StringLength(10)]
        public string PhoneNumber { get; set; } = null!;
    }
}