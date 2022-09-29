using OnlineStore.Infrastructure.Data.Identity;
using System.ComponentModel.DataAnnotations;

namespace OnlineStore.Infrastructure.Data.Models
{
    public class Review
    {
        public int Id { get; set; }

        [Required]
        [StringLength(20)]
        public string Title { get; set; } = null!;

        public double Rating { get; set; }

        public int ProductId { get; set; }

        public Product Product { get; set; } = null!;

        public string UserId { get; set; } = null!;

        public User User { get; set; } = null!;

        public DateTime PublishedAt { get; set; }

        [Required]
        [StringLength(1000)]
        public string Content { get; set; } = null!;
    }
}