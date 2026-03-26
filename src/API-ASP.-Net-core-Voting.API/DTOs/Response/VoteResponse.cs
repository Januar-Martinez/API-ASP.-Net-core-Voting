namespace API_ASP._Net_core_Voting.API.DTOs.Response
{
    public class VoteResponse
    {
        public int Id { get; set; }
        public int VoterId { get; set; }
        public string VoterName { get; set; } = string.Empty;
        public int CandidateId { get; set; }
        public string CandidateName { get; set; } = string.Empty;
        public DateTime VotedAt { get; set; }
    }
}