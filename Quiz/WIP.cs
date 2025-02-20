using System.ComponentModel.DataAnnotations;
using BlazorApp.Data.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Components.Controls;


public interface IComponentState<TState> where TState : struct
{
    TState State { get; set; }
}

public partial class QuestionPrompt : ComponentBase, IComponentState<QuestionState>
{
    private readonly string guid = Guid.NewGuid().ToString();
    private int x, y; /*For Visualisation*/

    [Parameter, EditorRequired]
    public QuestionModel Question { get; init; } = default!;

    [Parameter]
    public EventCallback<QuestionModel> OnSubmit { get; set; }

    [Parameter]
    public QuestionState State { get; set; }

    private bool CanSubmit => State.Answers.Count == Question.Options.Length;

    protected override void OnInitialized()
    {
        if (State.Equals(default)) State = new(Question.Options.Length);
    }

    private void OnSubmitData()
    {
        if (!CanSubmit) return;
        OnSubmit.InvokeAsync(new(Question.Text, State.Answers.Values.ToArray()));
#if DEBUG
        Console.WriteLine("SUBMIT");
#endif
    }

    private void ToggleColumnSelection(int x, int y)
    {
        string selection = $"SET{x}{y}";
        string value = Question.Options[y];
#if DEBUG
        Console.WriteLine($"{value} ({x})");
#endif
        if (State.Grid[y] == selection)
        {
            State.Grid[y] = null;
            State.UpdateAnswers(value, x, true);
        }
        else
        {
            State.Grid[y] = selection;
            State.UpdateAnswers(value, x);
        }
        this.x = x;
        this.y = y;
        StateHasChanged();
    }

}

public readonly struct QuestionState(int gridSize)
{
    public readonly SortedDictionary<int, string> Answers = new();
    public readonly string?[] Grid = new string[gridSize];
    public void UpdateAnswers(string value, int index, bool onlyRemove = false)
    {
        var keys = Answers.Where(x => x.Value == value);
        bool hasKey = keys.Count() > 0;

        if (hasKey)
        {
            Answers.Remove(keys.ElementAt(0).Key);
            Answers.Remove(index);
        }

        if (!onlyRemove) Answers[index] = value;
    }
}