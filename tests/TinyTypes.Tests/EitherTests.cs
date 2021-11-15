using System;
using FluentAssertions;
using Xunit;

namespace TinyTypes.Tests
{
    public class EitherTests
    {
        [Fact]
        public void EitherLeftShouldHaveValue()
        {
            var e = Either<int, string>.Some(123);
            var res = e switch
            {
                Some<int>(var i)    => i.ToString(),
                Some<string>(var s) => "str",
                _                   => "null",
            };

            res.Should().Be("123");
        }
        
        [Fact]
        public void EitherRightShouldHaveValue()
        {
            var e = Either<int, string>.Some("hello");
            var res = e switch
            {
                Some<int>(var i)    => "int",
                Some<string>(var s) => s,
                _                   => "null",
            };

            res.Should().Be("hello");
        }
    }
}