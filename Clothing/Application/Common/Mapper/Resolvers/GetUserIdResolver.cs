using AutoMapper;
using Clothing.Application.DTOs.AuthenticationDTO;
using Clothing.Application.Pipeline.Users.Query.GetUserByUserName;
using Clothing.Domain.Entities;
using MediatR;

namespace Clothing.Application.Common.Mapper.Resolvers
{
    public class GetUserIdResolver : IValueResolver<AuthenticationDto, UserLoginHistory, int>
    {
        private readonly IMediator _mediator;
        public GetUserIdResolver(IMediator mediator)
        {
            this._mediator = mediator;
        }

        public int Resolve(AuthenticationDto source, UserLoginHistory destination, int destMember, ResolutionContext context)
        {
            var user = _mediator.Send(new GetUserByUserNameQuery(source.UserName)).Result;

            return user.Id;
        }
    }
}
