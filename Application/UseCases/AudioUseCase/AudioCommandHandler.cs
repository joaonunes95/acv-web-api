using Application.UseCases.AudioUseCase.Commands.Requests;
using Application.UseCases.AudioUseCase.Commands.Responses;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.UseCases.AudioUseCase
{
    public class AudioCommandHandler : 
        IAudioCommandHandler,
        IRequestHandler<PostAudioRequest, PostAudioResponse>
    {
        private readonly ISectionRepository _sectionRepository;
        private readonly IAudioRepository _audioRepository;
        private readonly IChannelRepository _channelRepository;
        private readonly ISpeakerRepository _speakerRepository;
        private readonly IMapper _mapper;

        public AudioCommandHandler(
            ISectionRepository sectionRepository,
            IAudioRepository audioRepository,
            IChannelRepository channelRepository,
            ISpeakerRepository speakerRepository,
            IMapper mapper)
        {
            _sectionRepository = sectionRepository;
            _audioRepository = audioRepository;
            _channelRepository = channelRepository;
            _speakerRepository = speakerRepository;
            _mapper = mapper;
        }

        public async Task<PostAudioResponse> Handle(PostAudioRequest request, CancellationToken cancellationToken)
        {
            if (request is null)
                throw new ArgumentNullException();

            foreach (var section in request.Sections)
            {
                var audioName = section.Date.ToString("yyyyMMdd_HHmmss_") + section.ChannelCode.ToString().PadLeft(3, '0');

                var newSection = _mapper.Map<Section>(section);

                var audio = await _audioRepository.GetByNameLocal(audioName);
                var channel = await GetChannel(section.ChannelCode);
                var speaker = await GetSpeaker(section.Speaker);
                
                if (audio is null)
                {
                    audio = _mapper.Map<Audio>(section);
                    
                    audio.Channel = channel;
                                        
                    newSection.Speaker = speaker;
                    newSection.Audio = audio;

                    _audioRepository.Add(audio);
                }
                else
                {
                    if (await _sectionRepository.AnyAsync(x => x.AudioId == audio.Id && x.Start == section.Start))
                        return new PostAudioResponse { Success = false }; // pensar numa resposta desaforada - section repetida

                    newSection.Speaker = speaker;
                    newSection.Audio = audio;
                }
                
                _sectionRepository.Add(newSection);
            }

            await _audioRepository.CommitAsync();

            return new PostAudioResponse
            {
                Success = true
            };
        }

        #region aux

        private async Task<Channel> GetChannel(int code)
        {
            var channel = await _channelRepository.GetByCodeLocal(code);

            if (channel is null)
            {
                channel = new Channel()
                {
                    Id = Guid.NewGuid(),
                    ChannelCode = code
                };

                _channelRepository.Add(channel);
            }

            return channel;
        }

        private async Task<Speaker> GetSpeaker(string name)
        {
            var speaker = await _speakerRepository.GetByNameLocal(name);

            if (speaker is null)
            {
                speaker = new Speaker()
                {
                    Id = Guid.NewGuid(),
                    Name = name
                };

                _speakerRepository.Add(speaker);
            }

            return speaker;
        }

        #endregion
    }
}
