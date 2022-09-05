using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PostMessengerService.Application.Services;
using PostMessengerService.Domain.Dto;
using PostMessengerService.Domain.Models;

namespace PostMessengerService.WebAPI.Controllers;

[ApiController]
public class PostController : ControllerBase
{
    private readonly IPostService _postService;

    public PostController(IPostService postService)
    {
        _postService = postService;
    }

    [Authorize(AuthenticationSchemes = "TokenKey", Roles = "admin")]
    [HttpGet("post/all")]
    public async Task<IEnumerable<PostModel>> GetListOfPosts()
    {
        var post = await _postService.GerListOfPosts();
        return post;
    }

    [Authorize(AuthenticationSchemes = "TokenKey")]
    [HttpGet("user/{username}/posts")]
    public async Task<IEnumerable<PostInformationDto>> GetListOfPostsByUser(string username)
    {
        var post = await _postService.GerListOfPostsByUsername(username);
        return post;
    }

    [Authorize(AuthenticationSchemes = "TokenKey")]
    [HttpGet("post/{postId}")]
    public async Task<PostInformationDto> GetPost(int postId)
    {
        var user = await _postService.GetPost(postId);
        return user;
    }

    [Authorize(AuthenticationSchemes = "TokenKey")]
    [HttpPost("user/post-create")]
    public async Task<PostCreationDto> PostPost(PostCreationDto post)
    {
        await _postService.CreatePost(post);
        return post;
    }

    [Authorize(AuthenticationSchemes = "TokenKey")]
    [HttpPut("post/post-change")]
    public async Task<PostChangeDto> PutPost(PostChangeDto post, int postId)
    {
        await _postService.UpdatePost(post, postId);
        return post;
    }

    [Authorize(AuthenticationSchemes = "TokenKey")]
    [HttpDelete("post/delete")]
    public async Task DeletePost(int postId)
    {
        await _postService.DeletePost(postId);
    }
}