namespace API_ASP._Net_core_Voting.API.Models
{
    public class Voter
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public bool HasVoted { get; set; } = false;
        public Vote? Vote { get; set; }
    }
}