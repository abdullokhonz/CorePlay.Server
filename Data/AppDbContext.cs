using CorePlay.Entity;
using Microsoft.EntityFrameworkCore;

namespace CorePlay.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
        
    }
    public DbSet<UserEntity> Users { get; set; }
    public DbSet<GameEntity> Games { get; set; }
}