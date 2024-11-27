using AutoMapper;
using Clothing.Application.Common.Interface;
using Clothing.Application.DTOs.AuthenticationDTO;
using Clothing.Application.Pipeline.Users.Command;
using Clothing.Application.Pipeline.Users.Query.GetUserByUserName;
using Clothing.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Clothing.Infrastructure.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;

        public AuthenticationService(IMediator mediator, IMapper mapper, IConfiguration config)
        {
            _mediator = mediator;
            _mapper = mapper;
            _config = config;
        }

        public async Task<LoginResponseDto> Login(AuthenticationDto request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _mediator.Send(new GetUserByUserNameQuery(request.UserName));

                if (user == null) 
                {
                    return LoginResponseDto.NotSuccess("User Does Not Exsists On The System.");
                }

                if (!BCrypt.Net.BCrypt.Verify(request.Password, user.Password))
                {
                    return LoginResponseDto.NotSuccess("PassWord Incorrect.");
                }

                var userAthunticationResponse = await ConfigureJwtToken
                                                (
                                                    user,
                                                    cancellationToken
                                                 );

                if (userAthunticationResponse.IsLoginSuccess)
                {

                    var userLoginHistory = _mapper.Map<UserLoginHistory>(request);

                    await _mediator.Send(new CreateUserLoginHistoryCommand()
                    {
                        UserLoginHistory = userLoginHistory,
                    });

                    return userAthunticationResponse;
                }
                else
                {
                    return LoginResponseDto.NotSuccess("Error has been occurred please try again");
                }

            }
            catch (Exception ex) 
            {
                throw ex;
            }
        }

        private async Task<LoginResponseDto> ConfigureJwtToken(User user, CancellationToken cancellationToken)
        {
            var key = _config["Tokens:Key"];
            var issuer = _config["Tokens:Issuer"];


            var now = DateTime.UtcNow;
            DateTime nowDate = DateTime.UtcNow;
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                        new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                        new Claim("firstName",string.IsNullOrEmpty(user.FirstName)? "": user.FirstName),
                        new Claim("lastName", string.IsNullOrEmpty(user.LastName) ? "" : user.LastName),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            var token = new JwtSecurityToken
            (
                issuer: issuer,
                claims: claims,
                expires: nowDate.AddHours(8),
                signingCredentials: credentials
            );

            var tokenString = new JwtSecurityTokenHandler()
                            .WriteToken(token);


            var routeURL = "./home";



            return LoginResponseDto.Success
            (
                tokenString,
                user.UserName,
                user.Id,
                routeURL
            );
        }
    }
}
