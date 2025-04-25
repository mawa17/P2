using CLI.Interfaces;
using System.Text.RegularExpressions;
namespace CLI.Classes;

public class RegexPasswordValidator(Regex pattern) : IPasswordValidator
{
    public bool IsValid(string password) => pattern.IsMatch(password);
}
