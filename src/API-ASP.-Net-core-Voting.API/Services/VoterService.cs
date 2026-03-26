using API_ASP._Net_core_Voting.API.Common.Errors;
using API_ASP._Net_core_Voting.API.DTOs.Request;
using API_ASP._Net_core_Voting.API.DTOs.Response;
using API_ASP._Net_core_Voting.API.Models;
using API_ASP._Net_core_Voting.API.Repositories.Interfaces;
using API_ASP._Net_core_Voting.API.Services.Interfaces;

namespace API_ASP._Net_core_Voting.API.Services
{
    public class VoterService : IVoterService
    {
        private readonly IVoterRepository _voterRepository;

        public VoterService(IVoterRepository voterRepository)
        {
            _voterRepository = voterRepository;
        }

        public async Task<IEnumerable<VoterResponse>> GetAllAsync()
        {
            var voters = await _voterRepository.GetAllAsync();

            return voters.Select(v => MapToResponse(v));
        }

        public async Task<VoterResponse> GetByIdAsync(int id)
        {
            var voter = await _voterRepository.GetByIdAsync(id);

            if (voter is null)
                throw new AppException($"Voter with ID {id} not found", 404);

            return MapToResponse(voter);
        }

        public async Task<VoterResponse> CreateAsync(CreateVoterRequest request)
        {
            var existingVoter = await _voterRepository.GetByEmailAsync(request.Email);
            if (existingVoter is not null)
                throw new AppException($"Email {request.Email} is already registered as a voter", 409);

            var existsAsCandidate = await _voterRepository.ExistsAsCandidateAsync(request.Email);
            if (existsAsCandidate)
                throw new AppException($"Email {request.Email} is already registered as a candidate", 409);

            var voter = new Voter
            {
                Name = request.Name,
                Email = request.Email
            };

            var created = await _voterRepository.CreateAsync(voter);
            return MapToResponse(created);
        }

        public async Task DeleteAsync(int id)
        {
            var voter = await _voterRepository.GetByIdAsync(id);

            if (voter is null)
                throw new AppException($"Voter with ID {id} not found", 404);

            await _voterRepository.DeleteAsync(voter);
        }

        private static VoterResponse MapToResponse(Voter voter) => new()
        {
            Id = voter.Id,
            Name = voter.Name,
            Email = voter.Email,
            HasVoted = voter.HasVoted
        };
    }
}