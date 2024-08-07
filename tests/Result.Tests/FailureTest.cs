using FluentAssertions;
using Result.Errors;
using Xunit;

namespace Result.Tests
{
    public class FailureTest
    {
        [Fact]
        public void Should_CreateFailureWithOneError()
        {
            var failure = new Failure(new Error("1000", "Unexpected error"));

            failure.Should().HaveCount(1);
            failure.HasAnyErrors.Should().BeTrue();
        }

        [Fact]
        public void Should_CreateFailureWithListOfErrors()
        {
            var failure = new Failure(
                new List<Error>
                {
                    new Error("1000", "Unexpected error"),
                    new Error("1001", "Not found exception"),
                });

            failure.Should().HaveCount(2);
            failure.HasAnyErrors.Should().BeTrue();
        }

        [Fact]
        public void Should_CreateFailureWithErrors()
        {
            var failure = new Failure(
                new Error("1000", "failure", ErrorType.Failure),
                new Error("1001", "not found exception", ErrorType.NotFound));

            failure.Should().HaveCount(2);
            failure.HasAnyErrors.Should().BeTrue();
        }

        [Fact]
        public void Should_AddErrorToFailure()
        {
            var failure = new Failure()
            {
                new Error("1000", "failure", ErrorType.Failure),
                new Error("1001", "not found exception", ErrorType.NotFound),
            };

            failure.Should().HaveCount(2);
            failure.HasAnyErrors.Should().BeTrue();
        }

        [Fact]
        public void Should_ImplicitlyConvertFailureToBool()
        {
            var failure = new Failure(new Error("1000", "failure", ErrorType.Failure));

            bool hasAnyErrors = failure;

            hasAnyErrors.Should().BeTrue();
        }
    }
}
