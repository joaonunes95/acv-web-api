using Application.UseCases.AccountUseCase.Commands.Requests;
using Application.UseCases.AccountUseCase.Commands.Responses;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.UseCases.AccountUseCase
{
    public class AccountCommandHandler : IRequestHandler<SignUpRequest, SignUpResponse>
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

            await _accountRepository.SignUpAsync(user, request.Password);

            var result = new SignUpResponse
            {
                Name = request.FirstName + " " + request.LastName,
                UserName = request.Email,
                Password = request.Password,
                Date = DateTime.Now
            };

            return result;
        }
    }
}
