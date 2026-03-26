using API_ASP._Net_core_Voting.API.DTOs.Request;
using API_ASP._Net_core_Voting.API.DTOs.Response;

namespace API_ASP._Net_core_Voting.API.Services.Interfaces
{
    public interface ICandidateService
    {
        Task<IEnumerable<CandidateResponse>> GetAllAsync();
        Task<CandidateResponse> GetByIdAsync(int id);
        Task<CandidateResponse> CreateAsync(CreateCandidateRequest request);
        Task DeleteAsync(int id);
    }
}