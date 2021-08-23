using Domain.Entities;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IChannelRepository : IRepository<Channel>
    {
        Task<Channel> GetByCodeLocal(int code);
    }
}
