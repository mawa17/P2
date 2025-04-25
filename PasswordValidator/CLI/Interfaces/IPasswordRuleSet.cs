namespace CLI.Interfaces;

public interface IPasswordRuleSet
{
    bool Validate(string password);
}