using CourseProject.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace CourseProject.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) {
            Database.EnsureCreated();
        }
        public DbSet<Brand> Brand { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<OrderItem> OrderItem { get; set; }
        public DbSet<Cart> Cart { get; set; }
        public DbSet<CartItem> CartItem { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Phone> Phone { get; set; }
        public DbSet<SmartPhone> SmartPhone { get; set; }
        public DbSet<CellPhone> CellPhone { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // TPH конфигурация
            modelBuilder.Entity<Phone>()
                .HasDiscriminator<string>("PhoneType")
                .HasValue<SmartPhone>("SmartPhone")
                .HasValue<CellPhone>("CellPhone");
        }
    }
}
