using Application.UseCases.AudioUseCase.Commands.Requests;
using AutoMapper;
using Domain.Entities;

namespace Application.UseCases.Configurations
{
    public class Mapper : Profile
    {
        public Mapper()
        {
            CreateMap<PostAudioRequest.SectionInput, Audio>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Date.ToString("yyyyMMdd_HHmmss_") + src.ChannelCode.ToString().PadLeft(3, '0')))
                .ForMember(dest => dest.Duration, opt => opt.MapFrom(src => src.AudioDuration));

            CreateMap<PostAudioRequest.SectionInput, Section>()
                .ForMember(dest => dest.Speaker, opt => opt.Ignore());
        }
    }
}
