using Microsoft.EntityFrameworkCore;
using Shared.Models;


namespace EfcDataAccess;

public class PostContext : DbContext
{
    public DbSet<User> Users { get; set; }
    
    public DbSet<Post> Posts { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source = ../WebAPI/Todo.db");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Post>().HasKey(todo => todo.Id);
        modelBuilder.Entity<User>().HasKey(user => user.Id);
    }
}