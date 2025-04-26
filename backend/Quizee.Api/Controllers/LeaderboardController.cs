using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Quizee.Api.Data;
using Quizee.Api.DTOs;
using Quizee.Api.Models;
using System.Threading.Tasks;

namespace Quizee.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize] // Ensures the request is authenticated
    public class LeaderboardController : ControllerBase
    {
        private readonly QuizeeContext _context;

        public LeaderboardController(QuizeeContext context)
        {
            _context = context;
        }

        [HttpPost("save-leaderboard")]
        public async Task<IActionResult> SaveLeaderboard([FromBody] LeaderboardDto leaderboardDto)
        {
            if (leaderboardDto == null)
            {
                return BadRequest(new { success = false, message = "Invalid leaderboard data" });
            }

            // Validate required fields (optional, but recommended with 'required' modifier)
            if (string.IsNullOrEmpty(leaderboardDto.RoomId) || string.IsNullOrEmpty(leaderboardDto.HostName) ||
                string.IsNullOrEmpty(leaderboardDto.QuizName) || string.IsNullOrEmpty(leaderboardDto.ParticipantScores) ||
                string.IsNullOrEmpty(leaderboardDto.QuizDuration))
            {
                return BadRequest(new { success = false, message = "Missing required fields" });
            }

            var leaderboardData = new LeaderboardData
            {
                Id = leaderboardDto.Id,
                RoomId = leaderboardDto.RoomId,
                QuizDateTime = leaderboardDto.QuizDateTime,
                HostName = leaderboardDto.HostName,
                QuizName = leaderboardDto.QuizName,
                ParticipantScores = leaderboardDto.ParticipantScores, // Stored as JSON string
                AverageScore = leaderboardDto.AverageScore,
                TotalParticipants = leaderboardDto.TotalParticipants,
                QuizDuration = leaderboardDto.QuizDuration
            };

            _context.LeaderboardData.Add(leaderboardData);
            await _context.SaveChangesAsync();

            return Ok(new { success = true, message = "Leaderboard saved successfully", id = leaderboardData.Id });
        }
    }
}