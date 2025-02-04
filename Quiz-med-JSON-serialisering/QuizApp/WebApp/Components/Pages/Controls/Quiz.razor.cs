using Microsoft.AspNetCore.Components;
using WebApp.Components.Models;
using WebApp.Components.Services;

namespace WebApp.Components.Pages.Controls;

public partial class Quiz
{
    private Question[] questions;
    private int page = 0;
    private bool AnwserGiven;
    private bool IsAnwserCorrect;

    [Parameter, EditorRequired]
    public Func<IEnumerable<Question>> Questions { get; set; } = default!;

    protected override void OnInitialized()
    {
        questions = Questions.Invoke().ToArray();
        base.OnInitialized();
    }

    [Inject]
    public HttpClient Http { get; init; }

    [Inject]
    public ILogger<Quiz> Logger { get; init; }

    private void NextQuestion()
    {
        //page = Math.Clamp(++page, 0, (Questions?.Length - 1) ?? 0);
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