using FluentAssertions;
using Result.Abstractions;
using Result.Extensions;
using Xunit;

namespace Result.Tests.Extensions;

public partial class ResultExtensionsTests
{
    [Fact]
    public async Task ThenAsync_ShouldReturnSuccessResult_When_OnSuccessIsSuccessWithTaskNextValue()
    {
        var result1 = await Result<string>.Success("1")
            .ThenAsync(x => int.TryParse(x, out var value)
                ? Task.FromResult(value)
                : Task.FromResult(0));

        var result2 = await Task.FromResult(Result<string>.Success("1"))
            .ThenAsync(x => int.TryParse(x, out var value)
                ? Task.FromResult(value)
                : Task.FromResult(0));

        result1.IsSuccess.Should().BeTrue();
        result1.Value.Should().Be(1);

        result2.IsSuccess.Should().BeTrue();
        result2.Value.Should().Be(1);
    }

    [Fact]
    public async Task ThenAsync_ShouldReturnSuccessResult_When_OnSuccessIsSuccessWithTaskResultNextValue()
    {
        var resultSuccess1 = await Result<string>.Success("1")
            .ThenAsync(x => int.TryParse(x, out var value)
                ? Task.FromResult(Result<int>.Success(value))
                : Task.FromResult(Result<int>.Failed(new Error(ErrorMessage))));

        var resultSuccess2 = await Task.FromResult(Result<string>.Success("1"))
            .ThenAsync(x => int.TryParse(x, out var value)
                ? Task.FromResult(Result<int>.Success(value))
                : Task.FromResult(Result<int>.Failed()));

        var resultFailed1 = await Result<string>.Success("1i")
            .ThenAsync(x => int.TryParse(x, out var value)
                ? Task.FromResult(Result<int>.Success(value))
                : Task.FromResult(Result<int>.Failed(new Error(ErrorMessage))));

        var resultFailed2 = await Task.FromResult(Result<string>.Success("1i"))
            .ThenAsync(x => int.TryParse(x, out var value)
                ? Task.FromResult(Result<int>.Success(value))
                : Task.FromResult(Result<int>.Failed(new Error(ErrorMessage))));

        resultSuccess1.IsSuccess.Should().BeTrue();
        resultSuccess1.Value.Should().Be(1);

        resultFailed1.IsSuccess.Should().BeFalse();
        resultFailed1.Errors.Should().HaveCount(1);

        resultSuccess2.IsSuccess.Should().BeTrue();
        resultSuccess2.Value.Should().Be(1);

        resultFailed2.IsSuccess.Should().BeFalse();
        resultFailed2.Errors.Should().HaveCount(1);
    }

    [Fact]
    public async Task ThenAsync_ShouldReturnFailedResult_When_OnSuccessIsFailedWithTaskResultNextValue()
    {
        var resultFailed = await Result<string>.Success("1i")
            .ThenAsync(x => int.TryParse(x, out var value)
                ? Task.FromResult(Result<int>.Success(value))
                : Task.FromResult(Result<int>.Failed(new Error(ErrorMessage))));

        resultFailed.IsSuccess.Should().BeFalse();
        resultFailed.Errors.Should().HaveCount(1);
    }

    [Fact]
    public async Task ThenAsync_ShouldReturnSuccessResult_When_OnSuccessIsSuccessWithNexValue()
    {
        var resultSuccess = await Task.FromResult(Result<string>.Success("1"))
            .ThenAsync(x => int.TryParse(x, out var value)
                ? value
                : 0);

        resultSuccess.IsSuccess.Should().BeTrue();
        resultSuccess.Value.Should().Be(1);
    }

    [Fact]
    public async Task ThenAsync_ShouldReturnFailedResult_WhenAtLeastOneChainFailed()
    {
        var result = await Result<string>.Success("1i")
            .ThenAsync(x => int.TryParse(x, out var value)
                ? Task.FromResult(value)
                : Task.FromResult(0))
            .ThenAsync(x => x != 0
                ? Result<int>.Success(10 / x)
                : Result<int>.Failed(new Error("Division by zero")))
            .ThenAsync(x => x.ToString());

        result.IsSuccess.Should().BeFalse();
        result.Errors.Should().HaveCount(1);
    }

    [Fact]
    public async Task ThenAsync_ShouldReturnSuccessResult_WhenAllChainsAreSuccess()
    {
        var result = await Result<string>.Success("1")
            .ThenAsync(x => int.TryParse(x, out var value)
                ? Task.FromResult(value)
                : Task.FromResult(0))
            .ThenAsync(x => x != 0
                ? Result<int>.Success(10 / x)
                : Result<int>.Failed(new Error("Division by zero")))
            .ThenAsync(x => x.ToString());

        result.IsSuccess.Should().BeTrue();
        result.Errors.Should().HaveCount(0);
    }
}
