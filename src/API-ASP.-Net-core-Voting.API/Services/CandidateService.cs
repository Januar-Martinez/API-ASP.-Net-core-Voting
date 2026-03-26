using API_ASP._Net_core_Voting.API.Common.Errors;
using API_ASP._Net_core_Voting.API.DTOs.Request;
using API_ASP._Net_core_Voting.API.DTOs.Response;
using API_ASP._Net_core_Voting.API.Models;
using API_ASP._Net_core_Voting.API.Repositories.Interfaces;
using API_ASP._Net_core_Voting.API.Services.Interfaces;

namespace API_ASP._Net_core_Voting.API.Services
{
    public class CandidateService : ICandidateService
    {
        private readonly ICandidateRepository _candidateRepository;

        public CandidateService(ICandidateRepository candidateRepository)
        {
            _candidateRepository = candidateRepository;
        }

        public async Task<IEnumerable<CandidateResponse>> GetAllAsync()
        {
            var candidates = await _candidateRepository.GetAllAsync();
            return candidates.Select(c => MapToResponse(c));
        }

        public async Task<CandidateResponse> GetByIdAsync(int id)
        {
            var candidate = await _candidateRepository.GetByIdAsync(id);

            if (candidate is null)
                throw new AppException($"Candidate with ID {id} not found", 404);

            return MapToResponse(candidate);
        }

        public async Task<CandidateResponse> CreateAsync(CreateCandidateRequest request)
        {
            var existsAsVoter = await _candidateRepository.ExistsAsVoterAsync(request.Name);
            if (existsAsVoter)
                throw new AppException($"{request.Name} is already registered as a voter", 409);

            var candidate = new Candidate
            {
                Name = request.Name,
                Party = request.Party
            };

            var created = await _candidateRepository.CreateAsync(candidate);
            return MapToResponse(created);
        }

        public async Task DeleteAsync(int id)
        {
            var candidate = await _candidateRepository.GetByIdAsync(id);

            if (candidate is null)
                throw new AppException($"Candidate with ID {id} not found", 404);

            await _candidateRepository.DeleteAsync(candidate);
        }

        private static CandidateResponse MapToResponse(Candidate candidate) => new()
        {
            Id = candidate.Id,
            Name = candidate.Name,
            Party = candidate.Party,
            Votes = candidate.Votes
        };
    }
}