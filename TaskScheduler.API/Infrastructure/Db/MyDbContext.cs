using Microsoft.EntityFrameworkCore;
using TaskScheduler.API.Domain.Models;

namespace TaskScheduler.API.Infrastructure.Db
{
    public class MyDbContext : DbContext
    {
        private readonly IConfiguration _config;

        public MyDbContext(IConfiguration config)
        {
            _config = config;
        }

        public DbSet<User> Users { get; set; } = default!;
        public DbSet<MyTask> Tasks { get; set; } = default!;
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MyTask>()
                .Property(t => t.Status)
                .HasConversion<string>();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = _config.GetConnectionString("mysql") ?? throw new Exception("Connection String Is Missing.");

            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySql(
                    connectionString,
                    ServerVersion.AutoDetect(connectionString)
                );
            }
        }
    }
}