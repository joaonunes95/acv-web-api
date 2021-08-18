using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace Application.UseCases.AccountUseCase.Commands.Responses
{
    public class SignInResponse
    {
        public bool Success { get; set; }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public IList<string> Roles { get; set; }
        public string Token { get; set; }
    }
}
