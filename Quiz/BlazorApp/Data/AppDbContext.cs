using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using BlazorApp.Components.Pages;
using BlazorApp.Data.Models;
using Microsoft.EntityFrameworkCore;
namespace BlazorApp.Data;

public sealed class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<AnswerModel> AnswersTable { get; set; } = null!;
    public DbSet<SurveyAnswerView> AnswerView { get; set; } = null!;
    public DbSet<LoginModel> Login { get; set; } = null!;
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        // Seeding default data for the Login table
        modelBuilder.Entity<LoginModel>().HasData(
            new LoginModel { Id = 1, Username = "admin", Password = "Pa$$w0rd!" }
        );

        // Define the JsonSerializerOptions separately before using them in HasConversion
        var jsonOptions = new JsonSerializerOptions
        {
            Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
        };

        // Configure the QuestionModel to serialize List<string> into a JSON string
        modelBuilder.Entity<QuestionModel>()
            .Property(q => q.Options)
            .HasConversion(
                // Serialize the List<string> to JSON with the defined options
                v => JsonSerializer.Serialize(v, jsonOptions),
                // Deserialize the JSON back into List<string> with the defined options
                v => JsonSerializer.Deserialize<List<string>>(v, jsonOptions) ?? new List<string>()
            );

        modelBuilder.Entity<SurveyAnswerView>(entity =>
        {
            // Telling EF Core that this is a view and not a table
            entity.ToView("SurveyAnswerView"); // Name of the view in the database
            entity.HasNoKey(); // Views don't have primary keys
        });

        base.OnModelCreating(modelBuilder);
    }
}

public class AppDbContextService(AppDbContext context)
{
    private readonly AppDbContext _context = context;
    public LoginModel? Login => this._context.Login.AsNoTracking().FirstOrDefault();
    public IQueryable<AnswerModel> Entity => this._context.AnswersTable
        .Include(x => x.Survey)
        .Include(y => y.Survey.Questions)
        .AsNoTracking();

    public void UpdateLogin(string username, string password)
    {
        var loginEntry = _context.Login.Find(1);
        if (loginEntry != null)
        {
            loginEntry.Username = username;
            loginEntry.Password = password;
        }
        else _context.Login.Add(new() { Username = username, Password = password});
        _context.SaveChanges();
    }
    public async Task AddAnswerAsync(AnswerModel answer)
    {
        _context.AnswersTable.Add(answer);
        await _context.SaveChangesAsync();
#if DEBUG
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("DB SAVE");
        Console.ResetColor();
        foreach (var item in answer.Survey.Questions.SelectMany(x => x.Options))
        {
            Console.WriteLine(item);
        }
#endif
    }
}

public sealed class LoginModel
{
    public int Id { get; set; }
    public string Username { get; set; } = null!;
    public string Password { get; set; } = null!;
}

public class SurveyAnswerView
{
    public string SurveyTitle { get; set; } = null!;
    public string QuestionText { get; set; } = null!;
    public string QuestionOptions { get; set; } = null!;
    public string AnswerName { get; set; } = null!;
    public List<int> AnswerPoints { get; set; } = new List<int>();
}