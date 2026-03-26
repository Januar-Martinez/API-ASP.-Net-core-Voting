using API_ASP._Net_core_Voting.API.Models;

namespace API_ASP._Net_core_Voting.API.Repositories.Interfaces
{
    public interface IVoteRepository
    {
        Task<IEnumerable<Vote>> GetAllAsync();
        Task<Vote> CreateAsync(Vote vote);
    }
}