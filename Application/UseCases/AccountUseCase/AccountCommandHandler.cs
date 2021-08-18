using Application.UseCases.AccountUseCase.Commands.Requests;
using Application.UseCases.AccountUseCase.Commands.Responses;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace Application.UseCases.AccountUseCase
{
    public class AccountCommandHandler : 
        IRequestHandler<SignUpRequest, SignUpResponse>, 
        IRequestHandler<SignInRequest, SignInResponse>,
        IRequestHandler<AddPermissionRequest, AddPermissionResponse>
    {
        private readonly IAccountRepository _accountRepository;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountCommandHandler(IAccountRepository accountRepository, RoleManager<IdentityRole> roleManager)
        {
            _accountRepository = accountRepository;
            _roleManager = roleManager;
        }

        public async Task<SignUpResponse> Handle(SignUpRequest request, CancellationToken cancellationToken)
        {
            var user = new AppUser
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                UserName = request.Email
            };

            var result = await _accountRepository.SignUpAsync(user, request.Password);

            if (!result.Succeeded)
                return new SignUpResponse { Success = false };

            return new SignUpResponse
            {
                Success = true,
                Name = request.FirstName + " " + request.LastName,
                UserName = request.Email,
                Password = request.Password,
                Date = DateTime.Now
            };
        }

        public async Task<SignInResponse> Handle(SignInRequest request, CancellationToken cancellationToken)
        {
            var user = await _accountRepository.GetOne(request.UserName);

            var result = await _accountRepository.LoginAsync(user, request.Password);

            if (string.IsNullOrEmpty(result) || user == null)
                return new SignInResponse { Success = false };

            var roles = await _accountRepository.GetRolesAsync(user);

            return new SignInResponse
            {
                Success = true,
                Id = Guid.Parse(user.Id),
                Name = user.FirstName + " " + user.LastName,
                UserName = user.UserName,
                Email = user.Email,
                Roles = roles,
                Token = result
            };
        }

        public async Task<AddPermissionResponse> Handle(AddPermissionRequest request, CancellationToken cancellationToken)
        {
            var user = await _accountRepository.GetOne(request.Id);

            if (user == null)
                return new AddPermissionResponse { Success = false };

            var result = await _accountRepository.AddToRoleAsync(user, request.Role);

            if (result.Succeeded)
                return new AddPermissionResponse
                {
                    Success = true,
                    Id = request.Id,
                    Role = request.Role
                };
            else
                return new AddPermissionResponse { Success = false };
        }
    }
}
