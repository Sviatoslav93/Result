using FluentAssertions;
using Result.Errors;
using Result.Extensions;
using Xunit;

namespace Result.Tests.Extensions;

public class FailureExtensionsTests
{
    [Fact]
    public void Should_ConvertErrorsToFailure()
    {
        Error[] errors = [Error.Failure("code1", "description1"), Error.Failure("code2", "description2")];
        var failure = errors.ToFailure();

        failure.Should().HaveCount(2);
    }

    [Fact]
    public void Should_ConvertErrorsToFailureAndSetOut()
    {
        Error[] errors = [Error.Failure("code1", "description1"), Error.Failure("code2", "description2")];
        errors.ToFailure(out var failure);

        failure.Should().HaveCount(2);
    }
}
