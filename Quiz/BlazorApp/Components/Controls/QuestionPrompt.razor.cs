using BlazorApp.Data.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Components.Controls;

public partial class QuestionPrompt : ComponentBase
{
    [Parameter, EditorRequired]
    public QuestionModel Question { get; init; } = default!;

    [Parameter]
    public QuestionState State { get; 
        set; }

    [Parameter]
    public EventCallback<QuestionState> OnStateChange { get; set; }

    [Parameter]
    public EventCallback<QuestionModel> OnSubmit { get; set; }
    private bool CanSubmit => !EqualityComparer<QuestionState>.Default.Equals(State, default) ? State.answers.Count == Question.Options.Length : false;

    private readonly string guid = Guid.NewGuid().ToString();
    private int x, y; /*For Visualisation*/

    protected override void OnInitialized()
    {
        if (Question == null) throw new NullReferenceException($"{nameof(Question)} cannot be null!");
        bool isDefault = EqualityComparer<QuestionState>.Default.Equals(State, default);
        if (isDefault) 
            State = new(Question.Options.Length);
    }

    private void OnSubmitEvent()
    {
        if (!CanSubmit) return;
        OnSubmit.InvokeAsync(new(Question.Text, State.answers.Values.ToArray()));
#if DEBUG
        Console.WriteLine("INVOKE OnSubmit");
#endif
    }

    private void ToggleColumnSelection(int x, int y)
    {
        string selection = $"SET{x}{y}";
        string value = Question.Options[y];
#if DEBUG
        Console.WriteLine($"{value} ({x})");
#endif
        if (State.grid[y] == selection)
        {
            State.grid[y] = null;
            UpdateAnwsers(value, x, true);
        }
        else
        {
            State.grid[y] = selection;
            UpdateAnwsers(value, x);
        }
        this.x = x;
        this.y = y;
        StateHasChanged();
    }

    private void UpdateAnwsers(string value, int index, bool onlyRemove = false)
    {
        var keys = State.answers.Where(x => x.Value == value);
        bool hasKey = keys.Count() > 0;
        if (hasKey)
        {
            State.answers.Remove(keys.ElementAt(0).Key);
            State.answers.Remove(index);
        }
        if (!onlyRemove) State.answers[index] = value;
        OnStateChange.InvokeAsync(State);
#if DEBUG
        Console.WriteLine("INVOKE OnStateChange");
#endif
    }
}

public readonly struct QuestionState(int gridSize)
{
    public readonly SortedDictionary<int, string> answers = new();
    public readonly string?[] grid = new string?[gridSize];
    public QuestionState() : this(default) { }
}

