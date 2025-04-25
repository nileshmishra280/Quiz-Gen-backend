using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Quizee.Api.Data;
using Quizee.Api.Models;

namespace Quizee.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class QuestionController : ControllerBase
{
    private readonly ILogger<QuestionController> _logger;
    private readonly QuizeeContext _context;

    public QuestionController(ILogger<QuestionController> logger, QuizeeContext context)
    {
        _logger = logger;
        _context = context;
    }

    [HttpPost("save")]
    public async Task<ActionResult<object>> SaveQuestions([FromBody] List<Question> questions)
    {
        try
        {
            foreach (var question in questions)
            {
                question.CreatedDate = DateTime.UtcNow;
            }

            await _context.Questions.AddRangeAsync(questions);
            await _context.SaveChangesAsync();

            return Ok(new { success = true, message = "Questions saved successfully" });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error saving questions");
            return StatusCode(500, new { success = false, message = ex.Message });
        }
    }

    [HttpGet("user/{userId}")]
    public async Task<ActionResult<object>> GetUserQuestions(int userId)
    {
        try
        {
            var questions = await _context.Questions
                .Where(q => q.CreatedBy == userId)
                .OrderByDescending(q => q.CreatedDate)
                .ToListAsync();

            return Ok(new { success = true, questions });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving questions");
            return StatusCode(500, new { success = false, message = ex.Message });
        }
    }

   
}