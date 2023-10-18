using Shared.Models;

namespace BlazorWasm.Services;

public interface IPostService
{
    public Task CreatePost(int ownerId, string title, string body);

    public Task<Post> GetPost(int postId);

    public Task<List<Post>> GetAllPostAsync();
}