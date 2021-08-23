using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface ISectionRepository : IRepository<Section>
    {
        Task<Section> GetByStart(Audio audio, double start);
    }
}
