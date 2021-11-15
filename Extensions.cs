namespace TinyTypes
{
    public static class Extensions
    {
        public static T GetOrDefault<T>(this Option<T>? self, T defaultValue) =>
            self switch
            {
                Some<T>(var value) => value,
                _                  => defaultValue,
            };
    }
}