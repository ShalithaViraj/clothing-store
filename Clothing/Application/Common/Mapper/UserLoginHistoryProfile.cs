using AutoMapper;
using Clothing.Application.Common.Mapper.Resolvers;
using Clothing.Application.DTOs.AuthenticationDTO;
using Clothing.Domain.Entities;


namespace Clothing.Application.Common.Mapper
{
    public class UserLoginHistoryProfile : Profile
    {
        public UserLoginHistoryProfile()
        {
            CreateMap<AuthenticationDto, UserLoginHistory>()
                .ForMember(s => s.UserId, o => o.MapFrom<GetUserIdResolver>())
                .ForMember(s => s.LoggedInTime, o => o.MapFrom(x => DateTime.Now))
                .ForMember(s => s.CreatedDate, o => o.MapFrom(x => DateTime.Now))
                .ForMember(s => s.CreatedByUserId, o => o.MapFrom<GetUserIdResolver>());



        }
        
    }
}
