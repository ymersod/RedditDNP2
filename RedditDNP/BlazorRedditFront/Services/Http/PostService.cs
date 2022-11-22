using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Shared.Dtos;
using Shared.Models;

namespace BlazorRedditFront.Services.Http;

public class PostService : IPostService
{
    private readonly HttpClient client = new();
    public async Task<ICollection<PostBasicDto>> GetAllAsync()
    {
        var response = await client.GetAsync("https://localhost:7206/Reddit/Posts");
        string postValues = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(postValues);
        }

        PostBasicDto[] posts = JsonSerializer.Deserialize<PostBasicDto[]>(postValues, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;

        return posts;
    }

    public async Task CreatePostAsync(string title, string mainText, string nickName, string username)
    {
        //Skal gøres på authorized metoder i wep api
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", JwtAuthService.jwt);
        
        string postAsJson = JsonSerializer.Serialize(CreatePostDto(title, mainText, nickName, username));
        StringContent content = new(postAsJson, Encoding.UTF8, "application/json");
        HttpResponseMessage response = await client.PostAsync("https://localhost:7206/Reddit/Create", content);
        string responseContent = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(responseContent);
        }
    }

    public async Task<PostFullDto> GetAsync(int id)
    {
        string postAsJson = JsonSerializer.Serialize(CreateSelectedPostDto(id));
        StringContent content = new(postAsJson, Encoding.UTF8, "application/json");
        var response = await client.PostAsync("https://localhost:7206/Reddit/Post",content);
        string postValues = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(postValues);
        }

        PostFullDto post = JsonSerializer.Deserialize<PostFullDto>(postValues, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;

        return post;
    }

    private PostCreationDto CreatePostDto(string title, string mainText, string nickName, string username)
    {
        PostCreationDto postToCreate = new PostCreationDto
        {
            PostTitle = title,
            PostContext = mainText,
            NickName = nickName,
            Username = username
        };
        return postToCreate;
    }

    private SelectedPostDto CreateSelectedPostDto(int id)
    {
        SelectedPostDto postDto = new SelectedPostDto
        {
            id = id
        };
        return postDto;
    }
}