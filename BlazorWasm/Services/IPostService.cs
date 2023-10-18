using Shared.Models;

namespace BlazorWasm.Services;

public interface IPostService
{
    public Task CreatePost(int ownerId, string title, string body);

    public Task GetPost(Post post);

    public Task<List<Post>> GetAllPostAsync();
}