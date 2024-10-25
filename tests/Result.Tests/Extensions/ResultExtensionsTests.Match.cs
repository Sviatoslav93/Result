using FluentAssertions;
using Result.Extensions;
using Xunit;

namespace Result.Tests.Extensions;

public partial class ResultExtensionsTests
{
    [Fact]
    public void Should_MatchFailedResult()
    {
        var result = Result<int>.Failed(new Error());

        var value = result.Match(
            x => x,
            _ => 0);

        value.Should().Be(0);
    }

    [Fact]
    public void Should_MatchSuccessResult()
    {
        var result = Result<int>.Success(1);

        var value = result.Match(
            x => x,
            _ => 0);

        value.Should().Be(1);
    }
}
