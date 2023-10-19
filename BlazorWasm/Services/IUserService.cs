namespace BlazorWasm.Services;

public interface IUserService
{
    public Task CreateUser(string name, string username, string password, string email, int age);
    
    //public Task GetUser(int ownerId)
}