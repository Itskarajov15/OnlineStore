using System.ComponentModel.DataAnnotations;

namespace OnlineStore.Infrastructure.Data.Models
{
    public class City
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; } = null!;

        public int CountryId { get; set; }

        public Country Country { get; set; } = null!;
    }
}