using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServerLogic.Logic;
using Shared.Dtos;
using Shared.Models;

namespace RedditWebAPI.Controllers;

[ApiController]
[Route("Reddit")]
public class PostController : ControllerBase
{
    private IPostLogic PostLogic;

    public PostController(IPostLogic postLogic)
    {
        PostLogic = postLogic;
    }

    [HttpGet, Route("Posts")]
    public async Task<ActionResult> getAllAsync()
    {
        try
        {
            return Ok(await PostLogic.GetAllAsync());
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    [HttpPost, Route("Post")]
    public async Task<ActionResult> getAsync([FromBody] SelectedPostDto selectedPost)
    {
        try
        {
            
            PostFullDto post = await PostLogic.GetAsync(selectedPost);
            return Ok(post);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    [HttpPost, Route("Create"), Authorize]
    public async Task<ActionResult> CreatePostAsync([FromBody] PostCreationDto postToBeCreated)
    {
        try
        {
            await PostLogic.CreatePostDtoAsync(postToBeCreated);
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}