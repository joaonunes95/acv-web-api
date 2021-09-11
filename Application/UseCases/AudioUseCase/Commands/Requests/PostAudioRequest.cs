using Application.UseCases.AudioUseCase.Commands.Responses;
using MediatR;
using System;
using System.Collections.Generic;

namespace Application.UseCases.AudioUseCase.Commands.Requests
{
    public class PostAudioRequest : IRequest<PostAudioResponse>
    {
        public IEnumerable<SectionInput> Sections { get; set; }

        public class SectionInput
        {
            public string AudioName { get; set; }
            public string FilePath { get; set; }
            public double Duration { get; set; }
            public double AudioDuration { get; set; }
            public double AudioReliability { get; set; }
            public double Start { get; set; }
            public string Speaker { get; set; }
            public string Text { get; set; }
            public Guid? AudioId { get; set; }
        }
    }
}
