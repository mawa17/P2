namespace BlazorApp.Data.Models;
public sealed record Question(string Text, params string[] Options);