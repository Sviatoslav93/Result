using FluentAssertions;
using Result.Errors;
using Xunit;

namespace Result.Tests;

public partial class ResultTests
{
    [Fact]
    public void Should_CreateFailedResult()
    {
        var result = Result<int>.Failed(Error.Failure("code", "description"));

        result.IsSuccess.Should().BeFalse();
    }

    [Fact]
    public void Should_CreateSuccessResultForValueType()
    {
        var result = Result<int>.Success(1);

        result.Value.Should().Be(1);
        result.IsSuccess.Should().BeTrue();
    }

    [Fact]
    public void Should_CreateSuccessResultForNullableValueType()
    {
        var result = Result<int?>.Success(default);

        result.Value.Should().BeNull();
        result.IsSuccess.Should().BeTrue();
    }

    [Fact]
    public void Should_CreateSuccessResultForReferenceType()
    {
        var testDto = new TestUserDto
        {
            FullName = "John Doe",
            Email = "test@gmail.com",
        };
        var result = Result<TestUserDto>.Success(testDto);

        result.Value.Should().NotBeNull();
        result.IsSuccess.Should().BeTrue();
    }

    [Fact]
    public void Should_CreateSuccessResultForNullableReferenceType()
    {
        var testDto = default(TestUserDto);
        var result = Result<TestUserDto?>.Success(testDto);

        result.Value.Should().BeNull();
        result.IsSuccess.Should().BeTrue();
    }

    [Fact]
    public void Should_CreateFailedResultWithSeveralErrors()
    {
        var result = Result<int>.Failed(
            Error.Failure("code", "description"),
            Error.Failure("code", "description"));

        result.IsSuccess.Should().BeFalse();
        result.Failure.Should().HaveCount(2);
    }

    [Fact]
    public void Should_CreateFailedResultWithErrorsList()
    {
        var result = Result<int>.Failed(new List<Error>
        {
            Error.Failure("code", "description"),
            Error.Failure("code", "description"),
        });

        result.IsSuccess.Should().BeFalse();
        result.Failure.Should().HaveCount(2);
    }

    [Fact]
    public void Should_DeconstructFailedResult()
    {
        var (value, failure) = Result<int>.Failed(Error.Failure("code", "description"));

        value.Should().Be(default);
        failure.Should().HaveCount(1);
    }

    [Fact]
    public void Should_DeconstructSuccessResult()
    {
        var (value, failure) = Result<int>.Success(1);

        value.Should().Be(1);
        failure.Should().BeEmpty();

        var (value2, failure2) = Result<int>.Failed(Error.Failure("code", "description"));
        value2.Should().Be(default);
        failure2.Should().HaveCount(1);
    }

    [Fact]
    public void Should_GetValue_When_ResultIsSuccess()
    {
        var result = Result<int>.Success(1);

        result.Value.Should().Be(1);
    }

    [Fact]
    public void Should_ImplicitConvertErrorToResult()
    {
        Result<int> result = Error.Failure("code", "description");

        result.IsSuccess.Should().BeFalse();
    }

    [Fact]
    public void Should_ImplicitConvertFailureToResult()
    {
        Result<int> result = new Failure(new List<Error>
        {
            Error.Failure("code", "description"),
            Error.Failure("code", "description"),
        });

        result.IsSuccess.Should().BeFalse();
    }

    [Fact]
    public void Should_ImplicitConvertValueToResult()
    {
        Result<int> result = 1;

        result.IsSuccess.Should().BeTrue();
    }

    [Fact]
    public void Should_ThrowInvalidOperationException_When_TryToGetValueFromFailedResult()
    {
        var result = Result<TestUserDto?>.Failed(Error.Failure("code", "description"));

        FluentActions.Invoking(() => result.Value)
            .Should()
            .Throw<InvalidOperationException>();
    }

    private class TestUserDto
    {
        public string Email { get; set; } = null!;
        public string FullName { get; set; } = null!;
    }
}
