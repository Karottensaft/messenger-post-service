using AutoMapper;
using PostMessengerService.Domain.Dto;
using PostMessengerService.Domain.Models;
using PostMessengerService.Infrastructure.Repositories;

namespace PostMessengerService.Application.Services
{
    public class PostService
    {
        private readonly IMapper _mapper;
        private readonly UnitOfWork _unitOfWork;

        public PostService(UnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
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
            postMapped.Username = "Passion";
            _unitOfWork.PostRepository.PostEntity(postMapped);
            await _unitOfWork.SaveAsync();
        }

        public async Task UpdatePost(PostChangeDto postChanged, int postId)
        {
            var username = "Passion";
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
            var username = "Passion";
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
}
