using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Presentation.Models;
using System;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChannelController : ControllerBase
    {
        private readonly IChannelRepository _channelRepository;

        public ChannelController(IChannelRepository channelRepository)
        {
            _channelRepository = channelRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var audio = await _channelRepository.GetAllAsync();

            return Ok(audio);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            if (id == Guid.Empty)
                return NotFound();

            var username = "audio: " + id;
            return Ok(username);
        }


        //[HttpPost()]
        //public IActionResult AnalyzeAudio([FromBody] PostChannelRequest command)
        //{

        //    return Ok(input);
        //}
    }
}
