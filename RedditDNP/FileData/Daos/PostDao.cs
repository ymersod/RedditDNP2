using ServerLogic.Daos;
using Shared.Models;

namespace FileData.Daos;

public class PostDao : IPostDao
{
    private readonly FileContext FileContext;

    public PostDao(FileContext fileContext)
    {
        FileContext = fileContext;
    }
    
    public Task<RedditPost> CreateNewPostAsync(RedditPost post)
    {
        int postId = 1;

        if (FileContext.Posts.Any())
        {
            postId = FileContext.Posts.Max(p => p.index);
            postId++;
        }

        post.index = postId;
        
        FileContext.Posts.Add(post);
        FileContext.SaveChanges();

        return Task.FromResult(post);
    }

    public Task<IEnumerable<RedditPost>> GetAllPostsAsync()
    {
        IEnumerable<RedditPost>? redditPosts = FileContext.Posts;

        return Task.FromResult(redditPosts);
    }

    public Task<RedditPost> GetAsync(int id)
    {
        RedditPost? existing = FileContext.Posts.FirstOrDefault(u =>
            u.index.Equals(id)) ?? throw new ArgumentNullException("FileContext.Posts.FirstOrDefault(u =>\n            u.index.Equals(id))");
        return Task.FromResult(existing);
    }

    public Task<string> GetAuthor(int index)
    {
        throw new NotImplementedException();
    }
}