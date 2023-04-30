using Microsoft.EntityFrameworkCore;
using OnlinePasswordManager.Server.Data.Entities;
using System.Net;

namespace OnlinePasswordManager.Server.Data.Context
{
    public class OnlinePasswordManagerDbContext : DbContext
    {
        public DbSet<Category> categories { get; set; }
        public DbSet<User> users { get; set; }
        public DbSet<Password> password { get; set; }
        public DbSet<Login> logins { get; set; }
        public DbSet<Note> notes { get; set; }
        public DbSet<PasswordVersion> passwordsVersions { get; set; }

        public OnlinePasswordManagerDbContext(DbContextOptions<OnlinePasswordManagerDbContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Restaurant>()
            //    .Property(r => r.Name)
            //    .IsRequired()
            //    .HasMaxLength(25);

            //modelBuilder.Entity<Dish>()
            //    .Property(d => d.Name)
            //    .IsRequired();

            //modelBuilder.Entity<Dish>()
            //    .Property(d => d.Description)
            //    .HasDefaultValue("Description");

            //modelBuilder.Entity<Address>()
            //    .Property(a => a.City)
            //    .HasMaxLength(50);

            //modelBuilder.Entity<Address>()
            //    .Property(a => a.Street)
            //    .HasMaxLength(50);


        }

    }
}
