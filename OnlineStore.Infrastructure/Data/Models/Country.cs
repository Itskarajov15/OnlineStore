using System.ComponentModel.DataAnnotations;

namespace OnlineStore.Infrastructure.Data.Models
{
    public class Country
    {
        public Country()
        {
            this.Cities = new HashSet<City>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; } = null!;

        public ICollection<City> Cities { get; set; }
    }
}