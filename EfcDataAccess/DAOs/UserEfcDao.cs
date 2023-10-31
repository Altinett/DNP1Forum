using Microsoft.EntityFrameworkCore.ChangeTracking;
using Shared.Models;

namespace EfcDataAccess.DAOs;

public class UserEfcDao
{
    private readonly PostContext context;

    public UserEfcDao(PostContext context)
    {
        this.context = context;
    }

    public async Task<User> CreateAsync(User user)
    {
        EntityEntry<User> newUser = await context.Users.AddAsync(user);
        await context.SaveChangesAsync();
        return newUser.Entity;
    }
    
    
}