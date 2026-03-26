using API_ASP._Net_core_Voting.API.DTOs.Request;
using API_ASP._Net_core_Voting.API.DTOs.Response;

namespace API_ASP._Net_core_Voting.API.Services.Interfaces
{
    public interface IVoterService
    {
        Task<IEnumerable<VoterResponse>> GetAllAsync();
        Task<VoterResponse> GetByIdAsync(int id);
        Task<VoterResponse> CreateAsync(CreateVoterRequest request);
        Task DeleteAsync(int id);
    }
}