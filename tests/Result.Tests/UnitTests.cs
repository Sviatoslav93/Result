using FluentAssertions;
using Xunit;

namespace Result.Tests;

public class UnitTests
{
    [Fact]
    public void TwoNothingsValuesShouldBeEqual()
    {
        var first = Unit.Value;
        var second = Unit.Value;

        (first == second).Should().BeTrue();
        (first != second).Should().BeFalse();
        first.Equals(second).Should().BeTrue();
        first.CompareTo(second).Should().Be(0);
        first.Equals(new object()).Should().BeFalse();
    }

    [Fact]
    public void ShouldBeImplementedIComparable()
    {
        var nothing = Unit.Value;
        var toComparable = (IComparable)nothing;
        toComparable.CompareTo(new object()).Should().Be(0);
    }

    [Fact]
    public async Task ShouldReturnTaskNothing()
    {
        var task = Unit.Task;
        var nothing = await task;
        nothing.Should().Be(Unit.Value);
    }

    [Fact]
    public void ShouldBeOverridenToString()
    {
        var nothing = Unit.Value;
        nothing.ToString().Should().Be("()");
    }
}
