using Application.Models;
using Domain.Entities;
using System;
using System.Collections.Generic;

namespace Application.UseCases.AudioUseCase.Commands.Responses
{
    public class GetAudioResponse
    {
        public bool Success { get; set; }
        public ICollection<AudioModel> Audio { get; set; }
    }
}
