using AutoMapper;
using Clothing.Application.Common.Interface;
using Clothing.Application.DTOs.AuthenticationDTO;
using MediatR;

namespace Clothing.Application.Pipeline.Authentication.Login
{
    public class LoginCommand : IRequest<LoginResponseDto>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginResponseDto>
    {
        private readonly IMapper _mapper;
        private readonly IAuthenticationService _authenticationService;

        public LoginCommandHandler(IMapper mapper, IAuthenticationService authenticationService) 
        {
            _mapper = mapper;
            _authenticationService = authenticationService;
        }

        public async Task<LoginResponseDto> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var authenticationDto = new AuthenticationDto()
            {
                UserName = request.UserName,
                Password = request.Password
            };

            if (authenticationDto == null)
            {
                throw new ApplicationException("There is a problem in mapping.");
            }

            var response = await _authenticationService.Login(authenticationDto, cancellationToken);

            return response;
        }
    }
}
