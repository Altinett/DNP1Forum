using Shared.DTOs;
using Shared.Models;

namespace Application.DaoInterfaces;

public interface ITodoDao
{
    Task<Post> CreateAsync(Post post);
    Task<IEnumerable<Post>> GetAsync(SearchTodoParametersDto searchParameters);
    
    Task UpdateAsync(Post post);
    
    Task<Post> GetByIdAsync(int id);
    
    Task DeleteAsync(int id);
}