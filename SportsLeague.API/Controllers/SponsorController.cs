using SportsLeague.API.DTOs.Request;

namespace SportsLeague.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SponsorController : ControllerBase
    {
        private readonly ISponsorService _service;

        public SponsorController(ISponsorService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Create(SponsorRequestDTO dto)
        {
            var result = await _service.CreateAsync(dto);
            return Created("", result);
        }

        [HttpPost("{id}/tournaments")]
        public async Task<IActionResult> Link(int id, TournamentSponsorRequestDTO dto)
        {
            await _service.LinkTournamentAsync(id, dto);
            return StatusCode(201);
        }
    }
}
