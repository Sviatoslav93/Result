using FluentAssertions;
using Result.Extensions;
using Xunit;

namespace Result.Tests;

public partial class ResultTests
{
    [Fact]
    public void Should_MatchFailedResult()
    {
        var result = Result<int>.Failed(new Error());

        var value = result.Match(
            value => value,
            _ => 0);

        value.Should().Be(0);
    }

    [Fact]
    public async Task Should_MatchFailedResultAsync()
    {
        var result = Result<int>.Failed(new Error());

        var value = await result.MatchAsync(
            Task.FromResult,
            _ => 0);

        value.Should().Be(0);
    }

    [Fact]
    public void Should_MatchSuccessResult()
    {
        var result = Result<int>.Success(1);

        var value = result.Match(
            value => value,
            _ => 0);

        value.Should().Be(1);
    }

    [Fact]
    public async Task Should_MatchSuccessResultAsync()
    {
        var result = Result<int>.Success(1);

        var value = await result.MatchAsync(
            Task.FromResult,
            _ => 0);

        value.Should().Be(1);
    }
}
