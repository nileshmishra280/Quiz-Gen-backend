using System.ComponentModel.DataAnnotations;

namespace Quizee.Api.Models;

public class Question
{
    public int Id { get; set; }

    [Required]
    public required string QuestionText { get; set; }

    [Required]
    public required string[] Options { get; set; }

    [Required]
    public int CorrectAnswer { get; set; }

    [Required]
    public DateTime CreatedDate { get; set; }

    [Required]
    public int CreatedBy { get; set; }

    [Required]
    public required string Topic { get; set; }
}