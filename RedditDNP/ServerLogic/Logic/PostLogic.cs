using ServerLogic.Daos;
using Shared.Dtos;
using Shared.Models;

namespace ServerLogic.Logic;

public class PostLogic : IPostLogic
{
    private readonly IPostDao postDao;
    private readonly IUserDao userDao;

    public PostLogic(IPostDao postDao, IUserDao userDao)
    {
        this.postDao = postDao;
        this.userDao = userDao;
    }
    
    public async Task<RedditPost> CreatePostDtoAsync(PostCreationDto postToCreate)
    {

        User? user = await userDao.GetUser(postToCreate.Username);
        
        RedditPost post = new RedditPost()
        {
            PostTitle = postToCreate.PostTitle,
            PostContext = postToCreate.PostContext,
            Author = user,
            Date = DateTime.Now
        };

        RedditPost createdPost = await postDao.CreateNewPostAsync(post);

        return createdPost;
    }

    public async Task<IEnumerable<PostBasicDto>> GetAllAsync()
    {
        IEnumerable<RedditPost>? posts = await postDao.GetAllPostsAsync();


        List<PostBasicDto>? postBasicDtos = new List<PostBasicDto>();
        foreach (var post in posts) 
        {
            postBasicDtos.Add(new PostBasicDto
            {
                PostTitle = post.PostTitle,
                index = post.index
            });
        }
        return postBasicDtos;
    }

    public async Task<PostFullDto> GetAsync(SelectedPostDto postDto)
    {
        RedditPost? redditpost = await postDao.GetAsync(postDto.id);

        return new PostFullDto
        {
            PostTitle = redditpost.PostTitle,
            PostContext = redditpost.PostContext,
            PostCreator = redditpost.Author.Nickname,
            Date = redditpost.Date,
            index = redditpost.index
        };
    }
}