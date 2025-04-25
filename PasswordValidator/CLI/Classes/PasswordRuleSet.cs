using CLI.Interfaces;
namespace CLI.Classes;

public sealed class PasswordRuleSet(byte minNums, byte minSpecialChars, byte minLength, string specialChars) : IPasswordRuleSet
{
    private readonly byte _minNums = minNums, _minSpecialChars = minSpecialChars, _minLength = minLength;
    private readonly string _specialChars = specialChars;

    public bool Validate(string password)
    {
        return password.Length >= _minLength &&
               password.Count(x => Char.IsDigit(x)) >= _minNums &&
               password.Count(x => _specialChars.Contains(x)) >= _minSpecialChars;
    }
}