using Application.UseCases.AccountUseCase.Commands.Responses;
using MediatR;
using System;
using System.ComponentModel.DataAnnotations;

namespace Application.UseCases.AccountUseCase.Commands.Requests
{
    public class SignUpRequest : IRequest<SignUpResponse>
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Registration { get; set; }

        [Required]
        public string BirthDate { get; set; }

        [Required, Compare("ConfirmPassword")]
        public string Password { get; set; }
        
        [Required]
        public string ConfirmPassword { get; set; }
    }
}
