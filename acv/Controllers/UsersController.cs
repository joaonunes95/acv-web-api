using Microsoft.AspNetCore.Http;
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
    public class UsersController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("hello from get all");
        }

        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            if (id == Guid.Empty)
                return NotFound();

            var username = "caina: " + id;
            return Ok(username);
        }


        [HttpPost()]
        public IActionResult RegisterUser([FromBody] UserModel input)
        {
            return Ok(input);
        }
    }
}
