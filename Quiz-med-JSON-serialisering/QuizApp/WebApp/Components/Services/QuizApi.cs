using System.Reflection;
namespace WebApp.Components.Services;

[AttributeUsage(AttributeTargets.All, Inherited = false, AllowMultiple = false)]
public sealed class ValueAttribute(string value) : Attribute
{
    public string Value { get; } = value;
}

public enum QuizCatagory
{
    Linux = 1,
    Bash = 2,
    Uncategorized = 3,
    Docker = 4,
    SQL = 5,
    CMS = 6,
    Code = 7,
    DevOps = 8,
    React = 9,
    Laravel = 10,
    Postgres = 11,
    Django = 12,
    CPanel = 13,
    NodeJs = 14,
    WordPress = 15,
    [Value("next-js")] Nextjs = 16,
    VueJS = 17,
    [Value("apache-kafka")] ApacheKafka = 18,
}
public enum QuizDifficulty
{
    Easy = 1,
    Medium = 2,
    Hard = 3,
}

public sealed class QuizApi
{
    public async Task GenerateQuiz(byte limit = 10, QuizDifficulty difficulty = QuizDifficulty.Easy, QuizCatagory category = QuizCatagory.Code)
    {
        limit = (byte)Math.Clamp((int)limit, 1, 20);
        string categoryValue = typeof(QuizCatagory).GetType().GetField(category.ToString())?.GetCustomAttribute<ValueAttribute>()?.Value ?? category.ToString();

        string url = new("https://quizapi.io/api/v1/questions" +
            "?apiKey=6omIW0bULko7yrHdQmQVLN7dVrIKfUCNd5XUXchG" +
            $"&limit={limit}" +
            $"&difficulty={difficulty}" +
            $"&category={categoryValue}");


        using (HttpClient client = new())
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();
                Console.WriteLine(await response.Content.ReadAsStringAsync());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
