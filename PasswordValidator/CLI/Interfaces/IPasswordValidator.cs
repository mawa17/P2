namespace CLI.Interfaces;

public interface IPasswordValidator
{
    bool IsValid(string password);
}