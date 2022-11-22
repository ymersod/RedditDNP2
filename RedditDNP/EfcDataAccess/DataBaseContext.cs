using Microsoft.EntityFrameworkCore;
using Shared.Models;

namespace EfcDataAccess;

public class DataBaseContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<RedditPost> RedditPosts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source = ../EfcDataAccess/Reddit.db");
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<RedditPost>().HasKey(post => post.index);
        modelBuilder.Entity<User>().HasKey(user => user.Username);
    }
    
}