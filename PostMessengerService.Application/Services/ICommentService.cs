using PostMessengerService.Domain.Dto;

namespace PostMessengerService.Application.Services;

public interface ICommentService
{
    Task<IEnumerable<CommentInformationDto>> GerListOfCommentsByPostId(int postId);

    Task CreateComment(CommentCreationDto commentToMap, int postId);

    Task UpdateComment(CommentChangeDto commentToMap, int commentId);

    Task DeleteComment(int commentId);

    Task DeleteAllComments(int postId);
}