namespace API_ASP._Net_core_Voting.API.DTOs.Response
{
    public class VoteStatisticsResponse
    {
        public int TotalVotesCast { get; set; }
        public IEnumerable<CandidateStatistic> Results { get; set; } = new List<CandidateStatistic>();
    }

    public class CandidateStatistic
    {
        public int CandidateId { get; set; }
        public string CandidateName { get; set; } = string.Empty;
        public string? Party { get; set; }
        public int VotesReceived { get; set; }
        public double Percentage { get; set; }
    }
}