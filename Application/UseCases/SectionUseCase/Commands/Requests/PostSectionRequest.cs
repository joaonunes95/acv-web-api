using Application.UseCases.AccountUseCase.Commands.Responses;
using Application.UseCases.SectionUseCase.Commands.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Application.UseCases.SectionUseCase.Commands.Requests
{
    public class PostSectionRequest : IRequest<PostSectionResponse>
    {
        public IEnumerable<SectionInput> Sections { get; set; }

        public class SectionInput
        {
            public Guid? AudioId { get; set; }
            public string Audio_filepath { get; set; }
            public double Duration { get; set; }
            public double Start { get; set; }
            public string Classe { get; set; }
            public string Text { get; set; }
            public int ChannelCode { get; set; }
            public DateTime Date { get; set; }
        }
    }
}
