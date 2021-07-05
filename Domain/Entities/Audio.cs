using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Audio : Entity<Audio>
    {
        public string Descricao { get; set; }
        public string Falante { get; set; }
    }
}
