using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace OnlineStore.Infrastructure.Data.Identity
{
    public class User : IdentityUser
    {
        [Required]
        [StringLength(30)]
        public string FirstName { get; set; } = null!;

        [Required]
        [StringLength(30)]
        public string LastName { get; set; } = null!;
    }
}