namespace Shared.DTOs;

public class SearchTodoParametersDto
{
    public string? Username { get;}
    public int? UserId { get;}
    public string? TitleContains { get;}

    public SearchTodoParametersDto(string? username, int? userId, string? titleContains)
    {
        Username = username;
        UserId = userId;
        TitleContains = titleContains;
    }
}