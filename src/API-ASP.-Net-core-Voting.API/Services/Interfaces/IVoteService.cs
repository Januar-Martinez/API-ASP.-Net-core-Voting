using API_ASP._Net_core_Voting.API.DTOs.Request;
using API_ASP._Net_core_Voting.API.DTOs.Response;

namespace API_ASP._Net_core_Voting.API.Services.Interfaces
{
    public interface IVoteService
    {
        Task<IEnumerable<VoteResponse>> GetAllAsync();
        Task<VoteResponse> CreateAsync(CreateVoteRequest request);
        Task<VoteStatisticsResponse> GetStatisticsAsync();
    }
}