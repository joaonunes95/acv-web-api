using Domain.Entities;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface ISpeakerRepository : IRepository<Speaker>
    {
        Task<Speaker> GetByNameLocal(string name);
    }
}
