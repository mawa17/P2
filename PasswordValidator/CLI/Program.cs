using CLI.Classes;
using CLI.Interfaces;
using System.Text.RegularExpressions;



IFruit fruit1 = new Apple();
IFruit fruit2 = new Banana();
fruit2 = fruit1;
Console.WriteLine(fruit2.GetColor());

/*
    ^                       Start anchor
    (?=(.*\\d){2,})         Positiv lookahead gruppe som leder efter tal med 2 eller flere
    (?=(.*[!@#$%&]){2,})    Positiv lookahead gruppe som leder efter !@#$%& med 2 eller flere
    .{8,}                   Matcher 8 eller flere af de forgåne grupper
    $                       Slut anchor
*/
Regex pattern = new("^(?=(.*\\d){2,})(?=(.*[!@#$%&]){2,}).{8,}$");

// Init validators
IPasswordValidator regexValidator = new RegexPasswordValidator(pattern);
IPasswordRuleSet ruleSet = new PasswordRuleSet(2, 2, 8, "!@#$%&");
IPasswordValidator ruleValidator = new RulePasswordValidator(ruleSet);

// Vælg validator
Console.WriteLine("Tryk (1) for at vælge regexValidator");
Console.WriteLine("Tryk (2) for at vælge ruleValidator");
ConsoleKey key = Console.ReadKey().Key;
IPasswordValidator? validator = 
    key == ConsoleKey.D1 ? regexValidator : 
    key == ConsoleKey.D2 ? ruleValidator : null;

if(validator is null)
{
    Console.WriteLine("INGEN Validator valgt!");
    Environment.Exit(1);
}

do
{
    Console.Clear();
    Console.Write("Skriv et kodeord: ");
    string? password = Console.ReadLine();
    Console.WriteLine((!String.IsNullOrEmpty(password) && validator.IsValid(password)) ? "Stærk" : "Svag");
}
while (Console.ReadKey().Key != ConsoleKey.Escape);