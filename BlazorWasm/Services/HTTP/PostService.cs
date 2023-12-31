﻿using System.Text;
using Newtonsoft.Json;
using Shared.DTOs;
using Shared.Models;

namespace BlazorWasm.Services.Http;

public class PostService : IPostService
{
    private readonly HttpClient client = new ();

    public async Task CreatePost(int ownerId, string title, string body)
    {
        PostCreationDto postCreationDto = new PostCreationDto
        {
            OwnerId = ownerId,
            Title = title,
            Body = body
        };

        string postAsJson = JsonConvert.SerializeObject(postCreationDto);
        StringContent content = new StringContent(postAsJson, Encoding.UTF8, "application/json");
        Console.WriteLine(postAsJson);
        HttpResponseMessage response = await client.PostAsync("http://localhost:5096/posts", content);

        string responseContent = await response.Content.ReadAsStringAsync();
        Console.WriteLine(responseContent);

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(responseContent);
        }
    }

    public async Task<Post> GetPost(int postId)
    {
        HttpResponseMessage response = await client.GetAsync($"http://localhost:5096/Posts?postId={postId}");
        if (response.IsSuccessStatusCode)
        {
            string responseData = await response.Content.ReadAsStringAsync();
        
            // Deserialize the response as a List<Post>
            List<Post> posts = JsonConvert.DeserializeObject<List<Post>>(responseData);
            
            // If the response is an array, take the first item
            Post post = posts.FirstOrDefault();
        
            return post;
        }
        else
        {
            throw new Exception("Failed to retrieve post titles: " + response.ReasonPhrase);
        }
    }

    public async Task<List<Post>> GetAllPostAsync()
    {
        HttpResponseMessage response = await client.GetAsync("http://localhost:5096/posts");

        if (response.IsSuccessStatusCode)
        {
            string responseData = await response.Content.ReadAsStringAsync();
            List<Post> posts = JsonConvert.DeserializeObject<List<Post>>(responseData);
            return posts;
        }
        else
        {
            throw new Exception("Failed to retrieve post titles: " + response.ReasonPhrase);
        }
    }
}