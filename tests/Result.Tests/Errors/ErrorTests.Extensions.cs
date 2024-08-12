using FluentAssertions;
using Result.Errors;
using Result.Extensions;
using Xunit;

namespace Result.Tests.Errors;

public partial class ErrorTests
{
    [Fact]
    public void Should_ConvertErrorsListToFailure()
    {
        var errors = new List<Error>
        {
            Error.Conflict("code", "description"),
            Error.Failure("code", "description"),
        };
        var failure = errors.ToFailure();

        failure.HasAnyErrors.Should().BeTrue();
        failure.Should().HaveCount(2);
    }

    [Fact]
    public void Should_ConvertErrorsListToFailureAndFillOut()
    {
        var errors = new List<Error>
        {
            Error.Conflict("code", "description"),
            Error.Failure("code", "description"),
        };
        errors.ToFailure(out var failure);

        failure.HasAnyErrors.Should().BeTrue();
        failure.Should().HaveCount(2);
    }
}
