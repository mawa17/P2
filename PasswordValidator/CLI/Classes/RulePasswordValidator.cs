using CLI.Interfaces;
namespace CLI.Classes;

public class RulePasswordValidator(IPasswordRuleSet ruleSet) : IPasswordValidator
{
    private readonly IPasswordRuleSet _passwordRuleSet = ruleSet;
    public bool IsValid(string password) => _passwordRuleSet.Validate(password);
}
