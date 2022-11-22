using Shared.Models;

namespace FileData;

public class DataContainer
{
    public ICollection<User> Users { get; set; }
    public ICollection<RedditPost> Posts { get; set; }
    
}