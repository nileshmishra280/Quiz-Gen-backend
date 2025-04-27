using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Quizee.Api.Data;
using Quizee.Api.DTOs;
using Quizee.Api.Models;
using Microsoft.Extensions.Logging; // Added for logging
using System.Threading.Tasks;

namespace Quizee.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize] // Ensures the request is authenticated
    public class LeaderboardController : ControllerBase
    {
        private readonly QuizeeContext _context;
        private readonly ILogger<LeaderboardController> _logger; // Added logger field

        public LeaderboardController(QuizeeContext context, ILogger<LeaderboardController> logger) // Added logger to constructor
        {
            _context = context;
            _logger = logger;
            _logger.LogInformation("LeaderboardController initialized with QuizeeContext."); // Moved logger inside constructor
        }

        [HttpPost("save-leaderboard")]
        public async Task<IActionResult> SaveLeaderboard([FromBody] LeaderboardDto leaderboardDto)
        {
            _logger.LogInformation("I am in controller"); // Added requested log message

            if (leaderboardDto == null)
            {
                _logger.LogWarning("Received null leaderboard data");
                return BadRequest(new { success = false, message = "Invalid leaderboard data" });
            }

            // Validate required fields (optional, but recommended with 'required' modifier)
            if (string.IsNullOrEmpty(leaderboardDto.RoomId) || string.IsNullOrEmpty(leaderboardDto.HostName) ||
                string.IsNullOrEmpty(leaderboardDto.QuizName) || string.IsNullOrEmpty(leaderboardDto.ParticipantScores) ||
                string.IsNullOrEmpty(leaderboardDto.QuizDuration))
            {
                _logger.LogWarning("Missing required fields in leaderboard data: RoomId={RoomId}, HostName={HostName}, QuizName={QuizName}, ParticipantScores={ParticipantScores}, QuizDuration={QuizDuration}",
                    leaderboardDto.RoomId, leaderboardDto.HostName, leaderboardDto.QuizName, leaderboardDto.ParticipantScores, leaderboardDto.QuizDuration);
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

            try
            {
                _context.LeaderboardData.Add(leaderboardData);
                await _context.SaveChangesAsync();
                _logger.LogInformation("Leaderboard saved successfully for RoomId: {RoomId}, Id: {Id}", leaderboardData.RoomId, leaderboardData.Id);
                return Ok(new { success = true, message = "Leaderboard saved successfully", id = leaderboardData.Id });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error saving leaderboard for RoomId: {RoomId}", leaderboardData.RoomId);
                return StatusCode(500, new { success = false, message = "Internal server error" });
            }
        }
    }
}