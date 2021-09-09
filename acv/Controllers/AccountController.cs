using Application.UseCases.AccountUseCase.Commands.Requests;
using Domain.Entities.Identity;
using Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [ApiController, Route("api/[controller]")]
    [Authorize(Roles = "Admin,User")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountRepository _accountRepository;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(
            IAccountRepository accountRepository,
            UserManager<AppUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _accountRepository = accountRepository;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult GetAll()
        {
            var userList = _accountRepository.GetAll().Select(user => new
            {
                Id = Guid.Parse(user.Id),
                Name = user.FirstName,
                user.UserName,
                user.Approved
            });

            return Ok(userList);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var user = await _accountRepository.GetOne(id);

            var roles = await _accountRepository.GetRolesAsync(user);

            return Ok(new
            {
                user.Id,
                Name = user.FirstName + " " + user.LastName,
                user.UserName,
                user.BirthDate,
                user.Email,
                user.Registration,
                user.Approved,
                roles                
            });
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> SignIn(
            [FromServices] IMediator mediator,
            [FromBody] SignInRequest command)
        {
            var result = await mediator.Send(command);

            if (!result.Success)
                return BadRequest(result);

            return Ok(new
            {
                result.Id,
                result.Name,
                result.UserName,
                result.Email,
                result.ExpireDate,
                result.Token
            });
        }

        [HttpPost("signup")]
        [AllowAnonymous]
        public async Task<IActionResult> SignUp(
            [FromServices] IMediator mediator,
            [FromBody] SignUpRequest command)
        {
            var result = await mediator.Send(command);

            if (!result.Success)
                return Unauthorized();
            
            return Ok(result);
        }

        [HttpPut("EditAccount")]
        public async Task<IActionResult> Edit(
            [FromServices] IMediator mediator,
            [FromBody] EditAccountRequest command)
        {
            command.CurrentUserId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            command.isAdmin = User.IsInRole("Admin");

            var result = await mediator.Send(command);

            if (!result.Success)
                return Unauthorized();

            return Ok(result);
        }

        [HttpGet("roles")]
        [Authorize(Roles = "Admin")]
        public IActionResult GetRoles()
        {
            return Ok(_roleManager.Roles.Select(x => new
            {
                x.Id,
                x.Name
            }));
        }

        [HttpPost("roles")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateRole(
            [FromServices] IMediator mediator,
            [FromBody] CreateRoleRequest command)
        {
            var result = await mediator.Send(command);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpPost("permission")]
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllPermission(Guid id)
        {
            var user = await _accountRepository.GetOne(id);

            var roles = await _accountRepository.GetRolesAsync(user);

            return Ok(new
            {
                user.UserName,
                roles
            });
        }

        [HttpGet("unconfirmed")]
        [Authorize(Roles = "Admin")]
        public IActionResult GetUnconfirmed()
        {
            var userList = _accountRepository.GetAll().Select(user => new
            {
                Id = Guid.Parse(user.Id),
                Name = user.FirstName + " " + user.LastName,
                user.UserName,
                user.Approved
            }).Where(user => user.Approved == false);

            return Ok(userList);
        }
        
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(Guid id)
        {

            return Ok("Nao implementado. Id = " + id);
        }
    }
}
