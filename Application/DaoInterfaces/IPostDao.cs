using Shared.DTOs;
using Shared.Models;

namespace Application.DaoInterfaces;

public interface IPostDao
{
    Task<Post> CreateAsync(Post post);
    Task<IEnumerable<Post>> GetAsync(SearchPostParametersDto searchParameters);
    Task<IEnumerable<String>> GetAsync();
    Task UpdateAsync(Post post);
    
    Task<Post> GetByIdAsync(int id);
    
    Task DeleteAsync(int id);
}