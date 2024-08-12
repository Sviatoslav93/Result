using FluentAssertions;
using Result.Errors;
using Xunit;

namespace Result.Tests;

public class FailureTest
{
    [Fact]
    public void Should_CreateFailureWithEmptyErrors()
    {
        var failure = new Failure();

        failure.Should().BeEmpty();
        failure.HasAnyErrors.Should().BeFalse();
    }

    [Fact]
    public void Should_AddErrorToFailure()
    {
        var failure = new Failure
        {
            new("1000", "failure"),
        };

        failure.Should().HaveCount(1);
        failure.HasAnyErrors.Should().BeTrue();
    }

    [Fact]
    public void Should_AddRangeOfErrorsToFailure()
    {
        var failure = new Failure();

        failure.AddRange(new[]
        {
            Error.Failure("1000", "failure"),
            Error.Failure("1001", "failure"),
        });

        failure.Should().HaveCount(2);
        failure.HasAnyErrors.Should().BeTrue();
    }

    [Fact]
    public void Should_CreateFailureWithErrors()
    {
        var failure = new Failure(
            new Error("1000", "failure"),
            new Error("1001", "not found exception", ErrorType.NotFound));

        failure.Should().HaveCount(2);
        failure.HasAnyErrors.Should().BeTrue();
    }

    [Fact]
    public void Should_CreateFailureWithIEnumerableErrors()
    {
        var failure = new Failure(Enumerable.Empty<Error>());

        failure.Should().HaveCount(0);
        failure.HasAnyErrors.Should().BeFalse();
    }

    [Fact]
    public void Should_ImplicitlyConvertFailureToBool()
    {
        var failure = new Failure(new Error("1000", "failure"));

        bool hasAnyErrors = failure;

        hasAnyErrors.Should().BeTrue();
    }
}
