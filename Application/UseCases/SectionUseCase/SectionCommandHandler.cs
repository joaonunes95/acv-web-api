using Application.UseCases.SectionUseCase.Commands.Requests;
using Application.UseCases.SectionUseCase.Commands.Responses;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.UseCases.ModeloUseCase
{
    public class SectionCommandHandler :
        ISectionCommandHandler,
        IRequestHandler<PostSectionRequest, PostSectionResponse>
    {
        private readonly ISectionRepository _sectionRepository;
        private readonly IAudioRepository _audioRepository;
        private readonly IMapper _mapper;

        public SectionCommandHandler(
            ISectionRepository sectionRepository,
            IAudioRepository audioRepository,
            IMapper mapper)
        {
            _sectionRepository = sectionRepository;
            _audioRepository = audioRepository;
            _mapper = mapper;
        }


        public async Task<PostSectionResponse> Handle(PostSectionRequest request, CancellationToken cancellationToken)
        {
            return new PostSectionResponse
            {
                Success = false
            };
        }
    }
}
