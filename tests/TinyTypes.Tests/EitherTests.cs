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
                Some<int>(var i) => i.ToString(),
                Some<string>(_)  => "str",
                _                => "null",
            };

            res.Should().Be("123");
        }
        
        [Fact]
        public void MapEitherLeftShouldHaveValue()
        {
            var e = Either<int, string>.Some(123);
            var res = e.Map(l => l.ToString(), _ => "str");
            res.Should().Be("123");
        }

        [Fact]
        public void EitherRightShouldHaveValue()
        {
            var e = Either<int, string>.Some("hello");
            var res = e switch
            {
                Some<int>(_)        => "int",
                Some<string>(var s) => s,
                _                   => "null",
            };

            res.Should().Be("hello");
        }
        
        [Fact]
        public void MapEitherRightShouldHaveValue()
        {
            var e = Either<int, string>.Some("hello");
            var res = e.Map(l => "int", r => r);
            res.Should().Be("hello");
        }
    }
}