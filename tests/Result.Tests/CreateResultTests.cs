using FluentAssertions;
using Result.Errors;
using Xunit;

namespace Result.Tests;

public class CreateResultTests
{
    [Fact]
    public void Should_CreateFailedResult()
    {
        var result = new Result<int>(Error.Failure("code", "description"));

        result.IsFailed.Should().BeTrue();
    }

    [Fact]
    public void Should_CreateSuccessResult()
    {
        var result = new Result<int>(1);

        result.IsFailed.Should().BeFalse();
    }

    [Fact]
    public void Should_CreateSuccessResultWithSeveralErrors()
    {
        var result = new Result<int>(
            Error.Failure("code", "description"),
            Error.Failure("code", "description"));

        result.IsFailed.Should().BeTrue();
        result.Failure.Should().HaveCount(2);
    }

    [Fact]
    public void Should_ImplicitConvertErrorToResult()
    {
        Result<int> result = Error.Failure("code", "description");

        result.IsFailed.Should().BeTrue();
    }

    [Fact]
    public void Should_ImplicitConvertValueToResult()
    {
        Result<int> result = 1;

        result.IsFailed.Should().BeFalse();
    }
}
