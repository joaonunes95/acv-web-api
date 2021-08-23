using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IAudioRepository : IRepository<Audio>
    {
        Task<Audio> GetByName(string name);
        Task<Audio> GetByNameLocal(string name);
        void AddSection(Section section);
        Task<IEnumerable<Audio>> GetAllCompleteAsync();
    }
}
