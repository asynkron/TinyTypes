using System;
using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;

namespace TinyTypes
{
    [PublicAPI]
    public static class Options
    {
        public static Option<T>? Option<T>(T? value) where T:struct
        {
            return value is null ? null : new Some<T>(value.Value);
        }
        public static Option<T>? Option<T>(T? value)
        {
            return value is null ? null : new Some<T>(value);
        }

        public static Option<T> Some<T>(T value)
        {
            return new Some<T>(value);
        }

        public static Option<T>? None<T>()
        {
            return null;
        }

        public static Option<TOut>? Select<TIn, TOut>(this Option<TIn>? self, Func<TIn, TOut> selector)
        {
            if (self is not Some<TIn>(var v))
                return None<TOut>();
            var yy = selector(v);
            return Some(yy);
        }
    }

    [PublicAPI]
    public abstract record Option<T>
    {
        public static implicit operator Option<T>?(T? value)
        {
            return Options.Option(value);
        }
    }

    [PublicAPI]
    public record Some<T> : Option<T>
    {
        public Some(T value)
        {
            if (value is null) throw new ArgumentNullException(nameof(value));
            Value = value;
        }

        public T Value { get; }

        public void Deconstruct(out T o)
        {
            o = Value;
        }
    }
}