using System.ComponentModel.DataAnnotations;

namespace Quizee.Api.DTOs;

public class HostedQuizLeaderboardDto
{
    [Required]
    public required string RoomId { get; set; }

    public DateTime QuizDateTime { get; set; }

    [Required]
    public required string HostName { get; set; }

    [Required]
    public required List<ParticipantScore> ParticipantScores { get; set; }

    public decimal AverageScore { get; set; }

    public int TotalParticipants { get; set; }

    [Required]
    public required string QuizDuration { get; set; }
}

public class ParticipantScore
{
    [Required]
    public required string Name { get; set; }

    public int Score { get; set; }
}

// For creating new hosted quiz leaderboard
public class CreateHostedQuizLeaderboardDto
{
    [Required]
    public required string RoomId { get; set; }

    [Required]
    public required string HostName { get; set; }

    [Required]
    public required List<ParticipantScore> ParticipantScores { get; set; }

    [Required]
    public required string QuizDuration { get; set; }
}

// For updating hosted quiz leaderboard
public class UpdateHostedQuizLeaderboardDto
{
    [Required]
    public required List<ParticipantScore> ParticipantScores { get; set; }

    [Required]
    public required string QuizDuration { get; set; }
}