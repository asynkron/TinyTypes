using System.Threading;
using JetBrains.Annotations;

namespace TinyTypes.Atomic
{
    /// <summary>
    ///     Tiny Wrapper over Interlocked Int32
    /// </summary>
    [PublicAPI]
    public class AtomicInt32
    {
        private int _value;

        public AtomicInt32()
        {
        }

        public AtomicInt32(int initialValue)
        {
            _value = initialValue;
        }

        public int Value => _value;

        public int Add(int value)
        {
            return Interlocked.Add(ref _value, value);
        }

        public int Increment()
        {
            return Interlocked.Increment(ref _value);
        }

        public void Reset()
        {
            Interlocked.Exchange(ref _value, 0);
        }
        
        public static AtomicInt32 operator ++(AtomicInt32 v) => v.Increment();

        public static implicit operator AtomicInt32(int value)
        {
            return new(value);
        }

        public static implicit operator int(AtomicInt32 value)
        {
            return value.Value;
        }
    }
}