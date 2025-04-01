using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Quizee.Api.Models;

public class Quiz
{
    public int Id { get; set; }
    
    [Required]
    public required string Title { get; set; }
    
    [Required]
    public decimal PercentageScored { get; set; }
    
    [Required]
    public required string TimeTaken { get; set; }
    
    [Required]
    public DateTime CreationDate { get; set; }
    
    [Required]
    public int UserId { get; set; }
    
    [ForeignKey("UserId")]
    public User? User { get; set; }
}