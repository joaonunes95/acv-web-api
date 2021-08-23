using Application.UseCases.SectionUseCase.Commands.Requests;
using Application.UseCases.SectionUseCase.Commands.Responses;
using System.Threading;
using System.Threading.Tasks;

namespace Application.UseCases.ModeloUseCase
{
    interface ISectionCommandHandler
    {
        Task<PostSectionResponse> Handle(PostSectionRequest request, CancellationToken cancellationToken);
    }
}
