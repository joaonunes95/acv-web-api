using Database.Context;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Database.Repositories.Entities
{
    public class SectionRepository : Repository<Section>, ISectionRepository
    {
        public SectionRepository(AcvContext context)
            : base(context) { }

        public async Task<Section> GetByStart(Audio audio, double start)
        {
            return await CurrentSet
                            .FirstOrDefaultAsync(x => x.AudioId == audio.Id && x.Start == start);
        }

    }
}
