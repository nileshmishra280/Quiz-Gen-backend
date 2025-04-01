using System.ComponentModel.DataAnnotations;

namespace Quizee.Api.DTOs;

public class QuizDto
{
    [Required]
    public required string Title { get; set; }
    
    [Required]
    public decimal PercentageScored { get; set; }
    
    [Required]
    public required string TimeTaken { get; set; }
    
    [Required]
    public required string UserId { get; set; }
}