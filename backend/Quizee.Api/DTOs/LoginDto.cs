using System.ComponentModel.DataAnnotations;

namespace Quizee.Api.DTOs;

public class LoginDto
{
    [Required]
    [EmailAddress]
    public required string Email { get; set; }
    
    [Required]
    public required string Password { get; set; }
}