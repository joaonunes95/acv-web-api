using System;

namespace Application.UseCases.AccountUseCase.Commands.Responses
{
    public class AddPermissionResponse
    {
        public bool Success { get; set; }
        public Guid Id { get; set; }
        public string Role { get; set; }
    }
}
