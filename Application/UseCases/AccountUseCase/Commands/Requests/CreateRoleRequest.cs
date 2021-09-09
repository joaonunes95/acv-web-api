using Application.UseCases.AccountUseCase.Commands.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Application.UseCases.AccountUseCase.Commands.Requests
{
    public class CreateRoleRequest : IRequest<CreateRoleResponse>
    {
        [Required]
        public string Role { get; set; }
    }
}
