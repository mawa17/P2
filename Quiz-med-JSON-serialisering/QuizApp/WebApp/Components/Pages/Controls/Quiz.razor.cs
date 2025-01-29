using Microsoft.AspNetCore.Components;
using WebApp.Components.Models;
using WebApp.Components.Services;

namespace WebApp.Components.Pages.Controls;

public partial class Quiz
{
    private Question[]? Questions;
    private int page = 0;
    private bool AnwserGiven;
    private bool IsAnwserCorrect;

    [Parameter, EditorRequired]
    public string? QuizFile { get; set; }

    [Inject]
    public HttpClient Http { get; init; }

    [Inject]
    public ILogger<Quiz> Logger { get; init; }

    protected override async Task OnInitializedAsync()
    {
        try
        {
            Questions = await Http.GetFromJsonAsync<Question[]>(QuizFile);
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, ex.Message);
        }
    }
    private void NextQuestion()
    {
        page = Math.Clamp(++page, 0, (Questions?.Length - 1) ?? 0);
        AnwserGiven = false;
        IsAnwserCorrect = false;
    }
    private void ValidateAnswer(Question question, int anwser)
    {
        AnwserGiven = true;
        IsAnwserCorrect = question?.AnwserIndex == anwser;
    #if DEBUG
            Console.WriteLine($"{question.AnwserIndex} -> {anwser}");
    #endif
    }












}