#nullable enable
using System;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;

namespace TinyTypes;

[PublicAPI]
// ReSharper disable once InconsistentNaming
public interface Either<TLeft, TRight> : ITuple
{
    public void Deconstruct(out Option<TLeft>? left, out Option<TRight>? right)
    {
        left = GetLeft();
        right = GetRight();
    }
    int ITuple.Length => 2;
    object? ITuple.this[int index]
    {
        get
        {
            if (index == 0)
                return GetLeft();
            if (index == 1)
                return GetRight();
            return default;
        }
    }
    public Option<TLeft>? GetLeft()
    {
        return this as Left;
    }

    public Option<TRight>? GetRight()
    {
        return this as Right;
    }

    public static Either<TLeft, TRight> Some([System.Diagnostics.CodeAnalysis.NotNull] TLeft value)
    {
        if (value is null)
        {
            throw new ArgumentNullException(nameof(value));
        }
        return new Left(value);
    }

    public static Either<TLeft, TRight> Some([System.Diagnostics.CodeAnalysis.NotNull] TRight value)
    {
        if (value is null)
        {
            throw new ArgumentNullException(nameof(value));
        }
        return new Right(value);
    }

    private sealed record Left : Some<TLeft>, Either<TLeft, TRight>
    {
        public Left(TLeft value) : base(value)
        {
        }
    }

    private sealed record Right : Some<TRight>, Either<TLeft, TRight>
    {
        public Right(TRight value) : base(value)
        {
        }
    }
}