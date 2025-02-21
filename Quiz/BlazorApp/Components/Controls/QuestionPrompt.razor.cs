using BlazorApp.Data.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Components.Controls;

public partial class QuestionPrompt : ComponentBase
{
    [Parameter, EditorRequired]
    public QuestionModel Question { get; init; } = default!;

    [Parameter]
    public EventCallback<QuestionModel> OnSubmit { get; set; }

    [Parameter]
    public EventCallback OnStateChange { get; set; }

    private readonly string guid = Guid.NewGuid().ToString();
    private SortedDictionary<int, string> anwsers = new();
    private string?[] grid = { };
    private int x, y; /*For Visualisation*/
    private bool CanSubmit => anwsers.Count == Question.Options.Length;
    protected override void OnInitialized()
    {
        grid = new string[Question.Options.Length];
    }

    protected override void OnParametersSet()
    {
        Console.WriteLine($"New QuestionPrompt Instance: {guid}");
    }

    private void OnSubmitEvent()
    {
        if (!CanSubmit) return;
        OnSubmit.InvokeAsync(new(Question.Text, anwsers.Values.ToArray()));
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
        if (grid[y] == selection)
        {
            grid[y] = null;
            UpdateAnwsers(value, x, true);
        }
        else
        {
            grid[y] = selection;
            UpdateAnwsers(value, x);
        }
        this.x = x;
        this.y = y;
        StateHasChanged();
    }

    private void UpdateAnwsers(string value, int index, bool onlyRemove = false)
    {
        var keys = anwsers.Where(x => x.Value == value);
        bool hasKey = keys.Count() > 0;
        if (hasKey)
        {
            anwsers.Remove(keys.ElementAt(0).Key);
            anwsers.Remove(index);
        }
        if (!onlyRemove) anwsers[index] = value;
        OnStateChange.InvokeAsync();
    }
}


