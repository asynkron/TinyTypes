#nullable enable
using System;
using JetBrains.Annotations;

namespace TinyTypes
{
    [PublicAPI]
    public interface Either<TLeft, TRight>
    {
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
            if (value is null) throw new ArgumentNullException(nameof(value));
            return new Left(value);
        }

        public static Either<TLeft, TRight> Some([System.Diagnostics.CodeAnalysis.NotNull] TRight value)
        {
            if (value is null) throw new ArgumentNullException(nameof(value));
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
}