using System;
using System.Threading.Tasks;

namespace TinyTypes
{
    public static class Try
    {
        public static Either<TOut, Exception> Execute<TOut>(Func<TOut> action)
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

        public static async Task<Either<TOut, Exception>> ExecuteAsync<TOut>(Func<Task<TOut>> action)
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
}