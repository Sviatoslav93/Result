using FluentAssertions;
using Result.Errors;
using Xunit;

public class ResultMatchTests
{
    [Fact]
    public void Should_MatchSuccessResult()
    {
        var result = Result<int>.Success(1);

        var value = result.Match(
            success: value => value,
            failure: _ => 0);

        value.Should().Be(1);
    }

    [Fact]
    public void Should_MatchFailedResult()
    {
        var result = Result<int>.Failed(Error.Failure("code", "description"));

        var value = result.Match(
            success: value => value,
            failure: _ => 0);

        value.Should().Be(0);
    }

    [Fact]
    public async Task Should_MatchSuccessResultAsync()
    {
        var result = Result<int>.Success(1);

        var value = await result.MatchAsync(
            success: value => Task.FromResult(value),
            failure: _ => 0);

        value.Should().Be(1);
    }

    [Fact]
    public async Task Should_MatchFailedResultAsync()
    {
        var result = Result<int>.Failed(Error.Failure("code", "description"));

        var value = await result.MatchAsync(
            success: value => Task.FromResult(value),
            failure: _ => 0);

        value.Should().Be(0);
    }
}
