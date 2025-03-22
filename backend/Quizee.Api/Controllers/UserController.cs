using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Quizee.Api.Data;
using Quizee.Api.DTOs;
using Quizee.Api.Models;
using Quizee.Api.Services;
using System.ComponentModel.DataAnnotations;

namespace Quizee.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly ILogger<UserController> _logger;
    private readonly QuizeeContext _context;
    private readonly JwtService _jwtService;

    public UserController(
        ILogger<UserController> logger,
        QuizeeContext context,
        JwtService jwtService)
    {
        _logger = logger;
        _context = context;
        _jwtService = jwtService;
    }

    [HttpPost("register")]
    public async Task<ActionResult<object>> Register([FromBody] UserDto userDto)
    {
        try
        {
            _logger.LogInformation("Attempting to register user: {Email}", userDto.Email);

            // Validate required fields
            if (string.IsNullOrEmpty(userDto.Name) || string.IsNullOrEmpty(userDto.Email) || string.IsNullOrEmpty(userDto.Password))
            {
                _logger.LogWarning("Invalid input for registration: Name, Email, or Password is empty");
                return BadRequest(new { success = false, message = "Name, email, and password are required" });
            }

            // Validate email format
            var emailValidator = new EmailAddressAttribute();
            if (!emailValidator.IsValid(userDto.Email))
            {
                _logger.LogWarning("Invalid email format: {Email}", userDto.Email);
                return BadRequest(new { success = false, message = "Invalid email format" });
            }

            // Check if email already exists
            if (await _context.Users.AnyAsync(u => u.Email == userDto.Email))
            {
                _logger.LogWarning("Email already exists: {Email}", userDto.Email);
                return BadRequest(new { success = false, message = "Email already registered" });
            }

            // Hash the password
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(userDto.Password);

            // Create new user
            var user = new User
            {
                Name = userDto.Name,
                Email = userDto.Email,
                Password = hashedPassword
            };

            // Save user to database
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            _logger.LogInformation("User registered successfully: {Email}", userDto.Email);

            // Generate JWT token
            var token = _jwtService.GenerateToken(user);

            return Ok(new
            {
                success = true,
                token = token,
                user = new
                {
                    name = user.Name,
                    email = user.Email
                }
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error registering user: {Email}", userDto.Email);
            return StatusCode(500, new { success = false, message = $"Failed to register user: {ex.Message}" });
        }
    }

    [HttpPost("login")]
    public async Task<ActionResult<object>> Login([FromBody] LoginDto loginDto)
    {
        try
        {
            _logger.LogInformation("Attempting to login user: {Email}", loginDto.Email);

            // Validate required fields
            if (string.IsNullOrEmpty(loginDto.Email) || string.IsNullOrEmpty(loginDto.Password))
            {
                _logger.LogWarning("Invalid input for login: Email or Password is empty");
                return BadRequest(new { success = false, message = "Email and password are required" });
            }

            // Find user by email
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == loginDto.Email);

            if (user == null)
            {
                _logger.LogWarning("User not found for email: {Email}", loginDto.Email);
                return Unauthorized(new { success = false, message = "Invalid credentials" });
            }

            // Verify password
            if (!BCrypt.Net.BCrypt.Verify(loginDto.Password, user.Password))
            {
                _logger.LogWarning("Invalid password for email: {Email}", loginDto.Email);
                return Unauthorized(new { success = false, message = "Invalid credentials" });
            }

            // Generate JWT token
            var token = _jwtService.GenerateToken(user);

            _logger.LogInformation("User logged in successfully: {Email}", loginDto.Email);

            return Ok(new
            {
                success = true,
                token = token,
                user = new
                {
                    name = user.Name,
                    email = user.Email
                }
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error during login for email: {Email}", loginDto.Email);
            return StatusCode(500, new { success = false, message = $"Failed to login: {ex.Message}" });
        }
    }
}