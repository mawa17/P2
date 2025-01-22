using System.Text.Json.Serialization;

namespace WebApp.Components.Models;

public sealed record Question
{
    [JsonPropertyName("q"), JsonPropertyOrder(1)]
    public required string QuestionText { get; init; }

    [JsonPropertyName("i"), JsonPropertyOrder(2)]
    public required int AnwserIndex { get; init; }

    [JsonPropertyName("a"), JsonPropertyOrder(3)]
    public required string[] AnswerOptions { get; set; }

    [JsonPropertyName("h"), JsonPropertyOrder(4)]
    public required string HintText { get; set; }
}
