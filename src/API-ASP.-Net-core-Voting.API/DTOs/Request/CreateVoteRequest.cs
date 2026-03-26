using System.ComponentModel.DataAnnotations;

namespace API_ASP._Net_core_Voting.API.DTOs.Request
{
    public class CreateVoteRequest
    {
        [Required(ErrorMessage = "VoterId is required")]
        public int VoterId { get; set; }

        [Required(ErrorMessage = "CandidateId is required")]
        public int CandidateId { get; set; }
    }
}