using Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.Models;
using System;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    public class ChannelController : ControllerBase
    {
        private readonly IChannelRepository _channelRepository;

        public ChannelController(IChannelRepository channelRepository)
        {
            _channelRepository = channelRepository;
        }

        /// <summary>
        /// Gets all channels
        /// </summary>
        /// <returns>A list of channels</returns>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var audio = await _channelRepository.GetAllAsync();

            return Ok(audio);
        }

        /// <summary>
        /// Gets a channel by its Id
        /// </summary>
        /// <param name="id">Channel's Id</param>
        /// <returns>One channel</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {

            return Ok(await _channelRepository.GetByIdAsync(id));
        }

        //[HttpPost()]
        //public IActionResult AnalyzeAudio([FromBody] PostChannelRequest command)
        //{

        //    return Ok(input);
        //}
    }
}
