using Shared.Models;

namespace ServerLogic.Daos;

public interface IPostDao
{
    Task<RedditPost> CreateNewPostAsync(RedditPost post);

    Task<IEnumerable<RedditPost>> GetAllPostsAsync();

    Task<RedditPost> GetAsync(int id);
}