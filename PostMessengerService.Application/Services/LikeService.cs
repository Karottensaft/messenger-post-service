using AutoMapper;
using PostMessengerService.Application.Middlewares;
using PostMessengerService.Domain.Dto;
using PostMessengerService.Domain.Models;
using PostMessengerService.Infrastructure.Repositories;

namespace PostMessengerService.Application.Services;

public class LikeService : ILikeService
{
    private readonly IMapper _mapper;
    private readonly UnitOfWork _unitOfWork;
    private readonly IUserProviderMiddleware _userProviderMiddleware;

    public LikeService(UnitOfWork unitOfWork, IMapper mapper, IUserProviderMiddleware userProviderMiddleware)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _userProviderMiddleware = userProviderMiddleware;
    }

    public async Task<IEnumerable<LikeInformationDto>> GerListOfLikesByPost(int postId)
    {
        var likesToMap = await _unitOfWork.LikeRepository.GetEntityListAsyncByPostId(postId);
        var likes = _mapper.Map<IEnumerable<LikeInformationDto>>(likesToMap);
        return likes.ToList();
    }

    public async Task CreateLike(int postId)
    {
        var likeToMap = new LikeCreationDto();
        likeToMap.Username = _userProviderMiddleware.GetUsername();
        likeToMap.PostId = postId;
        var likeToValidate =
            await _unitOfWork.LikeRepository.GetEntityByUsernameAndPostIdAsync(likeToMap.Username, likeToMap.PostId);

        if (likeToValidate == null)
        {
            var likeMapped = _mapper.Map<LikeModel>(likeToMap);
            likeMapped.Username = _userProviderMiddleware.GetUsername();
            likeMapped.PostId = postId;
            likeMapped.CreationDate = DateTime.UtcNow;
            _unitOfWork.LikeRepository.PostEntity(likeMapped);
            await _unitOfWork.SaveAsync();
        }
        else
        {
            throw new ArgumentException("Like already exist");
        }
    }

    public async Task DeleteLike(int likeId)
    {
        var username = _userProviderMiddleware.GetUsername();
        var likeToValidate = await _unitOfWork.PostRepository.GetEntityByIdAsync(likeId);

        if (username == likeToValidate.Username)
        {
            _unitOfWork.LikeRepository.DeleteEntity(likeId);
            await _unitOfWork.SaveAsync();
        }
        else
        {
            throw new AccessViolationException("Current user doesn't match with like owner");
        }
    }

    public async Task DeleteAllLikes(int postId)
    {
        _unitOfWork.LikeRepository.DeleteAllEntitiesByPostId(postId);
        await _unitOfWork.SaveAsync();
    }
}