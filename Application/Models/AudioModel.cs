using System;
using System.Collections.Generic;

namespace Application.Models
{
    public class AudioModel
    {
        public Guid Id;
        public double Duration;
        public DateTime Date { get; set; }
        public double Reliability { get; set; }
        public ICollection<SectionModel> Sections { get; set; }
        public ChannelModel Channel { get; set; }
    }
}
