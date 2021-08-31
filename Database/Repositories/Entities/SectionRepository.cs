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
    public class SectionRepository : Repository<Section>, ISectionRepository
    {
        public SectionRepository(AcvContext context)
            : base(context) { }

        public async Task<Section> GetByStart(Audio audio, double start)
        {
            return await CurrentSet
                            .FirstOrDefaultAsync(x => x.AudioId == audio.Id && x.Start == start);
        }

        public async Task<IEnumerable<Section>> SearchSection(string speaker, string date, string before, string after, int channel)
        {
            CultureInfo provider = CultureInfo.InvariantCulture;
            IQueryable<Section> query = Context.Section;

            var date1 = new DateTime();
            var date2 = new DateTime();

            // date
            if (!string.IsNullOrEmpty(before))
            {
                if (!string.IsNullOrEmpty(after))
                {
                    date1 = DateTime.ParseExact(before, "yyyyMMdd", provider);
                    date2 = DateTime.ParseExact(after, "yyyyMMdd", provider);

                    query = query.Where(s => s.Audio.Date >= date1 && s.Audio.Date <= date2);
                }
                else
                {
                    date1 = DateTime.ParseExact(before, "yyyyMMdd", provider);

                    query = query.Where(s => s.Audio.Date >= date1);
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(after))
                {
                    date2 = DateTime.ParseExact(after, "yyyyMMdd", provider);

                    query = query.Where(s => s.Audio.Date <= date2);
                }
                else
                {
                    if (!string.IsNullOrEmpty(date))
                    {
                        date1 = DateTime.ParseExact(date, "yyyyMMdd", provider);

                        query = query.Where(s => s.Audio.Date == date1);
                    }
                }
            }

            // speaker
            if (!string.IsNullOrEmpty(speaker))
                query = query.Where(s => s.Speaker.Name == speaker);

            // channel
            if (channel != 0)
                query = query.Where(s => s.Audio.Channel.ChannelCode == channel);

            return await query.ToListAsync();
        }
    }
}
