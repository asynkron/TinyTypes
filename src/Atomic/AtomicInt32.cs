using System.Threading;
using JetBrains.Annotations;
using T = System.Int32;
namespace TinyTypes.Atomic
{
    /// <summary>
    ///     Tiny Wrapper over Interlocked Int64
    /// </summary>
    [PublicAPI]
    public class AtomicInt32
    {
        private T _value;

        public AtomicInt32()
        {
        }

        public AtomicInt32(T initialValue)
        {
            _value = initialValue;
        }

        public T Add(T value)
        {
            return Interlocked.Add(ref _value, value);
        }
        
        public T Or(T value)
        {
            return Interlocked.Or(ref _value, value);
        }
        
        public T And(T value)
        {
            return Interlocked.And(ref _value, value);
        }
        
        public T Read(T value)
        {
            Interlocked.MemoryBarrier();
            return _value;
        }
        
        public T Exchange(T value)
        {
            return Interlocked.Exchange(ref _value, value);
        }
        
        public T Exchange(T value, T comparand)
        {
            return Interlocked.CompareExchange(ref _value, value, comparand);
        }
        
        public T Decrement()
        {
            return Interlocked.Decrement(ref _value);
        }

        public long Increment()
        {
            return Interlocked.Increment(ref _value);
        }

        public void Reset()
        {
            Interlocked.Exchange(ref _value, 0);
        }
    }
}