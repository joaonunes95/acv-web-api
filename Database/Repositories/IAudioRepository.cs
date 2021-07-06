using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Database.Repositories
{
    public interface IAudioRepository
    {
        Task<List<Audio>> GetAllAudioAsync();
    }
}
