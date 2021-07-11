using System;

namespace Application.UseCases.AccountUseCase.Commands.Responses
{
    public class SignUpResponse
    {
        public bool Success { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public DateTime Date { get; set; }
    }
}