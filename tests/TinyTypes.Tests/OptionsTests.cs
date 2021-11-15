using FluentAssertions;
using TinyTypes;
using Xunit;

public class OptionsTests
{
    [Fact]
    public void SomeShouldHaveValue()
    {
        var some = Options.Some(123);
        var res = some switch
        {
            Some<int>(var i) => i,
            _                => 0,
        };

        res.Should().Be(123);
    }
    
    [Fact]
    public void MapSomeShouldHaveValue()
    {
        var some = Options.Some(123);
        var res = some.Map(i => i,() => 0);
        res.Should().Be(123);
    }
    
    [Fact]
    public void NoneShouldHaveNoValue()
    {
        var none = Options.None<int>();
        var res = none switch
        {
            Some<int>(var i) => i,
            _                => 0,
        };

        res.Should().Be(0);
    }
    
    [Fact]
    public void MapNoneShouldHaveNoValue()
    {
        var some = Options.None<int>();
        var res = some.Map(i => i,() => 0);
        res.Should().Be(0);
    }
}