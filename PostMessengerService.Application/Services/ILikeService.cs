using PostMessengerService.Domain.Dto;

namespace PostMessengerService.Application.Services;

public interface ILikeService
{
    Task<IEnumerable<LikeInformationDto>> GerListOfLikesByPost(int postId);

    Task CreateLike(int postId);

    Task DeleteLike(int likeId);

    Task DeleteAllLikes(int postId);
}