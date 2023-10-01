using Shared.DTOs;
using Shared.Models;

namespace Application.LogicInterfaces;

public interface ITodoLogic
{
    Task<Post> CreateAsync(TodoCreationDto dto);
    Task<IEnumerable<Post>> GetAsync(SearchTodoParametersDto searchParameters);
    
    Task UpdateAsync(PostUpdateDto post);
    
    Task DeleteAsync(int id);
}