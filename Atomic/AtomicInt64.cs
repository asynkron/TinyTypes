using System.Threading;
using JetBrains.Annotations;

namespace TinyTypes.Atomic
{
    /// <summary>
    ///     Tiny Wrapper over Interlocked Int64
    /// </summary>
    [PublicAPI]
    public class AtomicInt64
    {
        private long _value;

        public AtomicInt64()
        {
        }

        public AtomicInt64(long initialValue)
        {
            _value = initialValue;
        }

        public long Value => _value;

        public long Add(long value)
        {
            return Interlocked.Add(ref _value, value);
        }

        public long Increment()
        {
            return Interlocked.Increment(ref _value);
        }

        public void Reset()
        {
            Interlocked.Exchange(ref _value, 0);
        }
        
        public static AtomicInt64 operator ++(AtomicInt64 v) => v.Increment();

        public static implicit operator AtomicInt64(long value)
        {
            return new(value);
        }

        public static implicit operator long(AtomicInt64 value)
        {
            return value.Value;
        }
    }
}