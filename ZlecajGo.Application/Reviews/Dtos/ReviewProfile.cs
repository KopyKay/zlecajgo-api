using AutoMapper;
using ZlecajGo.Application.Reviews.Commands.CreateReview;
using ZlecajGo.Application.Reviews.Commands.UpdateReview;
using ZlecajGo.Domain.Entities;

namespace ZlecajGo.Application.Reviews.Dtos;

public class ReviewProfile : Profile
{
    public ReviewProfile()
    {
        CreateMap<Review, ReviewDto>();
        CreateMap<CreateReviewCommand, Review>();
        CreateMap<UpdateReviewCommand, Review>();
    }
}