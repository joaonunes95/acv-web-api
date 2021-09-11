using Application.UseCases.SectionUseCase.Commands.Requests;
using Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    public class SectionController : ControllerBase
    {
        private readonly ISectionRepository _sectionRepository;

        public SectionController(ISectionRepository sectionRepository)
        {
            _sectionRepository = sectionRepository;
        }

        /// <summary>
        /// Gets all sections
        /// </summary>
        /// <returns>A list of sections</returns>
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

        /// <summary>
        /// Gets one section by its Id
        /// </summary>
        /// <param name="id">Section's Id</param>
        /// <returns>One section</returns>
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {

            return Ok(await _sectionRepository.GetByIdAsync(id));
        }
    }
}
