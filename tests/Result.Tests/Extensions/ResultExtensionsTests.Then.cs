using FluentAssertions;
using Result.Abstractions;
using Result.Extensions;
using Xunit;

namespace Result.Tests.Extensions;

public partial class ResultExtensionsTests
{
    private const string ErrorMessage = "Input string was not in a correct format.";

    [Fact]
    public void Should_ReturnSuccessResult_When_AllThenPipeSuccess()
    {
        var result = Result<string>.Success("1")
            .Then(x => int.TryParse(x, out var value) ? Result<int>.Success(value) : new Error(ErrorMessage))
            .Then(x => x + 1);

        result.IsSuccess.Should().BeTrue();
        result.Value.Should().Be(2);
    }

    [Fact]
    public void Should_ReturnFailedResult_When_AnyOfThenPipeIsFailed()
    {
        var result = Result<string>.Success("1")
            .Then(x => int.TryParse($"{x}i", out var value) ? Result<int>.Success(value) : new Error(ErrorMessage))
            .Then(x => x + 1);

        result.IsSuccess.Should().BeFalse();
        result.Errors.Should().HaveCount(1).And.ContainSingle(e => e.Message == ErrorMessage);
    }
}
