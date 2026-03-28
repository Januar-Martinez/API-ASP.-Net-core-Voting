using Microsoft.AspNetCore.Mvc;
using API_ASP._Net_core_Voting.API.Common;
using API_ASP._Net_core_Voting.API.DTOs.Request;
using API_ASP._Net_core_Voting.API.Services.Interfaces;

namespace API_ASP._Net_core_Voting.API.Controllers
{
    [ApiController]
    [Route("api/candidates")]
    public class CandidatesController : ControllerBase
    {
        private readonly ICandidateService _candidateService;

        public CandidatesController(ICandidateService candidateService)
        {
            _candidateService = candidateService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var candidates = await _candidateService.GetAllAsync();
            return Ok(ApiResponse<object>.Ok(candidates, "Candidates retrieved successfully"));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var candidate = await _candidateService.GetByIdAsync(id);
            return Ok(ApiResponse<object>.Ok(candidate, "Candidate retrieved successfully"));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> Create([FromBody] CreateCandidateRequest request)
        {
            var candidate = await _candidateService.CreateAsync(request);
            return CreatedAtAction(
                nameof(GetById),
                new { id = candidate.Id },
                ApiResponse<object>.Ok(candidate, "Candidate created successfully")
            );
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            await _candidateService.DeleteAsync(id);
            return Ok(ApiResponse<object>.Ok(null!, "Candidate deleted successfully"));
        }
    }
}