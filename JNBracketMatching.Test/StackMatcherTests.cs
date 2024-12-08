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
        [InlineData(">", false)]
        [InlineData("<", false)]
        [InlineData("abcdefghijklmnopqrstuvwxyz0123456789 _-+={}[];:'\"\\/?.,!@#$%^&*()", true)] // non-angle bracket characters
        [InlineData("<<>>", true)] // empty nested
        [InlineData("<1<abc>2>3<4<xyz>5>", true)] // nested
        [InlineData("<1<2<3<abc>3>2>1>", true)] // multi-level nested
        [InlineData("<1<abc>2>3<4<xyz>>5>", false)] // nested with non-matching closing angle bracket
        [InlineData("<&gt;", false)] // html entity closing angle bracket
        [InlineData("&lt;>", false)] // html entity opening angle bracket
        [InlineData("a<b>c<d>e", true)] 
        [InlineData("blah <bleh blih> bluh <blarg>", true)]
        [InlineData(null, true)] // null case
        public void IsValid_WithInput_ReturnsExpectedResult(string input, bool expectedResult)
        {
            var matcher = new StackMatcher();

            var result = matcher.IsValid(input);

            result.Should().Be(expectedResult);
        }

        [Fact]
        public void IsValid_WithLotsOfMatchingBrackets_ReturnsTrue()
        {
            var matcher = new StackMatcher();
            string input = new string('<', 1000000) + "blah" + new string('>', 1000000);

            var result = matcher.IsValid(input);

            result.Should().BeTrue();
        }

        [Fact]
        public void IsValid_WithLotsOfManyBracketsAndAnOpenOneAtTheEnd_ReturnsFalse()
        {
            var matcher = new StackMatcher();
            string input = new string('<', 1000000) + "blah" + new string('>', 1000000) + "<";

            var result = matcher.IsValid(input);

            result.Should().BeFalse();
        }
    }
}