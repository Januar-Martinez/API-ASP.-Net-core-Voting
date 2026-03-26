using System.ComponentModel.DataAnnotations;

namespace API_ASP._Net_core_Voting.API.DTOs.Request
{
    public class CreateCandidateRequest
    {
        [Required(ErrorMessage = "Name is required")]
        [MaxLength(100, ErrorMessage = "Name cannot exceed 100 characters")]
        public string Name { get; set; } = string.Empty;

        [MaxLength(100, ErrorMessage = "Party cannot exceed 100 characters")]
        public string? Party { get; set; }
    }
}