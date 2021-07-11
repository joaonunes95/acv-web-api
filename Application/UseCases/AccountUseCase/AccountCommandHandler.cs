using Application.UseCases.AccountUseCase.Commands.Requests;
using Application.UseCases.AccountUseCase.Commands.Responses;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace Application.UseCases.AccountUseCase
{
    public class AccountCommandHandler : IRequestHandler<SignUpRequest, SignUpResponse>, IRequestHandler<SignInRequest, SignInResponse>
    {
        private readonly IAccountRepository _accountRepository;

        public AccountCommandHandler(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
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
            var result = await _accountRepository.LoginAsync(
                new AppUser { UserName = request.UserName }, 
                request.Password);

            if (string.IsNullOrEmpty(result))
                return new SignInResponse
                {
                    Success = false
                };

            var user = await _accountRepository.GetOne(request.UserName);

            var claims = await _accountRepository.GetClaimsAsync(user);

            return new SignInResponse
            {
                Success = true,
                Id = Guid.Parse(user.Id),
                Name = user.FirstName + " " + user.LastName,
                UserName = user.UserName,
                Email = user.Email,
                Claims = claims,
                Token = result
            };
        }
    }
}
