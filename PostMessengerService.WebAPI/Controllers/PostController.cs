using Microsoft.AspNetCore.Mvc;
using PostMessengerService.Application.Services;
using PostMessengerService.Domain.Dto;
using PostMessengerService.Domain.Models;

namespace PostMessengerService.WebAPI.Controllers
{
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly PostService _postService;

        public PostController(PostService postService)
        {
            _postService = postService;
        }

        [HttpGet("post/all")]
        public async Task<IEnumerable<PostModel>> GetListOfPosts()
        {
            var post = await _postService.GerListOfPosts();
            return post;
        }

        [HttpGet("user/{username}/posts")]
        public async Task<IEnumerable<PostInformationDto>> GetListOfPostsByUser(string username)
        {
            var post = await _postService.GerListOfPostsByUsername(username);
            return post;
        }

        [HttpGet("post/{postId}")]
        public async Task<PostInformationDto> GetPost(int postId)
        {
            var user = await _postService.GetPost(postId);
            return user;
        }

        [HttpPost("user/post-create")]
        public async Task<PostCreationDto> PostPost(PostCreationDto post)
        {
            await _postService.CreatePost(post);
            return post;
        }

        [HttpPut("post/post-change")]
        public async Task<PostChangeDto> PutPost(PostChangeDto post, int postId)
        {
            await _postService.UpdatePost(post, postId);
            return post;
        }

        [HttpDelete("post/delete")]
        public async Task DeletePost(int postId)
        {
            await _postService.DeletePost(postId);
        }
    }
}
