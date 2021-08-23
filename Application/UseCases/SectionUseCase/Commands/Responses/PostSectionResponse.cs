using System;

namespace Application.UseCases.SectionUseCase.Commands.Responses
{
    public class PostSectionResponse
    {
        public bool Success { get; set; }
        public Guid AudioId { get; set; }
        public Guid SectionId { get; set; }
    }
}
