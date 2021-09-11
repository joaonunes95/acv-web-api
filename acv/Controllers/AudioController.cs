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
    [Authorize(Roles = "Admin,User")]
    public class AudioController : ControllerBase
    {
        private readonly IAudioRepository _audioRepository;

        public AudioController(IAudioRepository audioRepository)
        {
            _audioRepository = audioRepository;
        }

        /// <summary>
        /// Gets basic information of all audio
        /// </summary>
        /// <returns>A list of audio</returns>
        /// <remarks>
        /// 
        /// Sample request: GET /api/Audio
        /// 
        /// </remarks>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _audioRepository.GetAllAsync(audio => new 
            {
                audio.Id,
                audio.Name,
                audio.Reliability,
                audio.Date,
                audio.Duration,
                Channel = new
                {
                    audio.Channel.Name,
                    audio.Channel.ChannelCode
                }
            }));
        }

        /// <summary>
        /// Gets basic information of searched audio
        /// </summary>
        /// <param name="channel">Channel code of the audio's channel</param>
        /// <param name="speaker">Name of any speaker of any audio's section</param>
        /// <param name="date">Exact date of an audio's record</param>
        /// <param name="after">Bottom limit of date of audio's record</param>
        /// <param name="before">Top limit of date of audio's record</param>
        /// <param name="name">Audio's name</param>
        /// <param name="RelySmallerThan">Top limit of date of audio's record</param>
        /// <param name="RelyGreaterThan">Bottom limit of date of audio's record</param>
        /// <returns>
        /// A list of audio
        /// </returns>
        [HttpGet("search")]
        public async Task<IActionResult> GetSearch(
            [FromQuery] int channel,
            [FromQuery] string speaker, 
            [FromQuery] string date, 
            [FromQuery] string after, 
            [FromQuery] string before,
            [FromQuery] string name,
            [FromQuery] double RelySmallerThan,
            [FromQuery] double RelyGreaterThan
            )
        {
            try
            {
                var result = await _audioRepository.SearchAudio(speaker, date, after, before, channel, name, RelyGreaterThan, RelySmallerThan);

                if (result.Any())
                {
                    return Ok(result.Select(audio => new
                    {
                        audio.Id,
                        audio.Name,
                        audio.Reliability,
                        audio.Date,
                        audio.Duration,
                        Channel = new
                        {
                            audio.Channel.Name,
                            audio.Channel.ChannelCode
                        }
                    }));
                }

                return NotFound();
            }
            catch
            {
                return BadRequest("Error retrieving data from database");
            }
        }

        /// <summary>
        /// Gets filepath, basic information, channel and sections of an audio
        /// </summary>
        /// <param name="id">Audio's Id</param>
        /// <returns>A full audio object</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            return Ok(await _audioRepository.GetByIdAsync(id, audio => new
            {
                audio.Id,
                audio.Name,
                audio.FilePath,
                audio.Reliability,
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
                }
            }));
        }

        /// <summary>
        /// Posts one or many audio
        /// </summary>
        /// <param name="mediator">Not a frontend's concern</param>
        /// <param name="command">List of sections of one or many audio</param>
        /// <returns>Success ou Error message</returns>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PostAnalysis(
            [FromServices] IMediator mediator,
            [FromBody] PostAudioRequest command)
        {
            var result = await mediator.Send(command);

            if (!result.Success)
                return BadRequest(result.Success);

            return Ok("Analysis post completed successfully");
        }

        //[HttpPost("analise")]
        //public IActionResult AnalyzeAudio([FromBody] AudioModel input)
        //{
        //    // analisar audio pontual vai ser o único post aqui eu acho
        //    // na verdade o que o acv poe de entrada são Sections
        //    return Ok(input);
        //}
    }
}
