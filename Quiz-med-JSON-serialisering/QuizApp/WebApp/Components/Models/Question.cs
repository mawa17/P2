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


//{
//    "id": 1878,
//    "question": "What is the purpose of the Global Interpreter Lock (GIL) in Python?",
//    "description": "The GIL is a mechanism used in Python to manage access to Python objects in memory.",
//    "answers": {
//        "answer_a": "To improve file handling",
//      "answer_b": "To ensure that only one thread executes Python bytecode at a time",
//      "answer_c": "To manage CPU utilization",
//      "answer_d": "To allocate memory for variables",
//      "answer_e": null,
//      "answer_f": null
//    },
//    "multiple_correct_answers": "false",
//    "correct_answers": {
//    "answer_a_correct": "false",
//      "answer_b_correct": "true",
//      "answer_c_correct": "false",
//      "answer_d_correct": "false",
//      "answer_e_correct": "false",
//      "answer_f_correct": "false"
//    },
//    "correct_answer": null,
//    "explanation": "The Global Interpreter Lock (GIL) ensures that only one thread executes Python bytecode at a time, which simplifies memory management in CPython.",
//    "tip": null,
//    "tags": [
//      {
//        "name": "Python"
//      }
//    ],
//    "category": "Code",
//    "difficulty": "Easy"
//  },