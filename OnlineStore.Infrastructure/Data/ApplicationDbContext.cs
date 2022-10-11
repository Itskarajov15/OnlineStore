using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OnlineStore.Infrastructure.Data.Identity;
using OnlineStore.Infrastructure.Data.Models;

namespace OnlineStore.Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<City> Cities { get; set; } = null!;

        public DbSet<Country> Countries { get; set; } = null!;

        public DbSet<Product> Products { get; set; } = null!;

        public DbSet<Order> Orders { get; set; } = null!;

        public DbSet<Review> Reviews { get; set; } = null!;

        public DbSet<Brand> Brands { get; set; } = null!;

        public DbSet<Category> Categories { get; set; } = null!;

        public DbSet<CartItem> ShoppingCartItems { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Product>()
                .Property(x => x.IsActive)
                .HasDefaultValue(true);

            builder.Entity<City>()
                .HasOne<Country>(ct => ct.Country)
                .WithMany(c => c.Cities)
                .HasForeignKey(ct => ct.CountryId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}