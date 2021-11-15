using JetBrains.Annotations;

namespace TinyTypes
{
    [PublicAPI]
    public static class Extensions
    {
        public static T GetOrDefault<T>(this Option<T>? self, T defaultValue)
        {
            return self switch
            {
                Some<T>(var value) => value,
                _                  => defaultValue
            };
        }
    }
}