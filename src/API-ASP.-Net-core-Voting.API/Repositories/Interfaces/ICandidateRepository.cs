using API_ASP._Net_core_Voting.API.Models;

namespace API_ASP._Net_core_Voting.API.Repositories.Interfaces
{
    public interface ICandidateRepository
    {
        Task<IEnumerable<Candidate>> GetAllAsync();
        Task<Candidate?> GetByIdAsync(int id);
        Task<Candidate> CreateAsync(Candidate candidate);
        Task DeleteAsync(Candidate candidate);
        Task<bool> ExistsAsVoterAsync(string email);
    }
}