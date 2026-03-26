using API_ASP._Net_core_Voting.API.Common.Errors;
using API_ASP._Net_core_Voting.API.DTOs.Request;
using API_ASP._Net_core_Voting.API.DTOs.Response;
using API_ASP._Net_core_Voting.API.Models;
using API_ASP._Net_core_Voting.API.Repositories.Interfaces;
using API_ASP._Net_core_Voting.API.Services.Interfaces;

namespace API_ASP._Net_core_Voting.API.Services
{
    public class VoteService : IVoteService
    {
        private readonly IVoteRepository _voteRepository;
        private readonly IVoterRepository _voterRepository;
        private readonly ICandidateRepository _candidateRepository;

        public VoteService(
            IVoteRepository voteRepository,
            IVoterRepository voterRepository,
            ICandidateRepository candidateRepository)
        {
            _voteRepository = voteRepository;
            _voterRepository = voterRepository;
            _candidateRepository = candidateRepository;
        }

        public async Task<IEnumerable<VoteResponse>> GetAllAsync()
        {
            var votes = await _voteRepository.GetAllAsync();
            return votes.Select(v => MapToResponse(v));
        }

        public async Task<VoteResponse> CreateAsync(CreateVoteRequest request)
        {
            var voter = await _voterRepository.GetByIdAsync(request.VoterId);
            if (voter is null)
                throw new AppException($"Voter with ID {request.VoterId} not found", 404);

            if (voter.HasVoted)
                throw new AppException($"Voter with ID {request.VoterId} has already voted", 409);

            var candidate = await _candidateRepository.GetByIdAsync(request.CandidateId);
            if (candidate is null)
                throw new AppException($"Candidate with ID {request.CandidateId} not found", 404);

            var vote = new Vote
            {
                VoterId = request.VoterId,
                CandidateId = request.CandidateId
            };

            voter.HasVoted = true;

            candidate.Votes += 1;

            var created = await _voteRepository.CreateAsync(vote);
            return MapToResponse(created);
        }

        public async Task<VoteStatisticsResponse> GetStatisticsAsync()
        {
            var votes = await _voteRepository.GetAllAsync();
            var candidates = await _candidateRepository.GetAllAsync();

            int totalVotes = votes.Count();

            var results = candidates.Select(c => new CandidateStatistic
            {
                CandidateId = c.Id,
                CandidateName = c.Name,
                Party = c.Party,
                VotesReceived = c.Votes,

                Percentage = totalVotes > 0
                    ? Math.Round((double)c.Votes / totalVotes * 100, 2)
                    : 0
            });

            return new VoteStatisticsResponse
            {
                TotalVotesCast = totalVotes,
                Results = results
            };
        }

        private static VoteResponse MapToResponse(Vote vote) => new()
        {
            Id = vote.Id,
            VoterId = vote.VoterId,
            VoterName = vote.Voter.Name,
            CandidateId = vote.CandidateId,
            CandidateName = vote.Candidate.Name,
            VotedAt = vote.VotedAt
        };
    }
}