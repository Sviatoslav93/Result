using FluentAssertions;
using Xunit;

namespace Result.Tests;

public partial class ResultTests
{
    [Fact]
    public void Should_CreateFailedResult()
    {
        var result = Result<int>.Failed(new Error());

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
        var result = Result<int>.Failed(new Error("error one"), new Error("error two"));

        result.IsSuccess.Should().BeFalse();
        result.Errors.Should().HaveCount(2);
    }

    [Fact]
    public void Should_CreateFailedResultWithErrorsList()
    {
        var result = Result<int>.Failed(new List<Error>
        {
            new Error("error one"),
            new Error("error two"),
        });

        result.IsSuccess.Should().BeFalse();
        result.Errors.Should().HaveCount(2);
    }

    [Fact]
    public void Should_DeconstructFailedResult()
    {
        var (value, errors) = Result<int>.Failed(new Error());

        value.Should().Be(default);
        errors.Should().HaveCount(1);
    }

    [Fact]
    public void Should_DeconstructSuccessResult()
    {
        var (value, errors) = Result<int>.Success(1);

        value.Should().Be(1);
        errors.Should().BeEmpty();

        var (value2, errors2) = Result<int>.Failed(new Error());
        value2.Should().Be(default);
        errors2.Should().HaveCount(1);
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
        Result<int> result = new Error("error");

        result.IsSuccess.Should().BeFalse();
    }

    [Fact]
    public void Should_ImplicitConvertArrayOfErrorToResult()
    {
        Result<int> result = new[] { new Error(), new Error(), new Error() };

        result.IsSuccess.Should().BeFalse();
    }

    [Fact]
    public void Should_ImplicitConvertValueToResult()
    {
        Result<int> result = 1;

        result.IsSuccess.Should().BeTrue();
    }

    private class TestUserDto
    {
        public string Email { get; set; } = null!;
        public string FullName { get; set; } = null!;
    }
}
