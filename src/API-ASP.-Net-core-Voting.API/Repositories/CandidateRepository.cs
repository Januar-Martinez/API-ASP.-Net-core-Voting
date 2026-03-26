using Microsoft.EntityFrameworkCore;
using API_ASP._Net_core_Voting.API.Data;
using API_ASP._Net_core_Voting.API.Models;
using API_ASP._Net_core_Voting.API.Repositories.Interfaces;

namespace API_ASP._Net_core_Voting.API.Repositories
{
    public class CandidateRepository : ICandidateRepository
    {
        private readonly AppDbContext _context;

        public CandidateRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Candidate>> GetAllAsync() =>
            await _context.Candidates.ToListAsync();

        public async Task<Candidate?> GetByIdAsync(int id) =>
            await _context.Candidates.FindAsync(id);

        public async Task<Candidate> CreateAsync(Candidate candidate)
        {
            _context.Candidates.Add(candidate);
            await _context.SaveChangesAsync();
            return candidate;
        }

        public async Task DeleteAsync(Candidate candidate)
        {
            _context.Candidates.Remove(candidate);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsVoterAsync(string email) =>
            await _context.Voters
                .AnyAsync(v => v.Email.ToLower() == email.ToLower());
    }
}