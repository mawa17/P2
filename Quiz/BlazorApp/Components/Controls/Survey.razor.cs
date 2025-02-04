using BlazorApp.Data.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Components.Controls;

public partial class Survey
{
    [Parameter, EditorRequired]
    public SurveyModel SurveyData { get; set; } = default!;

    [SupplyParameterFromForm]
    private Question[]? SurveyAnwsers { get; set; } = { };
}
