using Database.Context;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Database.Repositories.Entities
{
    public class SpeakerRepository : Repository<Speaker>, ISpeakerRepository
    {
        public SpeakerRepository(AcvContext context)
            : base(context) { }

        public async Task<Speaker> GetByNameLocal(string name)
        {
            var speaker = CurrentSet.Local.FirstOrDefault(x => x.Name == name);

            if (speaker is null)
                speaker = await CurrentSet.FirstOrDefaultAsync(x => x.Name == name);

            return speaker;
        }
    }
}
