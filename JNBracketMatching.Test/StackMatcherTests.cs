using FluentAssertions;
using JNBracketMatching.Library;

namespace JNBracketMatching.Test
{
    public class StackMatcherTests
    {
        [Theory]
        [InlineData("", true)]
        [InlineData("<>", true)]
        [InlineData("<abcdefghijklmnopqrstuvwxyz>", true)]
        [InlineData("<<>", false)]
        [InlineData("><", false)]
        public void IsValid_WithInput_ReturnsExpectedResult(string input, bool expectedResult)
        {
            var matcher = new StackMatcher();

            var result = matcher.IsValid(input);

            result.Should().Be(expectedResult);
        }
    }
}