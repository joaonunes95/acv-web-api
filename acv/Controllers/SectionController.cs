using Application.UseCases.SectionUseCase.Commands.Requests;
using Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SectionController : ControllerBase
    {
        private readonly ISectionRepository _sectionRepository;

        public SectionController(ISectionRepository sectionRepository)
        {
            _sectionRepository = sectionRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _sectionRepository.GetAllAsync(x => new
            {
                Id = x.Id,
                Text = x.Text,
                Duration = x.Duration,
                Start = x.Start,
                Audio = x.Audio,
                Speaker = x.Speaker
            }));
        }

        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            if (id == Guid.Empty)
                return NotFound();

            var username = "audio: " + id;
            return Ok(username);
        }

        [HttpPost]
        public async Task<IActionResult> Post(
            [FromServices] IMediator mediator,
            [FromBody] PostSectionRequest command)
        {

            var result = await mediator.Send(command);

            if (!result.Success)
                return BadRequest(result.Success);


            return Ok("Deu bom");
        }
    }
}
