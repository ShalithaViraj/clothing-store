using Clothing.Application.DTOs.AuthenticationDTO;

namespace Clothing.Application.Common.Interface
{
    public interface IAuthenticationService
    {
        Task<LoginResponseDto> Login(AuthenticationDto request, CancellationToken cancellationToken);
    }
}
