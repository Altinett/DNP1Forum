using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using Shared.Models;

namespace WebAPI.Services;

public class AuthService : IAuthService
{
    private string jsonFilePath = "data.json";
    private List<User> users;

    public AuthService()
    {
        LoadUsers();
    }

    private void LoadUsers()
    {
        try
        {
            // Check if the file exists
            if (File.Exists(jsonFilePath))
            {
                // Load users from the JSON file
                string jsonData = File.ReadAllText(jsonFilePath);

                // Deserialize JSON data to User objects
                var data = JsonConvert.DeserializeObject<JsonData>(jsonData);
                users = data?.Users ?? new List<User>();
                
                // Set Age and SecurityLevel to null and 1 respectively
            }
            else
            {
                // Handle the case when the file does not exist
                Console.WriteLine($"Error: JSON file not found at '{jsonFilePath}'. Initializing empty user list.");
                users = new List<User>();
            }
        }
        catch (Exception ex)
        {
            // Handle other exceptions (e.g., malformed JSON)
            Console.WriteLine($"Error: {ex.Message}. Initializing empty user list.");
            users = new List<User>();
        }
    }
    
    private class JsonData
    {
        public List<User> Users { get; set; }
    }

    public Task<User> ValidateUser(string username, string password)
    {
        User existingUser = users.FirstOrDefault(u =>
            u.UserName.Equals(username, StringComparison.OrdinalIgnoreCase));

        if (existingUser == null)
        {
            throw new Exception("User not found");
        }

        if (!existingUser.PassWord.Equals(password))
        {
            throw new Exception("Password mismatch");
        }

        return Task.FromResult(existingUser);
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
        // Do more user info validation here
        
        // save to persistence instead of list
        
        users.Add(user);
        
        return Task.CompletedTask;
    }
}