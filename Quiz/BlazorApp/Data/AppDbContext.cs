using BlazorApp.Data.Models;
using Microsoft.EntityFrameworkCore;
namespace BlazorApp.Data;

public sealed class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<AnswerModel> AnswersTable { get; set; } = null!;
}

public class AppDbContextService
{
    private readonly AppDbContext _context;

    public AppDbContextService(AppDbContext context)
    {
        _context = context;
    }

    public void AddAnswer(AnswerModel answer)
    {

        _context.AnswersTable.Add(answer);
        _context.SaveChanges();
#if DEBUG
        Console.WriteLine("DB SAVE");
#endif
    }
}
