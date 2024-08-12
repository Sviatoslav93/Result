using FluentAssertions;
using Result.Errors;
using Xunit;

namespace Result.Tests;

public partial class ResultTests
{
    [Fact]
    public void Should_CreateFailedResult()
    {
        var result = new Result<int>(Error.Failure("code", "description"));

        result.IsSuccess.Should().BeFalse();
    }

    [Fact]
    public void Should_CreateSuccessResultForValueType()
    {
        var result = new Result<int>(1);

        result.Value.Should().Be(1);
        result.IsSuccess.Should().BeTrue();
    }

    [Fact]
    public void Should_CreateSuccessResultForNullableValueType()
    {
        var result = new Result<int?>(default(int?));

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
        var result = new Result<TestUserDto>(testDto);

        result.Value.Should().NotBeNull();
        result.IsSuccess.Should().BeTrue();
    }

    [Fact]
    public void Should_CreateSuccessResultForNullableReferenceType()
    {
        var testDto = default(TestUserDto);
        var result = new Result<TestUserDto?>(testDto);

        result.Value.Should().BeNull();
        result.IsSuccess.Should().BeTrue();
    }

    [Fact]
    public void Should_CreateFailedResultWithSeveralErrors()
    {
        var result = new Result<int>(
            Error.Failure("code", "description"),
            Error.Failure("code", "description"));

        result.IsSuccess.Should().BeFalse();
        result.Failure.Should().HaveCount(2);
    }

    [Fact]
    public void Should_CreateFailedResultWithErrorsList()
    {
        var result = new Result<int>(new List<Error>
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
        var (value, failure) = new Result<int>(Error.Failure("code", "description"));

        value.Should().Be(default);
        failure.Should().HaveCount(1);
    }

    [Fact]
    public void Should_DeconstructSuccessResult()
    {
        var (value, failure) = new Result<int>(1);

        value.Should().Be(1);
        failure.Should().BeEmpty();

        var (value2, failure2) = new Result<int>(Error.Failure("code", "description"));
        value2.Should().Be(default);
        failure2.Should().HaveCount(1);
    }

    [Fact]
    public void Should_GetValue_When_ResultIsSuccess()
    {
        var result = new Result<int>(1);

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
        var result = new Result<TestUserDto?>(Error.Failure("code", "description"));

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
