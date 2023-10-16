namespace Shared.DTOs;

public class UserCreationDto
{
    public string UserName { get; }
    public string Password { get; }
    public int age { get; }
    public string email { get; }
    public string name { get; }

    public UserCreationDto(string userName, string password, int age, string email, string name)
    {
        UserName = userName;
        Password = password;
        this.age = age;
        this.email = email;
        this.name = name;
    }
    
}