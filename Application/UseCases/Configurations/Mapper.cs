using Application.UseCases.AudioUseCase.Commands.Requests;
using AutoMapper;
using Domain.Entities;
using System;
using System.Globalization;

namespace Application.UseCases.Configurations
{
    public class Mapper : Profile
    {
        public Mapper()
        {
            CreateMap<PostAudioRequest.SectionInput, Audio>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => getAudioName(src.AudioName)))
                .ForMember(dest => dest.Reliability, opt => opt.MapFrom(src => src.AudioReliability))
                .ForMember(dest => dest.Duration, opt => opt.MapFrom(src => src.AudioDuration));

            CreateMap<PostAudioRequest.SectionInput, Section>()
                .ForMember(dest => dest.Speaker, opt => opt.Ignore());
        }

        private string getAudioName(string crude)
        {
            CultureInfo provider = CultureInfo.InvariantCulture;
            var crudeDate = crude.Substring(0, crude.IndexOf('x'));
            var date = DateTime.ParseExact(crudeDate, "yyyyMMdd_HHmmss", provider);

            var channelCode = int.Parse(crude.Substring(crude.IndexOf('L')+1));

            return date.ToString("yyyyMMdd_HHmmss_") + channelCode.ToString().PadLeft(3, '0'); ;
        }
    }
}
