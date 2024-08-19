using FluentAssertions;
using Result.Errors;
using Result.Extensions;
using Xunit;

namespace Result.Tests.Extensions;

public partial class ResultExtensionsTests
{
    [Fact]
    public async Task ThenAsync_WithSuccess_ShouldReturnSuccessResult()
    {
        var result = await Result<int>.Success(1)
            .ThenAsync(value => Task.FromResult((value + 1).ToString()))
            .ThenAsync(x => (x + "3").ToString())
            .ThenAsync(x => Task.FromResult(int.Parse(x)));

        result.IsSuccess.Should().BeTrue();
        result.Value.Should().Be(23);
    }

    [Fact]
    public async Task ThenAsync_WithSuccess_ShouldReturnSuccessResult_1()
    {
        var result = await Result<int>.Success(1)
            .ThenAsync(value => Task.FromResult((value + 1).ToString()))
            .ThenAsync(x => (x + "3i").ToString()) // failed
            .ThenAsync(x => Task.FromResult(int.TryParse(x, out var value) ? Result<int>.Success(value) : Error.Failure("code", "description")));

        result.IsSuccess.Should().BeFalse();
        result.Failure.Should().HaveCount(1).And.Contain(Error.Failure("code", "description"));
    }
}
