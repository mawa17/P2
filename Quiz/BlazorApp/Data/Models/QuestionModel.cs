namespace BlazorApp.Data.Models;
public sealed record QuestionModel(string Text, params string[] Options);