using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class Section : Entity<Section>
    {
        public string Text { get; set; }
        public double Duration { get; set; }
        public double Start { get; set; }

        public Guid AudioId { get; set; }
        public Audio Audio { get; set; }

        public Guid SpeakerId { get; set; }
        public Speaker Speaker { get; set; }
    }
}
