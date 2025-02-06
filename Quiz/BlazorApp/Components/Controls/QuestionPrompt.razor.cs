using BlazorApp.Data.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Components.Controls;

public partial class QuestionPrompt
{

    [Parameter, EditorRequired]
    public QuestionModel Question { get; set; } = default!;
}
