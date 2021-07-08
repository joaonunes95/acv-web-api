using System;

namespace Application.UseCases.AccountUseCase.Commands.Responses
{
    public class SignUpResponse
    {
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; } // ambiente de dev acho que vale a pena
        public DateTime Date { get; set; }
    }
}