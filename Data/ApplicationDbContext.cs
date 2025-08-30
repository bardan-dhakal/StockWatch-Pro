using api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace api.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
        : IdentityDbContext<AppUser>(options)
    {
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Portfolio> Portfolios { get; set; }
        public DbSet<PriceAlert> PriceAlerts { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Portfolio>()
                .HasKey(p => new { p.AppUserId, p.StockId });

            builder.Entity<Portfolio>()
                .HasOne(p => p.AppUser)
                .WithMany(u => u.Portfolios)
                .HasForeignKey(p => p.AppUserId);
            
            builder.Entity<Portfolio>()
                .HasOne(p => p.Stock)
                .WithMany(s => s.Portfolios)
                .HasForeignKey(p => p.StockId);
                
            builder.Entity<PriceAlert>()
                .HasOne(pa => pa.AppUser)
                .WithMany(u => u.PriceAlerts)
                .HasForeignKey(pa => pa.AppUserId);
                
            builder.Entity<PriceAlert>()
                .HasOne(pa => pa.Stock)
                .WithMany(s => s.PriceAlerts)
                .HasForeignKey(pa => pa.StockId);
                
            List<IdentityRole> roles =
            [
                new() {
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                },
                new() {
                    Name = "User",
                    NormalizedName = "USER"
                }
            ];
            builder.Entity<IdentityRole>().HasData(roles);
        }
    }
}