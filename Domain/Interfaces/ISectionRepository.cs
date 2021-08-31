using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface ISectionRepository : IRepository<Section>
    {
        Task<Section> GetByStart(Audio audio, double start);
        Task<IEnumerable<Section>> SearchSection(string speaker, string date, string before, string after, int channel);
    }
}
