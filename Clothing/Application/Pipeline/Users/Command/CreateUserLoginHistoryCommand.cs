using Clothing.Domain.Entities;
using Clothing.Domain.Repository.Command;
using MediatR;

namespace Clothing.Application.Pipeline.Users.Command
{
    public class CreateUserLoginHistoryCommand : IRequest<UserLoginHistory>
    {
        public UserLoginHistory UserLoginHistory { get; set; }
    }

    public class CreateUserLoginHistoryCommandHandler : IRequestHandler<CreateUserLoginHistoryCommand, UserLoginHistory>
    {
        private readonly IUserHistoryCommandRepository _userHistoryCommandRepository;

        public CreateUserLoginHistoryCommandHandler(IUserHistoryCommandRepository userHistoryCommandRepository)
        {
            _userHistoryCommandRepository = userHistoryCommandRepository;
        }

        public async Task<UserLoginHistory> Handle(CreateUserLoginHistoryCommand request, CancellationToken cancellationToken)
        {
            var userLoginHistory = await _userHistoryCommandRepository
                .AddAsync(request.UserLoginHistory, cancellationToken);

            return userLoginHistory;
        }
    }
}
