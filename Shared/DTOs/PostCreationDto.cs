﻿namespace Shared.DTOs;

public class PostCreationDto
{
    public int OwnerId { get; init; }
    public string Title { get; init; }
    public string Body { get; init; }
}