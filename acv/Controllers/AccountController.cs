using Application.UseCases.AccountUseCase.Commands.Requests;
using Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [ApiController, Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountRepository _accountRespository;

        public AccountController(IAccountRepository accountRespository)
        {
            _accountRespository = accountRespository;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var userList = _accountRespository.GetAll().Select(user => new
            {
                Id = Guid.Parse(user.Id),
                Name = user.FirstName + " " + user.LastName,
                user.UserName
            });

            return Ok(userList);
        }

        [Authorize]
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var user = await _accountRespository.GetOne(id);

            var claims = await _accountRespository.GetClaimsAsync(user);

            return Ok(new
            {
                user.Id,
                Name = user.FirstName + " " + user.LastName,
                user.UserName,
                claims
            });
        }

        [HttpPost("login")]
        public async Task<IActionResult> SignIn(
            [FromServices] IMediator mediator,
            [FromBody] SignInRequest command)
        {
            var result = await mediator.Send(command);

            if (!result.Success)
                return Unauthorized();

            return Ok(new
            {
                result.Id,
                result.Name,
                result.UserName,
                result.Email,
                result.Claims,
                result.Token
            });
        }

        [HttpPost("signup")]
        public async Task<IActionResult> SignUp(
            [FromServices] IMediator mediator,
            [FromBody] SignUpRequest command)
        {
            var result = await mediator.Send(command);

            if (!result.Success)
                return Unauthorized();
            
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {

            return Ok(id);
        }
    }
}
