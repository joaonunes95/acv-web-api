using Application.UseCases.AccountUseCase.Commands.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Application.UseCases.AccountUseCase.Commands.Requests
{
    public class AddPermissionRequest : IRequest<AddPermissionResponse>
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public string Role { get; set; }
    }
}
