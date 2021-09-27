using Application.UseCases.AccountUseCase.Commands.Requests;
using Application.UseCases.AccountUseCase.Commands.Responses;
using Domain.Entities.Identity;
using Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.UseCases.AccountUseCase
{
    public class AccountCommandHandler : 
        IRequestHandler<SignUpRequest, SignUpResponse>, 
        IRequestHandler<SignInRequest, SignInResponse>,
        IRequestHandler<AddPermissionRequest, AddPermissionResponse>,
        IRequestHandler<CreateRoleRequest, CreateRoleResponse>,
        IRequestHandler<EditAccountRequest, EditAccountResponse>
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IConfiguration _configuration;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountCommandHandler(
            IAccountRepository accountRepository,
            IConfiguration configuration,
            UserManager<AppUser> userManager,
            RoleManager<IdentityRole> roleManager
            )
        {
            _accountRepository = accountRepository;
            _configuration = configuration;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<SignUpResponse> Handle(SignUpRequest request, CancellationToken cancellationToken)
        {
            if (request == null)
                throw new NullReferenceException("Sign up request is null");

            CultureInfo provider = CultureInfo.InvariantCulture;

            var user = new AppUser
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                UserName = request.Email,
                Registration = request.Registration,
                BirthDate = DateTime.ParseExact(request.BirthDate, "ddMMyyyy", provider)
            };

            var result = await _accountRepository.SignUpAsync(user, request.Password);

            if (!result.Succeeded)
            {
                return new SignUpResponse
                {
                    Success = false,
                    Errors = result.Errors.Select(e => e.Description)
                };
            }

            return new SignUpResponse
            {
                Success = true,
                Name = request.FirstName + " " + request.LastName,
                UserName = request.Email,
                Date = DateTime.Now
            };
        }

        public async Task<SignInResponse> Handle(SignInRequest request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);

            if(user == null || !await _userManager.CheckPasswordAsync(user, request.Password))
                return new SignInResponse
                {
                    Success = false
                };

            var claims = new List<Claim>
            {
                new Claim("Email", request.Email),
                new Claim(ClaimTypes.NameIdentifier, user.Id)
            };

            var role = await _userManager.GetRolesAsync(user);
            IdentityOptions _options = new IdentityOptions();

            if (role.Any())
                claims.Add(new Claim(_options.ClaimsIdentity.RoleClaimType, role.FirstOrDefault()));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["AuthSettings:Key"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["AuthSettings:Issuer"],
                audience: _configuration["AuthSettings:Audience"],
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256));

            string tokenAsString = new JwtSecurityTokenHandler().WriteToken(token);

            return new SignInResponse
            {
                Success = true,
                Id = Guid.Parse(user.Id),
                Name = user.FirstName + " " + user.LastName,
                UserName = user.UserName,
                Email = user.Email,
                ExpireDate = token.ValidTo,
                Token = tokenAsString,
            };
        }

        public async Task<AddPermissionResponse> Handle(AddPermissionRequest request, CancellationToken cancellationToken)
        {
            var user = await _accountRepository.GetOne(request.Id);

            if (user == null)
                return new AddPermissionResponse { Success = false };

            var result = await _accountRepository.AddToRoleAsync(user, request.Role);

            if (result.Succeeded)
            {
                user.Approved = true;
                await _accountRepository.UpdateUser(user);

                return new AddPermissionResponse
                {
                    Success = true,
                    Id = user.Id,
                    Name = user.FirstName,
                    Role = request.Role
                };
            }
            else
                return new AddPermissionResponse { Success = false };
        }

        public async Task<CreateRoleResponse> Handle(CreateRoleRequest request, CancellationToken cancellationToken)
        {
            var roleExists = await _roleManager.RoleExistsAsync(request.Role);
            if (roleExists)
                return new CreateRoleResponse
                {
                    Success = false,
                    Errors = new[] { $"The role \"{request.Role}\" already exists" }
                };

            IdentityRole identityRole = new IdentityRole
            {
                Name = request.Role
            };

            IdentityResult result = await _roleManager.CreateAsync(identityRole);

            if (!result.Succeeded)
                return new CreateRoleResponse { 
                    Success = false,
                    Errors = result.Errors.Select(x=>x.Description)
                };

            return new CreateRoleResponse
            {
                Success = true
            };
        }

        public async Task<EditAccountResponse> Handle(EditAccountRequest request, CancellationToken cancellationToken)
        {
            if (request == null)
                throw new NullReferenceException("Sign up request is null");

            var user = await _accountRepository.GetOne(request.Id);

            if (user == null)
                return new EditAccountResponse
                {
                    Success = false,
                    Errors = new[] { "User not found" }
                };

            if (request.isAdmin)
            {
                CultureInfo provider = CultureInfo.InvariantCulture;

                user.FirstName = request.FirstName;
                user.LastName = request.LastName;
                user.Email = request.Email;
                user.UserName = request.Email;
                user.BirthDate = DateTime.ParseExact(request.BirthDate, "ddMMyyyy", provider);
                user.Registration = request.Registration;

                if (request.NewPassword != null)
                {
                    var ChangePWResult = await _userManager.ChangePasswordAsync(user, request.OldPassword, request.NewPassword);

                    if(!ChangePWResult.Succeeded)
                        return new EditAccountResponse
                        {
                            Success = false,
                            Errors = ChangePWResult.Errors.Select(e => e.Description)
                        };
                }
            }
            else
            {
                if(request.CurrentUserId == Guid.Parse(user.Id))
                {
                    user.FirstName = request.FirstName;
                    user.LastName = request.LastName;
                    user.Email = request.Email;
                    user.UserName = request.Email;

                    if (request.NewPassword != null && request.NewPassword == request.ConfirmNewPassword)
                    {
                        var ChangePWResult = await _userManager.ChangePasswordAsync(user, request.OldPassword, request.NewPassword);

                        if (!ChangePWResult.Succeeded)
                            return new EditAccountResponse
                            {
                                Success = false,
                                Errors = ChangePWResult.Errors.Select(e => e.Description)
                            };
                    }
                }
                else
                {
                    return new EditAccountResponse
                    {
                        Success = false,
                        Errors = new[] { "Forbidden" }
                    };
                }
            }

            var result = await _accountRepository.UpdateUser(user);

            if (!result.Succeeded)
            {
                return new EditAccountResponse
                {
                    Success = false,
                    Errors = result.Errors.Select(e => e.Description)
                };
            }

            return new EditAccountResponse
            {
                Success = true,
                Name = request.FirstName + " " + request.LastName,
                UserName = request.Email,
                Date = DateTime.Now
            };
        }
    }
}
