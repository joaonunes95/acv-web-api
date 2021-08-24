using Application.UseCases.AudioUseCase.Commands.Responses;
using MediatR;
using System;

namespace Application.UseCases.AudioUseCase.Commands.Requests
{
    public class GetAudioRequest : IRequest<GetAudioResponse>
    {
        public Guid? AudioId { get; set; }
        public DateTime Date { get; set; }
        public int ChannelCode { get; set; }
        public string Speaker { get; set; }
        public double Duration { get; set; } // precisa?
        public string Text { get; set; } // problema
    }
}
