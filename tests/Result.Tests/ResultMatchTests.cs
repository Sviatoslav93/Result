using FluentAssertions;
using Result.Errors;
using Xunit;

namespace Result.Tests;

public class ResultMatchTests
{
    [Fact]
    public void Should_MatchFailedResult()
    {
        var result = new Result<int>(Error.Failure("code", "description"));

        var value = result.Match(
            value => value,
            _ => 0);

        value.Should().Be(0);
    }

    [Fact]
    public async Task Should_MatchFailedResultAsync()
    {
        var result = new Result<int>(Error.Failure("code", "description"));

        var value = await result.MatchAsync(
            Task.FromResult,
            _ => 0);

        value.Should().Be(0);
    }

    [Fact]
    public void Should_MatchSuccessResult()
    {
        var result = new Result<int>(1);

        var value = result.Match(
            value => value,
            _ => 0);

        value.Should().Be(1);
    }

    [Fact]
    public async Task Should_MatchSuccessResultAsync()
    {
        var result = new Result<int>(1);

        var value = await result.MatchAsync(
            Task.FromResult,
            _ => 0);

        value.Should().Be(1);
    }
}
