using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Application
{
    // Permite recuperar o assembly do projeto Application para o Startup informar ao MediatR
    public static class AssemblyReference
    {
        public static readonly Assembly Value = typeof(AssemblyReference).Assembly;
    }
}
