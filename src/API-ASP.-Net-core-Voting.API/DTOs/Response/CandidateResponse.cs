namespace API_ASP._Net_core_Voting.API.DTOs.Response
{
    public class CandidateResponse
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Party { get; set; }
        public int Votes { get; set; }
    }
}