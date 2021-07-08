using Application.UseCases.AccountUseCase.Commands.Requests;
using Application.UseCases.AccountUseCase.Commands.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        public AccountController()
        {
        }

        [HttpPost("signup")]
        public Task<SignUpResponse> SignUp(
            [FromServices] IMediator mediator,
            [FromBody] SignUpRequest command)
        {
            var result = mediator.Send(command);

            // if result is not valid then return error message
            // validar os dados na handler, segundo o balta.io

            return result;
        }
    }
}
