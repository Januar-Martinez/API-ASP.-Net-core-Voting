namespace API_ASP._Net_core_Voting.API.Models
{
    public class Candidate
    {

        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Party { get; set; }
        public int Votes { get; set; } = 0;
        public ICollection<Vote> VotesReceived { get; set; } = new List<Vote>();
    }
}