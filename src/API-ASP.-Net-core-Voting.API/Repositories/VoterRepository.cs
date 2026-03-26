using Microsoft.EntityFrameworkCore;
using API_ASP._Net_core_Voting.API.Data;
using API_ASP._Net_core_Voting.API.Models;
using API_ASP._Net_core_Voting.API.Repositories.Interfaces;

namespace API_ASP._Net_core_Voting.API.Repositories
{
    public class VoterRepository : IVoterRepository
    {
        private readonly AppDbContext _context;

        public VoterRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Voter>> GetAllAsync() =>
            await _context.Voters.ToListAsync();

        public async Task<Voter?> GetByIdAsync(int id) =>
            await _context.Voters.FindAsync(id);

        public async Task<Voter?> GetByEmailAsync(string email) =>
            await _context.Voters
                .FirstOrDefaultAsync(v => v.Email.ToLower() == email.ToLower());

        public async Task<Voter> CreateAsync(Voter voter)
        {
            _context.Voters.Add(voter);

            await _context.SaveChangesAsync();

            return voter;
        }

        public async Task DeleteAsync(Voter voter)
        {
            _context.Voters.Remove(voter);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsCandidateAsync(string email) =>
            await _context.Candidates
                .AnyAsync(c => c.Name.ToLower() == email.ToLower());
    }
}