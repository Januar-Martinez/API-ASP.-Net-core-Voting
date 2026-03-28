using Microsoft.AspNetCore.Mvc;
using API_ASP._Net_core_Voting.API.Common;
using API_ASP._Net_core_Voting.API.DTOs.Request;
using API_ASP._Net_core_Voting.API.Services.Interfaces;

namespace API_ASP._Net_core_Voting.API.Controllers
{
    [ApiController]
    [Route("api/voters")]
    public class VotersController : ControllerBase
    {
        private readonly IVoterService _voterService;

        public VotersController(IVoterService voterService)
        {
            _voterService = voterService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var voters = await _voterService.GetAllAsync();
            return Ok(ApiResponse<object>.Ok(voters, "Voters retrieved successfully"));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var voter = await _voterService.GetByIdAsync(id);
            return Ok(ApiResponse<object>.Ok(voter, "Voter retrieved successfully"));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> Create([FromBody] CreateVoterRequest request)
        {
            var voter = await _voterService.CreateAsync(request);

            return CreatedAtAction(
                nameof(GetById),
                new { id = voter.Id },
                ApiResponse<object>.Ok(voter, "Voter created successfully")
            );
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            await _voterService.DeleteAsync(id);
            return Ok(ApiResponse<object>.Ok(null!, "Voter deleted successfully"));
        }
    }
}