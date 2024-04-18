using ApplicationCore.Models;
using AutoMapper;
using Infrastructure.Entities;

namespace Infrastructure.Mappers;

public class QuizProfile : Profile
{
    public QuizProfile()
    {
        CreateMap<Quiz, QuizEntity>().ReverseMap();
        CreateMap<QuizItem, QuizItemEntity>().ReverseMap();
        CreateMap<QuizItemAnswer, QuizItemAnswerEntity>().ReverseMap();
        CreateMap<QuizItemUserAnswer, QuizItemUserAnswerEntity>().ReverseMap()
            .ForMember(dest => dest.Answer, src => src.MapFrom(opt => opt.UserAnswer));
        CreateMap<User, UserEntity>().ReverseMap();
    }
}