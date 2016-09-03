using FluentAssertions;
using Xunit;

namespace Tests
{
    public class Test
    {
        [Fact]
        public void IsZero()
        {
            1.Should().Be(0, "because 0 should be 0");
        }
    }
}
