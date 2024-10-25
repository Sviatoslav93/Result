using FluentAssertions;
using Result.Extensions;
using Xunit;

namespace Result.Tests.Extensions;

public partial class ResultExtensionsTests
{
    [Fact]
    public async Task Should_MatchSuccessAsync_From_Result()
    {
        var result = Result<string>.Success("1");

        (await result.MatchAsync(
                onSuccess: x => Task.FromResult(int.Parse(x)),
                onFailure: _ => Task.FromResult(0)))
            .Should().Be(1);

        (await result.MatchAsync(
                onSuccess: x => Task.FromResult(int.Parse(x)),
                onFailure: _ => 0))
            .Should().Be(1);

        (await result.MatchAsync(
                onSuccess: int.Parse,
                onFailure: _ => Task.FromResult(0)))
            .Should().Be(1);
    }

    [Fact]
    public async Task Should_MatchFailedAsync_From_Result()
    {
        var result = Result<string>.Failed(new Error());

        (await result.MatchAsync(
                onSuccess: x => Task.FromResult(int.Parse(x)),
                onFailure: _ => Task.FromResult(0)))
            .Should().Be(0);

        (await result.MatchAsync(
                onSuccess: x => Task.FromResult(int.Parse(x)),
                onFailure: _ => 0))
            .Should().Be(0);

        (await result.MatchAsync(
                onSuccess: int.Parse,
                onFailure: _ => Task.FromResult(0)))
            .Should().Be(0);
    }

    [Fact]
    public async Task Should_MatchSuccessAsync_From_ResultTask()
    {
        var result = Task.FromResult(Result<string>.Success("1"));

        (await result.MatchAsync(
                onSuccess: int.Parse,
                onFailure: _ => 0))
            .Should().Be(1);

        (await result.MatchAsync(
                onSuccess: x => Task.FromResult(int.Parse(x)),
                onFailure: _ => Task.FromResult(0)))
            .Should().Be(1);

        (await result.MatchAsync(
                onSuccess: x => Task.FromResult(int.Parse(x)),
                onFailure: _ => 0))
            .Should().Be(1);

        (await result.MatchAsync(
                onSuccess: int.Parse,
                onFailure: _ => Task.FromResult(0)))
            .Should().Be(1);
    }

    [Fact]
    public async Task Should_MatchFailedAsync_From_ResultTask()
    {
        var result = Task.FromResult(Result<string>.Failed(new Error()));

        (await result.MatchAsync(
                onSuccess: int.Parse,
                onFailure: _ => 0))
            .Should().Be(0);

        (await result.MatchAsync(
                onSuccess: v => Task.FromResult(int.Parse(v)),
                onFailure: _ => Task.FromResult(0)))
            .Should().Be(0);

        (await result.MatchAsync(
                onSuccess: x => Task.FromResult(int.Parse(x)),
                onFailure: _ => 0))
            .Should().Be(0);

        (await result.MatchAsync(
                onSuccess: int.Parse,
                onFailure: _ => Task.FromResult(0)))
            .Should().Be(0);
    }
}
