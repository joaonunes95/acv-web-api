using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Audio : Entity<Audio>
    {
        public string Name { get; set; }
        public double Duration { get; set; }
        public DateTime Date { get; set; }
        public double Reliability { get; set; }
        public string FilePath { get; set; }
        public ICollection<Section> Sections { get; set; }
        public Guid ChannelId { get; set; }
        public Channel Channel { get; set; }
    }
}