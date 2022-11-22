using System.ComponentModel.DataAnnotations;

namespace Shared.Models;

public class RedditPost
{
    public int index { get; set; }
    public DateTime Date { get; set; }
    public string? PostTitle { get; set; }
    public string? PostContext {get; set; }
    public User Author { get; set; }
    
    public RedditPost(){}
}