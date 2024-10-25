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
}
