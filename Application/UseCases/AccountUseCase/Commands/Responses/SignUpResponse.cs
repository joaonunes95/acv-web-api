using System;
using System.Collections.Generic;

namespace Application.UseCases.AccountUseCase.Commands.Responses
{
    public class SignUpResponse
    {
        public bool Success { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public DateTime Date { get; set; }
        public IEnumerable<string> Errors { get; set; }
    }
}