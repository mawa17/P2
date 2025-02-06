using BlazorApp.Data.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Components.Controls;

public partial class Survey
{
    [Parameter, EditorRequired]
    public SurveyModel SurveyData { get; set; } = default!;

    [SupplyParameterFromForm]
    private QuestionModel[]? SurveyAnswers { get; set; } = { };

    private uint SurvayPage;
    private void NextPage()
    {
        this.SurvayPage = (uint)Math.Clamp(++SurvayPage, 0, SurveyData.Questions.Length);
    }
    private void PrevPage()
    {
        this.SurvayPage = (uint)Math.Clamp(--SurvayPage, 0, SurveyData.Questions.Length);
    }
}
