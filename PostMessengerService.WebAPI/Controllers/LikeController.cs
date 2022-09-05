using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PostMessengerService.Application.Services;
using PostMessengerService.Domain.Dto;

namespace PostMessengerService.WebAPI.Controllers;

[ApiController]
public class LikeController : ControllerBase
{
    private readonly LikeService _likeService;

    public LikeController(LikeService likeService)
    {
        _likeService = likeService;
    }

    [Authorize(AuthenticationSchemes = "TokenKey")]
    [HttpGet("post/{postId}/likes")]
    public async Task<IEnumerable<LikeInformationDto>> GetListOfLikesByPost(int postId)
    {
        var like = await _likeService.GerListOfLikesByPost(postId);
        return like;
    }

    [Authorize(AuthenticationSchemes = "TokenKey")]
    [HttpPost("post/like-create")]
    public async Task PostLike(int postId)
    {
        await _likeService.CreateLike(postId);
    }

    [Authorize(AuthenticationSchemes = "TokenKey")]
    [HttpDelete("like/delete")]
    public async Task DeleteLike(int likeId)
    {
        await _likeService.DeleteLike(likeId);
    }

    [Authorize(AuthenticationSchemes = "TokenKey", Roles = "admin")]
    [HttpDelete("like/delete-all")]
    public async Task DeleteAllLikes(int postId)
    {
        await _likeService.DeleteAllLikes(postId);
    }
}