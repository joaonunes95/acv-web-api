using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class Speaker : Entity<Speaker>
    {        
        public string Name { get; set; }
        public string Local { get; set; }

        public ICollection<Section> Sections { get; set; }
    }
}
