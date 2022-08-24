using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PostMessengerService.Application.Services;
using PostMessengerService.Domain.Dto;

namespace PostMessengerService.WebAPI.Controllers
{
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly CommentService _commentService;

        public CommentController(CommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpGet("comment/{postId}/comments")]
        public async Task<IEnumerable<CommentInformationDto>> GetListOfCommentsByPost(int postId)
        {
            var comment = await _commentService.GerListOfCommentsByPostId(postId);
            return comment;
        }

        [HttpPost("user/comment-create")]
        public async Task<CommentCreationDto> PostComment(CommentCreationDto comment, int postId)
        {
            await _commentService.CreateComment(comment, postId);
            return comment;
        }

        [HttpPut("comment/comment-change")]
        public async Task<CommentChangeDto> PutComment(CommentChangeDto comment, int commentId)
        {
            await _commentService.UpdateComment(comment, commentId);
            return comment;
        }

        [HttpDelete("comment/delete")]
        public async Task DeleteComment(int commentId)
        {
            await _commentService.DeleteComment(commentId);
        }

        [Authorize(Roles = "admin")]
        [HttpDelete("comment/delete-all")]
        public async Task DeleteAllComments(int postId)
        {
            await _commentService.DeleteAllComments(postId);
        }
    }
}
