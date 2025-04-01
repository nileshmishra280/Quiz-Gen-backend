using Microsoft.EntityFrameworkCore;
using Quizee.Api.Models;

namespace Quizee.Api.Data;

public class QuizeeContext : DbContext
{
    public QuizeeContext(DbContextOptions<QuizeeContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Quiz> Quizzes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasIndex(u => u.Email)
            .IsUnique();
    }
}