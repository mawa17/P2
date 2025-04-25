using CLI.Classes;
using CLI.Interfaces;
using System.Text.RegularExpressions;
namespace UnitTests.Tests;

[TestClass]
public class RegexPasswordValidatorTests
{
    //MethodUnderTest_Condition_ExpectedBehavior
    [TestMethod]
    public void IsValid_ValidPassword_ReturnsTrue()
    {
        // Arrange
        var regex = new Regex(@"^(?=(.*\d){2,})(?=(.*[!@#$%&]){2,}).{8,}$");
        IPasswordValidator validator = new RegexPasswordValidator(regex);

        // Act
        var result = validator.IsValid("a0b!e$duR1");

        // Assert
        Assert.IsTrue(result);
    }

    [TestMethod]
    public void IsValid_InvalidPassword_ReturnsFalse()
    {
        // Arrange
        var regex = new Regex(@"^(?=(.*\d){2,})(?=(.*[!@#$%&]){2,}).{8,}$");
        IPasswordValidator validator = new RegexPasswordValidator(regex);

        // Act
        var result = validator.IsValid("ab3r!!");

        // Assert
        Assert.IsFalse(result);
    }
}