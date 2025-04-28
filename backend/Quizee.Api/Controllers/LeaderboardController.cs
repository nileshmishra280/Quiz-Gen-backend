using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Quizee.Api.Data;
using Quizee.Api.DTOs;
using Quizee.Api.Models;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Quizee.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   // [Authorize]
    public class LeaderboardController : ControllerBase
    {
        private readonly QuizeeContext _context;
        private readonly ILogger<LeaderboardController> _logger;

        public LeaderboardController(QuizeeContext context, ILogger<LeaderboardController> logger)
        {
            _context = context;
            _logger = logger;
            _logger.LogInformation("LeaderboardController initialized with QuizeeContext.");
        }

        [HttpPost("save-leaderboard")]
        public async Task<IActionResult> SaveLeaderboard([FromBody] LeaderboardDto leaderboardDto)
        {
            _logger.LogInformation("Received request to save leaderboard");

            if (leaderboardDto == null)
            {
                _logger.LogWarning("Received null leaderboard data");
                return BadRequest(new { success = false, message = "Invalid leaderboard data" });
            }

            if (string.IsNullOrEmpty(leaderboardDto.RoomId) || string.IsNullOrEmpty(leaderboardDto.HostName) ||
                string.IsNullOrEmpty(leaderboardDto.QuizName) || string.IsNullOrEmpty(leaderboardDto.ParticipantScores) ||
                string.IsNullOrEmpty(leaderboardDto.QuizDuration))
            {
                _logger.LogWarning("Missing required fields in leaderboard data");
                return BadRequest(new { success = false, message = "Missing required fields" });
            }

            var leaderboardData = new LeaderboardData
            {
                Id = leaderboardDto.Id,
                RoomId = leaderboardDto.RoomId,
                QuizDateTime = leaderboardDto.QuizDateTime,
                HostName = leaderboardDto.HostName,
                QuizName = leaderboardDto.QuizName,
                ParticipantScores = leaderboardDto.ParticipantScores,
                AverageScore = leaderboardDto.AverageScore,
                TotalParticipants = leaderboardDto.TotalParticipants,
                QuizDuration = leaderboardDto.QuizDuration
            };

            try
            {
                _context.LeaderboardData.Add(leaderboardData);
                await _context.SaveChangesAsync();
                _logger.LogInformation("Leaderboard saved successfully for RoomId: {RoomId}", leaderboardData.RoomId);
                return Ok(new { success = true, message = "Leaderboard saved successfully", id = leaderboardData.Id });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error saving leaderboard for RoomId: {RoomId}", leaderboardData.RoomId);
                return StatusCode(500, new { success = false, message = "Internal server error" });
            }
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAllLeaderboards()
        {
            _logger.LogInformation("Received request to fetch all leaderboard data");
            try
            {
                // Get all leaderboard entries including related data
                var leaderboards = await _context.LeaderboardData
                    .AsNoTracking() // For better read performance
                    .OrderByDescending(l => l.QuizDateTime) // Most recent first
                    .Select(l => new
                    {
                        l.Id,
                        l.RoomId,
                        l.QuizDateTime,
                        l.HostName,
                        l.QuizName,
                        ParticipantScores = l.ParticipantScores, // This is your JSON string
                        l.AverageScore,
                        l.TotalParticipants,
                        l.QuizDuration
                    })
                    .ToListAsync();

                _logger.LogInformation($"Successfully retrieved {leaderboards.Count} leaderboard entries");

                if (!leaderboards.Any())
                {
                    return Ok(new { 
                        success = true, 
                        message = "No leaderboard data found", 
                        data = new List<object>() 
                    });
                }

                return Ok(new
                {
                    success = true,
                    message = "Leaderboard data retrieved successfully",
                    totalRecords = leaderboards.Count,
                    data = leaderboards
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching leaderboard data");
                return StatusCode(500, new
                {
                    success = false,
                    message = "An error occurred while fetching leaderboard data",
                    error = ex.Message
                });
            }
        }
    }
}