using BlazorApp.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace BlazorApp.Data;


file static class Config
{
    internal const string server = "10.0.1.222";
    internal const string database = "quizdb";
    internal const string user = "Administrator";
    internal const string pass = "1";
}

public sealed class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<AnswerModel> AnswersTable { get; set; } = null!;
}
