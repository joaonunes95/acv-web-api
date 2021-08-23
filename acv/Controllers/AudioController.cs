using Application.UseCases.AudioUseCase.Commands.Requests;
using Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AudioController : ControllerBase
    {
        private readonly IAudioRepository _audioRepository;

        public AudioController(IAudioRepository audioRepository)
        {
            _audioRepository = audioRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _audioRepository.GetAllAsync(audio => new 
            {
                audio.Id,
                audio.Name,
                audio.Date,
                audio.Duration,
                audio.Channel.ChannelCode
            }));
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            return Ok(await _audioRepository.GetByIdAsync(id, audio => new
            {
                audio.Id,
                audio.Name,
                audio.Date,
                audio.Duration,
                Sections = audio.Sections.Select(section => new
                {
                    section.Id,
                    section.Duration,
                    section.Speaker.Name,
                    section.Text
                }),
                Channel = new
                {
                    audio.Channel.Id,
                    audio.Channel.ChannelCode,
                    audio.Channel.Name
                },
                AudioPlayer = "Retorno para tocar áudio..."
            }));
        }

        [HttpPost]
        public async Task<IActionResult> Post(
            [FromServices] IMediator mediator,
            [FromBody] PostAudioRequest command)
        {
            var result = await mediator.Send(command);

            if (!result.Success)
                return BadRequest(result.Success);

            return Ok("Deu bom");
        }

        [HttpPost("analise")]
        public IActionResult AnalyzeAudio([FromBody] AudioModel input)
        {
            // analisar audio pontual vai ser o único post aqui eu acho
            // na verdade o que o acv poe de entrada são Sections
            return Ok(input);
        }
    }
}
