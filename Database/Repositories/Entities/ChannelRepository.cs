using Database.Context;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Database.Repositories.Entities
{
    public class ChannelRepository : Repository<Channel>, IChannelRepository
    {
        public ChannelRepository(AcvContext context)
            : base(context) { }

        public async Task<Channel> GetByCodeLocal(int code)
        {
            var channel = CurrentSet.Local.FirstOrDefault(x => x.ChannelCode == code);

            if(channel is null)
                channel = await CurrentSet.FirstOrDefaultAsync(x => x.ChannelCode == code);

            return channel;
        }
    }
}
