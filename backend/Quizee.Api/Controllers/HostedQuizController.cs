using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Quizee.Api.Data;
using Quizee.Api.Models;
using Quizee.Api.DTOs;
using System.Text.Json;

namespace Quizee.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HostedQuizController : ControllerBase
{
    private readonly ILogger<HostedQuizController> _logger;
    private readonly QuizeeContext _context;

    public HostedQuizController(ILogger<HostedQuizController> logger, QuizeeContext context)
    {
        _logger = logger;
        _context = context;
    }

    [HttpPost("save")]
    public async Task<ActionResult<object>> SaveHostedQuiz([FromBody] CreateHostedQuizLeaderboardDto dto)
    {
        try
        {
            var leaderboard = new HostedQuizLeaderboard
            {
                RoomId = dto.RoomId,
                QuizDateTime = DateTime.UtcNow,
                HostName = dto.HostName,
                ParticipantScores = JsonSerializer.Serialize(dto.ParticipantScores.OrderByDescending(p => p.Score)),
                AverageScore = (decimal)dto.ParticipantScores.Average(p => p.Score),
                TotalParticipants = dto.ParticipantScores.Count,
                QuizDuration = dto.QuizDuration
            };

            await _context.HostedQuizLeaderboards.AddAsync(leaderboard);
            await _context.SaveChangesAsync();

            return Ok(new { success = true, message = "Quiz results saved successfully" });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error saving hosted quiz results");
            return StatusCode(500, new { success = false, message = ex.Message });
        }
    }

    [HttpGet("host/{hostName}")]
    public async Task<ActionResult<object>> GetHostQuizzes(string hostName)
    {
        try
        {
            var quizzes = await _context.HostedQuizLeaderboards
                .Where(q => q.HostName == hostName)
                .OrderByDescending(q => q.QuizDateTime)
                .ToListAsync();

            var results = quizzes.Select(q => new HostedQuizLeaderboardDto
            {
                RoomId = q.RoomId,
                QuizDateTime = q.QuizDateTime,
                HostName = q.HostName,
                ParticipantScores = JsonSerializer.Deserialize<List<ParticipantScore>>(q.ParticipantScores) ?? new List<ParticipantScore>(),
                AverageScore = q.AverageScore,
                TotalParticipants = q.TotalParticipants,
                QuizDuration = q.QuizDuration
            });

            return Ok(new { success = true, quizzes = results });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving hosted quizzes");
            return StatusCode(500, new { success = false, message = ex.Message });
        }
    }

    [HttpGet("{roomId}")]
    public async Task<ActionResult<object>> GetQuizDetails(string roomId)
    {
        try
        {
            var quiz = await _context.HostedQuizLeaderboards
                .FirstOrDefaultAsync(q => q.RoomId == roomId);

            if (quiz == null)
            {
                return NotFound(new { success = false, message = "Quiz not found" });
            }

            var result = new HostedQuizLeaderboardDto
            {
                RoomId = quiz.RoomId,
                QuizDateTime = quiz.QuizDateTime,
                HostName = quiz.HostName,
                ParticipantScores = JsonSerializer.Deserialize<List<ParticipantScore>>(quiz.ParticipantScores) ?? new List<ParticipantScore>(),
                AverageScore = quiz.AverageScore,
                TotalParticipants = quiz.TotalParticipants,
                QuizDuration = quiz.QuizDuration
            };

            return Ok(new { success = true, quiz = result });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving quiz details");
            return StatusCode(500, new { success = false, message = ex.Message });
        }
    }

    [HttpGet("leaderboard/{roomId}")]
    public async Task<ActionResult<object>> GetQuizLeaderboard(string roomId)
    {
        try
        {
            var quiz = await _context.HostedQuizLeaderboards
                .FirstOrDefaultAsync(q => q.RoomId == roomId);

            if (quiz == null)
            {
                return NotFound(new { success = false, message = "Quiz not found" });
            }

            var participantScores = JsonSerializer.Deserialize<List<ParticipantScore>>(quiz.ParticipantScores);

            return Ok(new { 
                success = true, 
                leaderboard = participantScores,
                quizDateTime = quiz.QuizDateTime,
                averageScore = quiz.AverageScore,
                totalParticipants = quiz.TotalParticipants,
                quizDuration = quiz.QuizDuration
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving quiz leaderboard");
            return StatusCode(500, new { success = false, message = ex.Message });
        }
    }

    [HttpGet("get-all")]
    public async Task<ActionResult<object>> GetAllQuizzes()
    {
        try
        {
            var allQuizzes = await _context.HostedQuizLeaderboards
                .OrderByDescending(q => q.QuizDateTime)
                .ToListAsync();

            var results = allQuizzes.Select(q => new HostedQuizLeaderboardDto
            {
                RoomId = q.RoomId,
                QuizDateTime = q.QuizDateTime,
                HostName = q.HostName,
                ParticipantScores = JsonSerializer.Deserialize<List<ParticipantScore>>(q.ParticipantScores) ?? new List<ParticipantScore>(),
                AverageScore = q.AverageScore,
                TotalParticipants = q.TotalParticipants,
                QuizDuration = q.QuizDuration
            });

            return Ok(new { 
                success = true, 
                quizzes = results,
                totalCount = allQuizzes.Count
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving all quizzes");
            return StatusCode(500, new { success = false, message = ex.Message });
        }
    }
}