using Application.UseCases.AccountUseCase.Commands.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Application.UseCases.AccountUseCase.Commands.Requests
{
    public class SignInRequest : IRequest<SignInResponse>
    {
        [Required, EmailAddress]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
