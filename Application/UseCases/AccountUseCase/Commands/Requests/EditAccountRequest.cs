using Application.UseCases.AccountUseCase.Commands.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Application.UseCases.AccountUseCase.Commands.Requests
{
    public class EditAccountRequest : IRequest<EditAccountResponse>
    {
        public Guid CurrentUserId { get; set; }

        [Required]
        public Guid Id { get; set; }

        [Required]
        public bool isAdmin { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [EmailAddress, Required]
        public string Email { get; set; }

        [Required]
        public string Registration { get; set; }

        [Required]
        public string BirthDate { get; set; }

        public string OldPassword { get; set; }

        [Compare("ConfirmNewPassword")]
        public string NewPassword { get; set; }
        public string ConfirmNewPassword { get; set; }
    }
}
