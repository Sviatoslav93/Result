using FluentAssertions;
using Xunit;

namespace Result.Tests;

public class NothingTests
{
    [Fact]
    public void TwoNothingsValuesShouldBeEqual()
    {
        var first = Nothing.Value;
        var second = Nothing.Value;

        (first == second).Should().BeTrue();
        (first != second).Should().BeFalse();
        first.Equals(second).Should().BeTrue();
        first.CompareTo(second).Should().Be(0);
        first.Equals(new object()).Should().BeFalse();
    }

    [Fact]
    public void ShouldBeImplementedIComparable()
    {
        var nothing = Nothing.Value;
        var toComparable = (IComparable)nothing;
        toComparable.CompareTo(new object()).Should().Be(0);
    }

    [Fact]
    public async Task ShouldReturnTaskNothing()
    {
        var task = Nothing.Task;
        var nothing = await task;
        nothing.Should().Be(Nothing.Value);
    }

    [Fact]
    public void ShouldBeOverridenToString()
    {
        var nothing = Nothing.Value;
        nothing.ToString().Should().Be("()");
    }
}
