using Database.Context;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Repositories
{
    public class AudioRepository : IAudioRepository
    {
        private readonly AcvContext _context;

        public AudioRepository(AcvContext context)
        {
            _context = context;
        }

        public async Task<List<Audio>> GetAllAudioAsync()
        {
            return await _context.Audio.ToListAsync();
        }
    }
}
