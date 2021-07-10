using Application.UseCases.AccountUseCase.Commands.Requests;
using Application.UseCases.AccountUseCase.Commands.Responses;
using Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Presentation.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
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

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(string id)
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
