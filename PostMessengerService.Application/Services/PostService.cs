using AutoMapper;
using PostMessengerService.Application.Middlewares;
using PostMessengerService.Domain.Dto;
using PostMessengerService.Domain.Models;
using PostMessengerService.Infrastructure.Repositories;

namespace PostMessengerService.Application.Services;

public class PostService : IPostService
{
    private readonly IMapper _mapper;
    private readonly UnitOfWork _unitOfWork;
    private readonly IUserProviderMiddleware _userProviderMiddleware;

    public PostService(UnitOfWork unitOfWork, IMapper mapper, IUserProviderMiddleware userProviderMiddleware)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _userProviderMiddleware = userProviderMiddleware;
    }

    public async Task<IEnumerable<PostModel>> GerListOfPosts()
    {
        return await _unitOfWork.PostRepository.GetEntityListAsync();
    }

    public async Task<IEnumerable<PostInformationDto>> GerListOfPostsByUsername(string username)
    {
        var postToMap = await _unitOfWork.PostRepository.GetEntityListAsyncByUserId(username);
        return _mapper.Map<IEnumerable<PostInformationDto>>(postToMap);
    }

    public async Task<PostInformationDto> GetPost(int postId)
    {
        var postToMap = await _unitOfWork.PostRepository.GetEntityByIdAsync(postId);
        return _mapper.Map<PostInformationDto>(postToMap);
    }

    public async Task CreatePost(PostCreationDto postToMap)
    {
        var postMapped = _mapper.Map<PostModel>(postToMap);
        postMapped.CreationDate = DateTime.UtcNow;
        postMapped.Username = _userProviderMiddleware.GetUsername();
        _unitOfWork.PostRepository.PostEntity(postMapped);
        await _unitOfWork.SaveAsync();
    }

    public async Task UpdatePost(PostChangeDto postChanged, int postId)
    {
        var username = _userProviderMiddleware.GetUsername();
        var post = await _unitOfWork.PostRepository.GetEntityByIdAsync(postId);
        if (username == post.Username)
        {
            _mapper.Map(postChanged, post);
            await _unitOfWork.SaveAsync();
        }
        else
        {
            throw new AccessViolationException("Current user doesn't match with post owner");
        }
    }

    public async Task DeletePost(int postId)
    {
        var username = _userProviderMiddleware.GetUsername();
        var postToValidate = await _unitOfWork.PostRepository.GetEntityByIdAsync(postId);

        if (username == postToValidate.Username)
        {
            _unitOfWork.CommentRepository.DeleteAllEntitiesByPostId(postId);
            _unitOfWork.LikeRepository.DeleteAllEntitiesByPostId(postId);
            _unitOfWork.PostRepository.DeleteEntity(postId);
            await _unitOfWork.SaveAsync();
        }
        else
        {
            throw new AccessViolationException("Current user doesn't match with post owner");
        }
    }
}