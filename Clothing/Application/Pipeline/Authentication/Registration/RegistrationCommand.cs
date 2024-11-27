using BCrypt.Net;
using Clothing.Application.DTOs.Common;
using Clothing.Domain.Entities;
using Clothing.Domain.Repository.Command;
using Clothing.Domain.Repository.Query;
using MediatR;

namespace Clothing.Application.Pipeline.Authentication.Registration
{
    public class RegistrationCommand: IRequest<ResultDto>
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public int Phone { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        public string Address01 { get; set; }
        public string Address02 { get; set; }
    }

    public class RegistrationCommandHandler : IRequestHandler<RegistrationCommand, ResultDto>
    {
        private readonly IUserQueryRepository _userQueryRepository;
        private readonly IUserCommandRepository _userCommandRepository;

        public RegistrationCommandHandler(IUserQueryRepository userQueryRepository, IUserCommandRepository userCommandRepository)
        {
            _userQueryRepository = userQueryRepository;
            _userCommandRepository = userCommandRepository;
        }

        public async Task<ResultDto> Handle(RegistrationCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var user = (await _userQueryRepository.Query(x => x.Email == request.Email)).FirstOrDefault();

                if (user == null)
                {
                    var newUser = new User()
                    {
                        Email = request.Email,
                        Password = BCrypt.Net.BCrypt.HashPassword(request.Password),
                        PhoneNo = request.Phone,
                        UserName = request.Email,
                        FirstName = request.FirstName,
                        LastName = request.LastName,
                        Position = Domain.Enum.Position.Admin,
                        CreateDate = DateTime.Now,
                        UpdateDate = DateTime.Now,
                        IsActive = true
                    };

                    await _userCommandRepository.AddAsync(newUser, cancellationToken);

                    return ResultDto.Success("New user has been added successfully.");
                }
                else
                {
                    return ResultDto.Failure(new List<string>()
                        {
                          "User name already exists. Try another user name."
                        });
                }
            }
            catch (Exception ex)
            {
                return ResultDto.Failure(new List<string>()
                        {
                          "Registraion Process Failed. Please Contact Admin."
                        });
            }
        }
    }
}
