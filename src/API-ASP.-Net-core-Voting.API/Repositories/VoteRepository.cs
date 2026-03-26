using Microsoft.EntityFrameworkCore;
using API_ASP._Net_core_Voting.API.Data;
using API_ASP._Net_core_Voting.API.Models;
using API_ASP._Net_core_Voting.API.Repositories.Interfaces;

namespace API_ASP._Net_core_Voting.API.Repositories
{
    public class VoteRepository : IVoteRepository
    {
        private readonly AppDbContext _context;

        public VoteRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Vote>> GetAllAsync() =>
            await _context.Votes
                .Include(v => v.Voter)
                .Include(v => v.Candidate)
                .ToListAsync();

        public async Task<Vote> CreateAsync(Vote vote)
        {
            _context.Votes.Add(vote);
            await _context.SaveChangesAsync();
            return vote;
        }
    }
}