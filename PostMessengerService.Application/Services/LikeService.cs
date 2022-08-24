using AutoMapper;
using PostMessengerService.Domain.Dto;
using PostMessengerService.Domain.Models;
using PostMessengerService.Infrastructure.Repositories;

namespace PostMessengerService.Application.Services
{
    public class LikeService
    {
        private readonly IMapper _mapper;
        private readonly UnitOfWork _unitOfWork;

        public LikeService(UnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
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
            likeToMap.Username = "Passion";
            likeToMap.PostId = postId;
            var likeToValidate =
                await _unitOfWork.LikeRepository.GetEntityByUsernameAndPostIdAsync(likeToMap.Username, likeToMap.PostId);

            if (likeToValidate == null)
            {
                var likeMapped = _mapper.Map<LikeModel>(likeToMap);
                likeMapped.Username = "Passion";
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
            var username = "Passion";
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
}
