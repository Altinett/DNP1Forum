using System.ComponentModel.DataAnnotations;
using Application.Logic;
using EfcDataAccess.DAOs;
using FileData;
using Shared.DTOs;
using Shared.Models;

namespace WebAPI.Services;

public class AuthService : IAuthService
{
    private UserEfcDao userEfcDao;
    public AuthService(UserEfcDao userEfcDao)
    {
        this.userEfcDao = userEfcDao;
    }

    public async Task<User> ValidateUser(string username, string password)
    {
        /*
        User existingUser = users.FirstOrDefault(u =>
            u.UserName.Equals(username, StringComparison.OrdinalIgnoreCase));
            */
        User? existingUser = await userEfcDao.GetByUsernameAsync(username);
        if (existingUser == null)
        {
            throw new Exception("User not found");
        }

        if (!existingUser.PassWord.Equals(password))
        {
            throw new Exception("Password mismatch");
        }

        return existingUser;
    }

    public Task RegisterUser(User user)
    {

        if (string.IsNullOrEmpty(user.UserName))
        {
            throw new ValidationException("Username cannot be null");
        }

        if (string.IsNullOrEmpty(user.PassWord))
        {
            throw new ValidationException("Password cannot be null");
        }

        userEfcDao.CreateAsync(user);
        return Task.CompletedTask;
    }
}