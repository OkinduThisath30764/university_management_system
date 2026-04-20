using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlindMatchPAS.Core.Entities
{
    public class ProjectProposal
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Title is required")]
        [MaxLength(200)]
        public string Title { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "Abstract is required")]
        public string Abstract { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "Technical stack is required")]
        [RegularExpression(@"^[a-zA-Z0-9,\s\.\+#]+$", ErrorMessage = "Tech Stack contains invalid characters. Use alphanumeric, spaces, and [, . + #]")]
        public string TechStack { get; set; } = string.Empty;
        
        [Required]
        public int ResearchAreaId { get; set; }
        public ResearchArea? ResearchArea { get; set; }
        
        [Required]
        public string StudentId { get; set; } = string.Empty;
        [ForeignKey(nameof(StudentId))]
        public ApplicationUser? Student { get; set; }
        
        public string Status { get; set; } = "Pending";
        
        public string? MatchedSupervisorId { get; set; }
        [ForeignKey(nameof(MatchedSupervisorId))]
        public ApplicationUser? MatchedSupervisor { get; set; }
    }
}
