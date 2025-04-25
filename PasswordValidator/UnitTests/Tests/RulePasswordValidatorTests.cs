using CLI.Classes;
namespace UnitTests.Tests;

[TestClass]
public class RulePasswordValidatorTests
{
    [TestMethod]
    public void IsValid_ValidPassword_ReturnsTrue()
    {
        // Arrange
        var ruleSet = new PasswordRuleSet(minNums: 2, minSpecialChars: 2, minLength: 8, specialChars: "!@#$%");
        var validator = new RulePasswordValidator(ruleSet);

        // Act
        var result = validator.IsValid("a0b!e$duR1");

        // Assert
        Assert.IsTrue(result);
    }

    [TestMethod] // Marks the method as a test method
    public void IsValid_InvalidPassword_ReturnsFalse()
    {
        // Arrange
        var ruleSet = new PasswordRuleSet(minNums: 2, minSpecialChars: 2, minLength: 8, specialChars: "!@#$%");
        var validator = new RulePasswordValidator(ruleSet);

        // Act
        var result = validator.IsValid("ab3r!!");

        // Assert
        Assert.IsFalse(result);
    }

}