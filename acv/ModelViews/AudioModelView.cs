using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Presentation.Models
{
    public class AudioModelView
    {
        public Guid? AudioId { get; set; }
        public DateTime Date { get; set; }
        public int ChannelCode { get; set; }
        public string Speaker { get; set; }
        public double Duration { get; set; } // precisa?
        public string Text { get; set; } // problema
    }
}
