using System;

namespace Application.UseCases.AudioUseCase.Commands.Responses
{
    public class PostAudioResponse
    {
        public bool Success { get; set; }
        public Guid AudioId { get; set; }
        public Guid SectionId { get; set; }
    }
}
