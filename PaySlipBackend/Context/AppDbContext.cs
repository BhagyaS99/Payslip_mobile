using Microsoft.EntityFrameworkCore;
using PaySlipBackend.Models;
using System.Net;

namespace PaySlipBackend.Context
{
    public class AppDbContext:DbContext
    {
        public AppDbContext()
        {
        }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<PayDetails> PayDetails { get; set; }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().ToTable("Employees");
            modelBuilder.Entity<PayDetails>().ToTable("PayDetails");
            modelBuilder.Entity<User>().ToTable("Users");
        }
    }
}
