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

        public async Task<IEnumerable<Audio>> SearchAudio(
            string speaker, 
            string dateStr, 
            string after, 
            string before, 
            int channel, 
            string name,
            double RelyGreaterThan,
            double RelySmallerThan)
        {
            IQueryable<Audio> query = Context.Audio;

            // date
            CultureInfo provider = CultureInfo.InvariantCulture;

            var date1 = new DateTime();
            var date2 = new DateTime();

            if (!string.IsNullOrEmpty(dateStr))
            {
                date1 = DateTime.ParseExact(dateStr, "ddMMyyyy", provider);
                query = query.Where(a => a.Date.Date == date1);
            }
            else
                if (!string.IsNullOrEmpty(after) && !string.IsNullOrEmpty(before))
                {
                    date1 = DateTime.ParseExact(after, "ddMMyyyy", provider);
                    date2 = DateTime.ParseExact(before, "ddMMyyyy", provider);
                    query = query.Where(a => a.Date >= date1 && a.Date <= date2);
                }
                else if (!string.IsNullOrEmpty(after))
                    {
                        date1 = DateTime.ParseExact(after, "ddMMyyyy", provider);
                        query = query.Where(a => a.Date >= date1);
                    }
                    else if (!string.IsNullOrEmpty(before))
                    {
                        date1 = DateTime.ParseExact(before, "ddMMyyyy", provider);
                        query = query.Where(a => a.Date <= date1);
                    }

            // Relyability
            if (RelyGreaterThan != 0)
                query = query.Where(a => a.Reliability >= RelyGreaterThan);

            if (RelySmallerThan != 0)
                query = query.Where(a => a.Reliability <= RelySmallerThan);

            // name
            if (!string.IsNullOrEmpty(name))
                query = query.Where(a => a.Name == name);

            // speaker
            if (!string.IsNullOrEmpty(speaker))
                query = query.Where(a => a.Sections.Select(s => s.Speaker.Name).Contains(speaker));

            // channel
            if (channel != 0)
                query = query.Where(a => a.Channel.ChannelCode == channel);

            return await query.Include(x => x.Channel).ToListAsync();
        }
    }
}
