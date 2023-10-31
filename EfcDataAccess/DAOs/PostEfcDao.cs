using Microsoft.EntityFrameworkCore.ChangeTracking;
using Shared.Models;

namespace EfcDataAccess.DAOs;

public class PostEfcDao
{
    private readonly PostContext context;

    public PostEfcDao(PostContext context)
    {
        this.context = context;
    }
    
    public async Task<Post> CreateAsync(Post todo)
    {
        EntityEntry<Post> added = await context.Posts.AddAsync(todo);
        await context.SaveChangesAsync();
        return added.Entity;
    }
}