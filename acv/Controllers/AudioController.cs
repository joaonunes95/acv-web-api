using Microsoft.AspNetCore.Mvc;
using Presentation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AudioController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("hello from get all audio");
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
