using Xunit;

namespace Result.Tests;

public class ErrorTests
{
    [Fact]
    public void Should_CreateErrorWithMessage()
    {
        const string message = "Error message";

        var error = new Error(message);

        Assert.Equal(message, error.Message);
    }

    [Fact]
    public void Should_CreateErrorWithoutMessage()
    {
        var error = new Error();

        Assert.Null(error.Message);
    }

    [Fact]
    public void Should_ReturnMessageWhenToStringIsCalled()
    {
        const string message = "Error message";
        var error = new Error(message);

        var result = error.ToString();

        Assert.Equal(message, result);
    }

    [Fact]
    public void Should_ReturnEmptyStringWhenToStringIsCalledAndMessageIsNull()
    {
        var error = new Error();

        var result = error.ToString();

        Assert.Equal(string.Empty, result);
    }
}
