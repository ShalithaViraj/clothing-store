using Clothing.Application.Pipeline.Authentication.Login;
using Clothing.Application.Pipeline.Authentication.Registration;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Clothing.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthenticationController(IMediator mediator)
        {
            this._mediator = mediator;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginCommand authenticationCommand)
        {
            var response = await _mediator.Send(authenticationCommand);

            return Ok(response);
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegistrationCommand registrationCommand)
        {
            var response = await _mediator.Send(registrationCommand);

            return Ok(response);
        }
    }
}
