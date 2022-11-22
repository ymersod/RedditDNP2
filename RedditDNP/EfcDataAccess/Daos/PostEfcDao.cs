using System.Collections;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using ServerLogic.Daos;
using Shared.Models;

namespace EfcDataAccess.Daos;

public class PostEfcDao : IPostDao
{

    private readonly DataBaseContext _dataBaseContext;

    public PostEfcDao(DataBaseContext _dataBaseContext)
    {
        this._dataBaseContext = _dataBaseContext;
    }
    public async Task<RedditPost> CreateNewPostAsync(RedditPost post)
    {
        EntityEntry<RedditPost> newPost = await _dataBaseContext.RedditPosts.AddAsync(post);
        await _dataBaseContext.SaveChangesAsync();
        return newPost.Entity;
    }

    public async Task<IEnumerable<RedditPost>> GetAllPostsAsync()
    {
        List<RedditPost> posts = new List<RedditPost>();
        await _dataBaseContext.RedditPosts.ForEachAsync(post => posts.Add(post));
        Console.WriteLine(posts.Count);
        return posts;
    }

    public async Task<RedditPost> GetAsync(int id)
    {

        IQueryable<RedditPost> redditpost = _dataBaseContext.RedditPosts.Include(
            post => post.Author);

        redditpost = redditpost.Where(post => post.index == id);

        List<RedditPost> posts = await redditpost.ToListAsync();
        return posts.First();
    }
    
    
}