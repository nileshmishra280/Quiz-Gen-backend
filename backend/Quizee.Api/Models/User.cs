using System;
using System.ComponentModel.DataAnnotations;

namespace Quizee.Api.Models;

public class User
{
    public int Id { get; set; }
    
    [Required]
    public required string Name { get; set; }
    
    [Required]
    [EmailAddress]
    public required string Email { get; set; }
    
    [Required]
    public required string Password { get; set; }
}
