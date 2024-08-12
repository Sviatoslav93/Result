using FluentAssertions;
using Result.Errors;
using Xunit;

namespace Result.Tests.Errors;

public partial class ErrorTests
{
    [Fact]
    public void Should_CreateConflictError()
    {
        var error = Error.Conflict("code", "description");

        error.ErrorType.Should().Be(ErrorType.Conflict);
    }

    [Fact]
    public void Should_CreateFailureError()
    {
        var error = Error.Failure("code", "description");

        error.ErrorType.Should().Be(ErrorType.Failure);
    }

    [Fact]
    public void Should_CreateForbiddenError()
    {
        var error = Error.Forbidden("code", "description");

        error.ErrorType.Should().Be(ErrorType.Forbidden);
    }

    [Fact]
    public void Should_CreateNotFoundError()
    {
        var error = Error.NotFound("code", "description");

        error.ErrorType.Should().Be(ErrorType.NotFound);
    }

    [Fact]
    public void Should_CreateUnexpectedError()
    {
        var error = Error.Unexpected("code", "description");

        error.ErrorType.Should().Be(ErrorType.Unexpected);
    }

    [Fact]
    public void Should_CreateValidationError()
    {
        var error = Error.Validation("code", "description");

        error.ErrorType.Should().Be(ErrorType.Validation);
    }
}
