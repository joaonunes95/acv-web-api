using Application.UseCases.AccountUseCase.Commands.Responses;
using Application.UseCases.AudioUseCase.Commands.Responses;
using Application.UseCases.SectionUseCase.Commands.Responses;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Application.UseCases.AudioUseCase.Commands.Requests
{
    public class PostAudioRequest : IRequest<PostAudioResponse>
    {
        public IEnumerable<SectionInput> Sections { get; set; }

        public class SectionInput
        {
            public Guid? AudioId { get; set; }
            public string Audio_filepath { get; set; }
            public double Duration { get; set; }
            public double AudioDuration { get; set; }
            public double Start { get; set; }
            public string Text { get; set; }
            public int ChannelCode { get; set; }
            public DateTime Date { get; set; }
            public string Speaker { get; set; }
        }
    }
}
