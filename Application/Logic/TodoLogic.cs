using Application.DaoInterfaces;
using Application.LogicInterfaces;
using Shared.DTOs;
using Shared.Models;

namespace Application.Logic;

public class TodoLogic : ITodoLogic
{
    private readonly ITodoDao postDao;
    private readonly IUserDao userDao;

    public TodoLogic(ITodoDao postDao, IUserDao userDao)
    {
        this.postDao = postDao;
        this.userDao = userDao;
    }
    
    public async Task<Post> CreateAsync(TodoCreationDto dto)
    {
        User? user = await userDao.GetByIdAsync(dto.OwnerId);
        if (user == null)
        {
            throw new Exception($"User with id {dto.OwnerId} was not found.");
        }

        ValidatePost(dto);
        Post post = new Post(user, dto.Title, dto.Body);
        Post created = await postDao.CreateAsync(post);
        return created;
    }

    public Task<IEnumerable<Post>> GetAsync(SearchTodoParametersDto searchParameters)
    {
        return postDao.GetAsync(searchParameters);
    }

    private void ValidatePost(TodoCreationDto dto)
    {
        if (string.IsNullOrEmpty(dto.Title)) throw new Exception("Title cannot be empty.");
        
        if (string.IsNullOrEmpty(dto.Body)) throw new Exception("Body cannot be empty.");

        // other validation stuff
    }
    
    public async Task UpdateAsync(PostUpdateDto dto)
    {
        Post? existing = await postDao.GetByIdAsync(dto.Id);

        if (existing == null)
        {
            throw new Exception($"Todo with ID {dto.Id} not found!");
        }

        User? user = null;
        if (dto.OwnerId != null)
        {
            user = await userDao.GetByIdAsync((int)dto.OwnerId);
            if (user == null)
            {
                throw new Exception($"User with id {dto.OwnerId} was not found.");
            }
        }
        

        User userToUse = user ?? existing.Owner;
        string titleToUse = dto.Title ?? existing.Title;
        string bodyToUse = dto.Body ?? existing.Body;
    
        Post updated = new (userToUse, titleToUse, bodyToUse)
        {
            Id = existing.Id,
        };

        ValidateTodo(updated);

        await postDao.UpdateAsync(updated);
    }

    private void ValidateTodo(Post dto)
    {
        if (string.IsNullOrEmpty(dto.Title)) throw new Exception("Title cannot be empty.");
        // other validation stuff
    }
    
    public async Task DeleteAsync(int id)
    {
        Post? todo = await postDao.GetByIdAsync(id);
        if (todo == null)
        {
            throw new Exception($"Todo with ID {id} was not found!");
        }

        await postDao.DeleteAsync(id);
    }
    
}