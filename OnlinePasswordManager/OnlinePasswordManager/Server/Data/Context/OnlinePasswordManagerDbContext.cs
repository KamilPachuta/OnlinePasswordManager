using Microsoft.EntityFrameworkCore;
using OnlinePasswordManager.Server.Data.Entities;
using System.Net;

namespace OnlinePasswordManager.Server.Data.Context
{
    public class OnlinePasswordManagerDbContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Password> Passwords { get; set; }
        public DbSet<Login> Logins { get; set; }
        public DbSet<Note> Notes { get; set; }
        public DbSet<PasswordVersion> PasswordsVersions { get; set; }

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
