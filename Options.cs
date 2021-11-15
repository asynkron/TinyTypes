using System;

namespace TinyTypes
{
    public static class Options
    {
        public static Option<T>? Option<T>(T? value) => value is null ? null : new Some<T>(value);

        public static Option<T> Some<T>(T value)
        {
            return new Some<T>(value);
        }

        public static Option<T>? Null<T>() => null;

        public static Option<TOut>? Select<TIn, TOut>(this Option<TIn>? self, Func<TIn, TOut> selector)
        {
            if (self is not Some<TIn>(var v))
                return Null<TOut>();

            var yy = selector(v);
            return Some(yy);
        }
    }
    
    public abstract record Option<T>
    {
        public static implicit operator Option<T>?(T? value) => Options.Option(value);
    }

    public record Some<T> : Option<T>
    {
        public Some(T value)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            Value = value;
        }

        public T Value { get; }

        public void Deconstruct(out T o)
        {
            o = Value;
        }
    }
}