using Database.Context;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
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

        public async Task<IEnumerable<Audio>> SearchAudio(string speaker, string date, string after, string before, int channel)
        {
            CultureInfo provider = CultureInfo.InvariantCulture;
            IQueryable<Audio> query = Context.Audio;

            var date1 = new DateTime();
            var date2 = new DateTime();

            // date
            if (!string.IsNullOrEmpty(after))
            {
                if (!string.IsNullOrEmpty(before))
                {
                    date1 = DateTime.ParseExact(after, "yyyyMMdd", provider);
                    date2 = DateTime.ParseExact(before, "yyyyMMdd", provider);

                    query = query.Where(a => a.Date >= date1 && a.Date <= date2);
                }
                else
                {
                    date1 = DateTime.ParseExact(after, "yyyyMMdd", provider);

                    query = query.Where(a => a.Date >= date1);
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(before))
                {
                    date2 = DateTime.ParseExact(before, "yyyyMMdd", provider);

                    query = query.Where(a => a.Date <= date2);
                }
                else
                {
                    if (!string.IsNullOrEmpty(date))
                    {
                        date1 = DateTime.ParseExact(date, "yyyyMMdd", provider);

                        query = query.Where(a => a.Date.Date == date1);
                    }
                }
            }

            // speaker
            if (!string.IsNullOrEmpty(speaker))
                query = query.Where(a => a.Sections.Select(s => s.Speaker.Name).Contains(speaker));

            // channel
            if (channel != 0)
                query = query.Where(a => a.Channel.ChannelCode == channel);

            return await query.ToListAsync();
        }
    }
}
