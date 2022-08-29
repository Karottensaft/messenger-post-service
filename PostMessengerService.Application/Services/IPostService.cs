using PostMessengerService.Domain.Dto;
using PostMessengerService.Domain.Models;

namespace PostMessengerService.Application.Services;

internal interface IPostService
{
    Task<IEnumerable<PostModel>> GerListOfPosts();

    Task<IEnumerable<PostInformationDto>> GerListOfPostsByUsername(string username);

    Task<PostInformationDto> GetPost(int postId);

    Task CreatePost(PostCreationDto postToMap);

    Task UpdatePost(PostChangeDto postChanged, int postId);

    Task DeletePost(int postId);
}