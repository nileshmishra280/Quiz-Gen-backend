using System.ComponentModel.DataAnnotations;

namespace Quizee.Api.Models;

public class HostedQuizLeaderboard
{
    public int Id { get; set; }

    [Required]
    public required string RoomId { get; set; }

    [Required]
    public DateTime QuizDateTime { get; set; }

    [Required]
    public required string HostName { get; set; }

    [Required]
    // Stores participants and scores in JSON format: [{"name":"John","score":95},{"name":"Jane","score":85}]
    public required string ParticipantScores { get; set; }

    [Required]
    public decimal AverageScore { get; set; }

    [Required]
    public int TotalParticipants { get; set; }

    [Required]
    public required string QuizDuration { get; set; }
}