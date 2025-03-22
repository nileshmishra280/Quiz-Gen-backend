using System.ComponentModel.DataAnnotations;

namespace Quizee.Api.DTOs;

public class UserDto
{
    [Required]
    public required string Name { get; set; }
    
    [Required]
    [EmailAddress]
    public required string Email { get; set; }
    
    [Required]
    public required string Password { get; set; }
}