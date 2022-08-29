using AutoMapper;
using PostMessengerService.Domain.Dto;
using PostMessengerService.Domain.Models;

namespace PostMessengerService.Application.Middlewares;

public class PostCreationProfile : Profile
{
    public PostCreationProfile()
    {
        CreateMap<PostCreationDto, PostModel>();
    }
}

public class PostInformationProfile : Profile
{
    public PostInformationProfile()
    {
        CreateMap<PostModel, PostInformationDto>();
    }
}

public class PostChangeProfile : Profile
{
    public PostChangeProfile()
    {
        CreateMap<PostChangeDto, PostModel>();
    }
}

public class CommentInformationProfile : Profile
{
    public CommentInformationProfile()
    {
        CreateMap<CommentModel, CommentInformationDto>();
    }
}

public class CommentCreateProfile : Profile
{
    public CommentCreateProfile()
    {
        CreateMap<CommentCreationDto, CommentModel>();
    }
}

public class CommentChangeProfile : Profile
{
    public CommentChangeProfile()
    {
        CreateMap<CommentChangeDto, CommentModel>();
    }
}

public class LikeInformationProfile : Profile
{
    public LikeInformationProfile()
    {
        CreateMap<LikeModel, LikeInformationDto>();
    }
}

public class LikeCreateProfile : Profile
{
    public LikeCreateProfile()
    {
        CreateMap<LikeCreationDto, LikeModel>();
    }
}