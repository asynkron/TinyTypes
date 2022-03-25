using System;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace TinyTypes;

[PublicAPI]
public static class Extensions
{
    public static void Do<T>(this Option<T>? self, Action<T> some, Action none)
    {
        switch (self)
        {
            case Some<T>(var v):
                some(v);
                break;
            default:
                none();
                break;
        }
    }
        
    public static TOut Map<T,TOut>(this Option<T>? self, Func<T,TOut> some, Func<TOut> none) => 
        self is Some<T>(var v) ? some(v) : none();
        
    public static TOut Map<TLeft,TRight,TOut>(this Either<TLeft,TRight>? self, Func<TLeft,TOut> left,Func<TRight,TOut> right, Func<TOut> none) =>
        self switch
        {
            Some<TLeft>(var l)  => left(l),
            Some<TRight>(var r) => right(r),
            _                   => none()
        };
        
    public static TOut Map<TLeft,TRight,TOut>(this Either<TLeft,TRight> self, Func<TLeft,TOut> left,Func<TRight,TOut> right) =>
        self switch
        {
            Some<TLeft>(var l)  => left(l),
            Some<TRight>(var r) => right(r),
            _                   => throw new ArgumentOutOfRangeException(nameof(self), self, null)
        };

    public static void Do<TLeft, TRight>(this Either<TLeft, TRight>? self, Action<TLeft> left, Action<TRight> right, Action none)
    {
        switch (self)
        {
            case Some<TLeft>(var l):
                left(l);
                break;
            case Some<TRight>(var r):
                right(r);
                break;
            default:
                none();
                break;
        }
    }

    public static void Do<TLeft,TRight>(this Either<TLeft,TRight> self, Action<TLeft> left,Action<TRight> right)
    {
        switch (self)
        {
            case Some<TLeft>(var l):
                left(l);
                break;
            case Some<TRight>(var r):
                right(r);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(self), self, null);
        }
    }

    public static T GetOrDefault<T>(this Option<T>? self, T defaultValue) => 
        self is Some<T>(var value) ? value : defaultValue;

    public static IO<Either<TOut, Exception>> Try<TOut>(this IO<TOut> action) =>
        () => TryAsync(() => action());

    public static IO<Either<T, Exception>> Retry<T>(this IO<T> io, int count) =>
        async () =>
        {
            for (var i = 0; i < count; i++)
            {
                var y = await io.Try()();

                if (y is Some<T>)
                {
                    return y;
                }
            }
            return await io.Try()();
        };
    
    public static Either<TOut, Exception> Try<TOut>(this Func<TOut> action)
    {
        try
        {
            var res = action();
            return Either<TOut, Exception>.Some(res);
        }
        catch (Exception x)
        {
            return Either<TOut, Exception>.Some(x);
        }
    }

    public static async ValueTask<Either<TOut, Exception>> TryAsync<TOut>(this Func<ValueTask<TOut>> action)
    {
        try
        {
            var res = await action();
            return Either<TOut, Exception>.Some(res);
        }
        catch (Exception x)
        {
            return Either<TOut, Exception>.Some(x);
        }
    }
}