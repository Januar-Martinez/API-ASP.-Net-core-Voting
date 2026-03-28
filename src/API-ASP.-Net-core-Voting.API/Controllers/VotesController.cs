using Microsoft.AspNetCore.Mvc;
using API_ASP._Net_core_Voting.API.Common;
using API_ASP._Net_core_Voting.API.DTOs.Request;
using API_ASP._Net_core_Voting.API.Services.Interfaces;

namespace API_ASP._Net_core_Voting.API.Controllers;

[ApiController]
[Route("api/votes")]
public class VotesController : ControllerBase
{
    private readonly IVoteService _voteService;

    public VotesController(IVoteService voteService)
    {
        _voteService = voteService;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        var votes = await _voteService.GetAllAsync();
        return Ok(ApiResponse<object>.Ok(votes, "Votes retrieved successfully"));
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Create([FromBody] CreateVoteRequest request)
    {
        var vote = await _voteService.CreateAsync(request);
        return CreatedAtAction(
            nameof(GetAll),
            ApiResponse<object>.Ok(vote, "Vote cast successfully")
        );
    }

    [HttpGet("statistics")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetStatistics()
    {
        var statistics = await _voteService.GetStatisticsAsync();
        return Ok(ApiResponse<object>.Ok(statistics, "Statistics retrieved successfully"));
    }
}