using Database.Repositories;
using Microsoft.AspNetCore.Mvc;
using Presentation.Models;
using System;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("[controller]")]
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
            var audio = await _audioRepository.GetAllAudioAsync();


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


        [HttpPost()]
        public IActionResult RegisterUser([FromBody] AudioModel input)
        {
            return Ok(input);
        }
    }
}
