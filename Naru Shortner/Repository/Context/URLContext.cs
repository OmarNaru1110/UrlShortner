using Microsoft.EntityFrameworkCore;
using Naru_Shortner.Models;

namespace Naru_Shortner.Repository.Context
{
    public class UrlContext : DbContext
    {
        public UrlContext(DbContextOptions options):base(options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Url>(u =>
            {
                u.HasKey(x => x.Id);
            });
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Url> URLs { get; set; }
    }
}
