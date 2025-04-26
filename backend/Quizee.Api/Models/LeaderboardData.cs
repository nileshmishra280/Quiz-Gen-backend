using System.ComponentModel.DataAnnotations;

namespace Quizee.Api.Models
{
    public class LeaderboardData
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public required string RoomId { get; set; } // 'required' modifier for C# 11+

        [Required]
        public required DateTime QuizDateTime { get; set; }

        [Required]
        public required string HostName { get; set; }

        [Required]
        public required string QuizName { get; set; }

        [Required]
        public required string ParticipantScores { get; set; } // Stored as JSON string

        [Required]
        public required double AverageScore { get; set; }

        [Required]
        public required int TotalParticipants { get; set; }

        [Required]
        public required string QuizDuration { get; set; }
    }
}