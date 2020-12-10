using System;
using Abouca.Domain.Information;
using Abouca.Domain.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Abouca.Database.Context
{
    public class MainContext:DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Information> Informations { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = Environment.GetEnvironmentVariable("CONNECTION_STR");
            if (string.IsNullOrEmpty(connectionString))
            {
                Console.WriteLine("The connection string is missing");
                Environment.Exit(0);
            }
            optionsBuilder.UseSqlServer(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasOne(m => m.About)
                .WithOne(m => m.User)
                .HasForeignKey<Information>(m => m.UserId);
            base.OnModelCreating(modelBuilder);
        }
    }
}
