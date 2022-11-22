using Shared.Dtos;
using Shared.Models;

namespace ServerLogic.Logic;

public interface IPostLogic
{
    Task<RedditPost> CreatePostDtoAsync(PostCreationDto userToCreate);
    Task<IEnumerable<PostBasicDto>> GetAllAsync();

    Task<PostFullDto> GetAsync(SelectedPostDto postDto);
}