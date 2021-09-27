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

        /// <summary>
        /// Gets basic information of all users
        /// </summary>
        /// <returns>A list of users</returns>
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

        /// <summary>
        /// Gets more information of one specific user
        /// </summary>
        /// <param name="id">User's Id</param>
        /// <returns>One user</returns>
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

        /// <summary>
        /// Log in to API
        /// </summary>
        /// <param name="mediator">Not a frontend's concern</param>
        /// <param name="command">User's email and password</param>
        /// <returns>Some user's data and token</returns>
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

        /// <summary>
        /// Sign up to API
        /// </summary>
        /// <param name="mediator">Not a frontend's concern</param>
        /// <param name="command">User's Firstname, LastName, Email, Registration, BirthDate, Password and ConfirmPassword</param>
        /// <returns>Some user data and a list of errors</returns>
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

        /// <summary>
        /// Edits user's informations. Only an Admin can edit user's birthdate and registration.
        /// </summary>
        /// <param name="mediator"></param>
        /// <param name="command">Any user data</param>
        /// <returns></returns>
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

        /// <summary>
        /// Gets all the roles available
        /// </summary>
        /// <returns>A list of roles</returns>
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

        /// <summary>
        /// Posts a new role
        /// </summary>
        /// <param name="mediator"></param>
        /// <param name="command"></param>
        /// <returns>The role name</returns>
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

        /// <summary>
        /// Associates a user to a role
        /// </summary>
        /// <param name="mediator"></param>
        /// <param name="command">User Id and role name</param>
        /// <returns></returns>
        [HttpPost("Permission")]
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

        /// <summary>
        /// Gets a user's roles by his Id
        /// </summary>
        /// <param name="id">User's Id</param>
        /// <returns>User name and his roles</returns>
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

        /// <summary>
        /// Gets all users who weren't approved by an Admin
        /// </summary>
        /// <returns>A list of users</returns>
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
        
        /// <summary>
        /// Not implemented
        /// </summary>
        /// <param name="id">User's Id</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(Guid id)
        {
            return Ok("Nao implementado. Id = " + id);
        }
    }
}
