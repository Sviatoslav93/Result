using FluentAssertions;
using Result.Errors;
using Xunit;

namespace Result.Tests;

public class CreateResultTests
{
    [Fact]
    public void Should_CreateSuccessResult()
    {
        var result = Result<int>.Success(1);

        result.IsFailed.Should().BeFalse();
    }

    [Fact]
    public void Should_CreateFailedResult()
    {
        var result = Result<int>.Failed(Error.Failure("code", "description"));

        result.IsFailed.Should().BeTrue();
    }

    [Fact]
    public void Should_ImplicitConvertValueToResult()
    {
        Result<int> result = 1;

        result.IsFailed.Should().BeFalse();
    }

    [Fact]
    public void Should_ImplicitConvertErrorToResult()
    {
        Result<int> result = Error.Failure("code", "description");

        result.IsFailed.Should().BeTrue();
    }

    [Fact]
    public void Should_ImplicitConvertFailureToResult()
    {
        var failure = new Failure
        {
            Error.Failure("code", "description"),
        };

        Result<int> result = failure;

        result.IsFailed.Should().BeTrue();
    }
}
