using Application.UseCases.AudioUseCase.Commands.Requests;
using Application.UseCases.AudioUseCase.Commands.Responses;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.UseCases.AudioUseCase
{
    public interface IAudioCommandHandler
    {
        Task<PostAudioResponse> Handle(PostAudioRequest request, CancellationToken cancellationToken);
    }
}
