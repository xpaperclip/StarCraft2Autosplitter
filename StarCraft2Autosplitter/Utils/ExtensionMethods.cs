using System.Collections.Generic;

namespace StarCraft2Autosplitter
{
    static class ExtensionMethods
    {
        public static T GetValueOrDefault<T>(this Dictionary<T, T> dict, T key)
        {
            T value;
            if (dict.TryGetValue(key, out value))
                return value;
            return key;
        }

        public static TValue GetValueOrDefault<TKey, TValue>(this Dictionary<TKey, TValue> dict, TKey key, TValue defaultValue)
        {
            TValue value;
            if (dict.TryGetValue(key, out value))
                return value;
            return defaultValue;
        }
    }
}
