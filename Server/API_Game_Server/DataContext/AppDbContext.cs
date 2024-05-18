using API_Game_Server.Models;
using Microsoft.EntityFrameworkCore;

namespace API_Game_Server.DataContext;

public class AppDbContext : DbContext
{
    public DbSet<Account> Accounts { get; set; }
    public DbSet<UserInfo> UserInfos { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Account>()
            .HasIndex(a => new { a.LoginProviderUserId, a.LoginProviderType })
            .IsUnique();
    }
}
