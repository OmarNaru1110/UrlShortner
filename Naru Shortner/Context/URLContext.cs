using Microsoft.EntityFrameworkCore;
using Naru_Shortner.Models;

namespace Naru_Shortner.Context
{
    public class UrlContext:DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
            optionsBuilder.UseSqlServer(config.GetConnectionString("default"));
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Url>(u => {
                u.HasKey(x=>x.Id);
            });
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Url> URLs { get; set; }
    }
}
