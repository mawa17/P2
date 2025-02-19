namespace BlazorApp.Data.Models;
public sealed record SurveyModel(string Title, params QuestionModel[] Questions);