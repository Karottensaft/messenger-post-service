using AutoMapper;
using PostMessengerService.Application.Middlewares;
using PostMessengerService.Domain.Dto;
using PostMessengerService.Domain.Models;
using PostMessengerService.Infrastructure.Repositories;

namespace PostMessengerService.Application.Services;

public class CommentService : ICommentService
{
    private readonly IMapper _mapper;
    private readonly UnitOfWork _unitOfWork;
    private readonly IUserProviderMiddleware _userProviderMiddleware;

    public CommentService(UnitOfWork unitOfWork, IMapper mapper, IUserProviderMiddleware userProviderMiddleware)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _userProviderMiddleware = userProviderMiddleware;
    }

    public async Task<IEnumerable<CommentInformationDto>> GerListOfCommentsByPostId(int postId)
    {
        var commentsToMap = await _unitOfWork.CommentRepository.GetEntityListByPostIdAsync(postId);
        var commentsMapped = _mapper.Map<IEnumerable<CommentInformationDto>>(commentsToMap);
        return commentsMapped.ToList();
    }

    public async Task CreateComment(CommentCreationDto commentToMap, int postId)
    {
        var commentMapped = _mapper.Map<CommentModel>(commentToMap);
        commentMapped.Username = _userProviderMiddleware.GetUsername();
        commentMapped.PostId = postId;
        commentMapped.CreationDate = DateTime.UtcNow;
        _unitOfWork.CommentRepository.PostEntity(commentMapped);
        await _unitOfWork.SaveAsync();
    }

    public async Task UpdateComment(CommentChangeDto commentToMap, int commentId)
    {
        var username = _userProviderMiddleware.GetUsername();
        var comment = await _unitOfWork.CommentRepository.GetEntityByIdAsync(commentId);
        if (username == comment.Username)
        {
            _mapper.Map(commentToMap, comment);
            await _unitOfWork.SaveAsync();
        }
        else
        {
            throw new AccessViolationException("Current user doesn't match with comment owner");
        }
    }

    public async Task DeleteComment(int commentId)
    {
        var username = _userProviderMiddleware.GetUsername();
        var commentToValidate = await _unitOfWork.CommentRepository.GetEntityByIdAsync(commentId);
        if (username == commentToValidate.Username)
        {
            _unitOfWork.CommentRepository.DeleteEntity(commentId);
            await _unitOfWork.SaveAsync();
        }
        else
        {
            throw new AccessViolationException("Current user doesn't match with comment owner");
        }
    }

    public async Task DeleteAllComments(int postId)
    {
        _unitOfWork.CommentRepository.DeleteAllEntitiesByPostId(postId);
        await _unitOfWork.SaveAsync();
    }
}