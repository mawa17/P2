using BlazorApp.Data.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Components.Controls;

public partial class Survey
{
    private int index;
    public float Progress => ((float)(SurvayPage+1) / (float)SurveyModel.Questions.Count) * 100;
    private void Submit(QuestionModel question)
    {
        anwsers.Add(question);
        NextPage();
    }

    [Parameter, EditorRequired]
    public SurveyModel SurveyModel { get; set; } = default!;

    [Parameter]
    public string? SurvayNotice { get; set; }

    [Parameter]
    public EventCallback<SurveyModel> OnComplete { get; set; }

    private bool IsSurvayNoticeAccepcted;
    private int SurvayPage;
    private void NextPage()
    {
        this.SurvayPage = Math.Clamp(++SurvayPage, 0, SurveyModel.Questions.Count);

        if (SurvayPage == SurveyModel.Questions.Count)
        {
            OnComplete.InvokeAsync(new(this.SurveyModel.Title, anwsers.ToArray()));
        }
    }
    private void PrevPage()
    {
        this.SurvayPage = Math.Clamp(--SurvayPage, 0, SurveyModel.Questions.Count);
    }



    private List<QuestionModel> anwsers = new();
}
