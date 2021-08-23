using Database.Context;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Database.Repositories.Entities
{
    public class AudioRepository : Repository<Audio>, IAudioRepository
    {
        public AudioRepository(AcvContext context)
            : base(context) { }

        public async Task<Audio> GetByName(string name)
        {
            return await CurrentSet.FirstOrDefaultAsync(x => x.Name == name);
        }

        public async Task<Audio> GetByNameLocal(string name)
        {
            var audio = CurrentSet.Local.FirstOrDefault(x => x.Name == name);

            if(audio is null)
                audio = await CurrentSet.FirstOrDefaultAsync(x => x.Name == name);

            return audio;
        }

        public async Task<IEnumerable<Audio>> GetAllCompleteAsync()
        {
            return await CurrentSet
                            .Include(x => x.Sections)
                            .ToListAsync();
        }

        public void AddSection(Section section)
        {
            Context.Set<Section>().Add(section);
        }
    }
}
