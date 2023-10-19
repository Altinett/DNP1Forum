using System.Text;
using Newtonsoft.Json;
using Shared.DTOs;

namespace BlazorWasm.Services.Http;

public class UserService : IUserService
{
    private readonly HttpClient client = new ();


    public async Task CreateUser(string name, string username, string password, string email, int age)
    {
        UserCreationDto userCreationDto = new UserCreationDto
        {
            Name = name,
            Username = username,
            Password = password,
            Email = email,
            Age = age
        };
        
        string postAsJson = JsonConvert.SerializeObject(userCreationDto);
        StringContent content = new StringContent(postAsJson, Encoding.UTF8, "application/json");
        Console.WriteLine(postAsJson);
        HttpResponseMessage response = await client.PostAsync("http://localhost:5096/users", content);
        
        string responseContent = await response.Content.ReadAsStringAsync();
        Console.WriteLine(responseContent);

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(responseContent);
        }
    }
}