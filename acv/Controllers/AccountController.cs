using Application.UseCases.AccountUseCase.Commands.Requests;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
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
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(
            IAccountRepository accountRespository,
            UserManager<AppUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _accountRespository = accountRespository;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var userList = _accountRespository.GetAll().Select(user => new
            {
                Id = Guid.Parse(user.Id),
                Name = user.FirstName + " " + user.LastName,
                user.UserName,
                user.Approved
            });

            return Ok(userList);
        }

        [Authorize]
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var user = await _accountRespository.GetOne(id);

            var roles = await _accountRespository.GetRolesAsync(user);

            return Ok(new
            {
                user.Id,
                Name = user.FirstName + " " + user.LastName,
                user.UserName,
                user.Approved,
                roles                
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
                result.Roles,
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

            return Ok("Nao implementado. Id = " + id);
        }

        [HttpPost("permission")]
        public async Task<IActionResult> AddPermission(
            [FromServices] IMediator mediator,
            [FromBody] AddPermissionRequest command)
        {
            var result = await mediator.Send(command);

            if (!result.Success)
                return BadRequest(result);
            
            return Ok(result);
        }

        [HttpGet("permission/{id:guid}")]
        public async Task<IActionResult> GetAllPermission(Guid id)
        {
            var user = await _accountRespository.GetOne(id);

            var roles = await _accountRespository.GetRolesAsync(user);

            return Ok(new
            {
                user.UserName,
                roles
            });
        }

        [HttpGet("unconfirmed")]
        public IActionResult GetUnconfirmed()
        {
            var userList = _accountRespository.GetAll().Select(user => new
            {
                Id = Guid.Parse(user.Id),
                Name = user.FirstName + " " + user.LastName,
                user.UserName,
                user.Approved
            }).Where(user => user.Approved == false);

            return Ok(userList);
        }

        //[Authorize(Policy = "AdminAccess")]
        [HttpPut("confirm/{id:guid}")]
        //[Authorize(Roles = "Admin,Manager,User")]
        public async Task<IActionResult> Confirm(Guid id)
        {
            var user = await _accountRespository.GetOne(id);

            if (user.Approved)
                return Ok("Já tinha dado bom. Id: " + user.FirstName);

            user.Approved = true;

            await _accountRespository.UpdateUser(user);

            return Ok("Deu bom só agora. Id: " + user.FirstName);
        }
    }
}
