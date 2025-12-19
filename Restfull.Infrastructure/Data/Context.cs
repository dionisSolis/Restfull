using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;
using Restfull.Domain.Entities;

namespace Restfull.Infrastructure.Data
{
    public class Context : DbContext
    {
        public Context() { }

        public Context(DbContextOptions<Context> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json")
                    .Build();

                optionsBuilder.UseSqlServer(configuration.GetConnectionString("Database"));
            }
        }

        public DbSet<Resource> Resources { get; set; }
        public DbSet<Software> Software { get; set; }
        public DbSet<Equipment> Equipment { get; set; }
        public DbSet<Furniture> Furniture { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Location> Locations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Настройка TPT (Table Per Type) наследования для Resource
            modelBuilder.Entity<Resource>().ToTable("Resources");
            modelBuilder.Entity<Software>().ToTable("Software");
            modelBuilder.Entity<Equipment>().ToTable("Equipment");
            modelBuilder.Entity<Furniture>().ToTable("Furniture");
            modelBuilder.Entity<Room>().ToTable("Rooms");

            // Настройка связей
            modelBuilder.Entity<Resource>()
                .HasOne(r => r.Supplier)
                .WithMany()
                .HasForeignKey(r => r.SupplierId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Resource>()
                .HasOne(r => r.Location)
                .WithMany()
                .HasForeignKey(r => r.LocationId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Location>()
                .HasOne(l => l.Room)
                .WithMany()
                .HasForeignKey(l => l.RoomId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}