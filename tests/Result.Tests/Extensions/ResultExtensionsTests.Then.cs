using FluentAssertions;
using Result.Errors;
using Result.Extensions;
using Xunit;

namespace Result.Tests.Extensions;

public partial class ResultExtensionsTests
{
    [Fact]
    public void Should_ThenReturnSuccessResult_WhenAllOperationsIsSuccess()
    {
        var result = Result<int>.Success(1)
            .Then(x => (x + 1).ToString())
            .Then(x => x + "3")
            .Then(x => int.TryParse(x, out var value) ? Result<int>.Success(value) : Error.Failure("code", "description"));

        result.IsSuccess.Should().BeTrue();
        result.Value.Should().Be(23);
    }

    [Fact]
    public void Should_ThenReturnFailedResult_WhenAtLeastOneOperationFailed()
    {
        var result = Result<int>.Success(1)
            .Then(x => (x + 1).ToString())
            .Then(x => x + "3i") // failed
            .Then(x => int.TryParse(x, out var value) ? Result<int>.Success(value) : Error.Failure("code", "description"));

        result.IsSuccess.Should().BeFalse();
        result.Failure.Should().HaveCount(1).And.Contain(Error.Failure("code", "description"));
    }
}
