using API_ASP._Net_core_Voting.API.Models;

namespace API_ASP._Net_core_Voting.API.Repositories.Interfaces
{
    public interface IVoterRepository
    {
        Task<IEnumerable<Voter>> GetAllAsync();
        Task<Voter?> GetByIdAsync(int id);
        Task<Voter?> GetByEmailAsync(string email);
        Task<Voter> CreateAsync(Voter voter);
        Task DeleteAsync(Voter voter);
        Task<bool> ExistsAsCandidateAsync(string email);
    }
}