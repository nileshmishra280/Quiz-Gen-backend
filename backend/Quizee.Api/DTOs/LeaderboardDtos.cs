namespace Quizee.Api.DTOs
{
    public class LeaderboardDto
    {
        public int Id { get; set; }

        public required string RoomId { get; set; } // 'required' modifier for C# 11+

        public required DateTime QuizDateTime { get; set; }

        public required string HostName { get; set; }

        public required string QuizName { get; set; }

        public required string ParticipantScores { get; set; } // JSON string

        public required double AverageScore { get; set; }

        public required int TotalParticipants { get; set; }

        public required string QuizDuration { get; set; }
    }
}