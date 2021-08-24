using System.Collections.Generic;

namespace Domain.Entities
{
    public class Channel : Entity<Channel>
    {
        public string Name { get; set; }
        public int ChannelCode { get; set; }

        public ICollection<Audio> Audio { get; set; }
    }
}
