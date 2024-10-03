using Clothing.Domain.Entities;
using Clothing.Domain.Repository.Query;
using MediatR;

namespace Clothing.Application.Pipeline.Users.Query.GetUserByUserName
{
    public record GetUserByUserNameQuery(string UserName) : IRequest<User>;


    public class GetUserByUserNameQueryHandler : IRequestHandler<GetUserByUserNameQuery, User>
    {
        private readonly IUserQueryRepository _userQueryRepository;
        public GetUserByUserNameQueryHandler(IUserQueryRepository userQueryRepository)
        {
            _userQueryRepository = userQueryRepository;
        }

        public async Task<User> Handle(GetUserByUserNameQuery request, CancellationToken cancellationToken)
        {
            var selectedUser = _userQueryRepository.GetUserByUserName(request.UserName);

            return selectedUser;
        }
    }
}
