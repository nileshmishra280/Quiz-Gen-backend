using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Quizee.Api.Data;
using Quizee.Api.DTOs;
using Quizee.Api.Models;

namespace Quizee.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class QuizController : ControllerBase
{
    private readonly ILogger<QuizController> _logger;
    private readonly QuizeeContext _context;

    public QuizController(
        ILogger<QuizController> logger,
        QuizeeContext context)
    {
        _logger = logger;
        _context = context;
    }

    [HttpPost("submit")]
    public async Task<ActionResult<object>> SubmitQuiz([FromBody] QuizDto quizDto)
    {
        try
        {
            _logger.LogInformation("Attempting to submit quiz for user: {UserId}", quizDto.UserId);

            var quiz = new Quiz
            {
                Title = quizDto.Title,
                PercentageScored = quizDto.PercentageScored,
                TimeTaken = quizDto.TimeTaken,
                CreationDate = DateTime.UtcNow,
                UserId = int.Parse(quizDto.UserId)
            };

            await _context.Quizzes.AddAsync(quiz);
            await _context.SaveChangesAsync();

            _logger.LogInformation("Quiz submitted successfully for user: {UserId}", quizDto.UserId);

            return Ok(new
            {
                success = true,
                message = "Quiz submitted successfully",
                quizId = quiz.Id
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error submitting quiz");
            return StatusCode(500, new { success = false, message = $"Failed to submit quiz: {ex.Message}" });
        }
    }

    [HttpGet("getUserQuiz/{userId}")]
    public async Task<ActionResult<object>> GetUserQuiz(int userId)
    {
        _logger.LogInformation("Getting user quiz for userId: {UserId}", userId);
        try
        {
            var quizzes = await _context.Quizzes
                .Where(q => q.UserId == userId)
                .OrderByDescending(q => q.CreationDate)
                .ToListAsync();

            _logger.LogInformation("Found {Count} quizzes for user {UserId}", quizzes.Count, userId);
            
            return Ok(new
            {
                success = true,
                quizzes = quizzes
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving quizzes for user: {UserId}", userId);
            return StatusCode(500, new { success = false, message = $"Failed to retrieve quizzes: {ex.Message}" });
        }
    }
}