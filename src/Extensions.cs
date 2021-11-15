using System;
using JetBrains.Annotations;

namespace TinyTypes
{
    [PublicAPI]
    public static class Extensions
    {
        public static void Do<T>(this Option<T>? self, Action<T> some, Action none)
        {
            if (self is Some<T>(var v))
            {
                some(v);
            }
            else
            {
                none();
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
            if (self is Some<TLeft>(var l))
                left(l);
            else if (self is Some<TRight>(var r))
                right(r);
            else
                none();
        }

        public static void Do<TLeft,TRight>(this Either<TLeft,TRight> self, Action<TLeft> left,Action<TRight> right)
        {
            if (self is Some<TLeft>(var l))
                left(l);
            else if (self is Some<TRight>(var r))
                right(r);
            else
                throw new ArgumentOutOfRangeException(nameof(self), self, null);
        }

        public static T GetOrDefault<T>(this Option<T>? self, T defaultValue) => 
            self is Some<T>(var value) ? value : defaultValue;
    }
}