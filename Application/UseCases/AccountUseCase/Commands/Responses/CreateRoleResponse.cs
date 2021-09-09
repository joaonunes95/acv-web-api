using System;
using System.Collections.Generic;
using System.Text;

namespace Application.UseCases.AccountUseCase.Commands.Responses
{
    public class CreateRoleResponse
    {
        public bool Success { get; set; }
        public string Role { get; set; }
        public IEnumerable<string> Errors { get; set; }
    }
}
