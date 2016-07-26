using System;
using System.Collections;
using System.Collections.Generic;

public static class DictionaryExtensions
{
    public static TValue GetValueOrDefault<TKey, TValue>
        (this IDictionary<TKey, TValue> dictionary,
            TKey key,
            TValue defaultValue)
    {
        TValue value;
        return dictionary.TryGetValue(key, out value) ? value : defaultValue;
    }
    public static TValue GetValueOrDefault<TKey, TValue>
        (this IDictionary<TKey, TValue> dictionary,
            TKey key,
            Func<TValue> defaultValueProvider)
    {
        TValue value;
        return dictionary.TryGetValue(key, out value)
            ? value
            : defaultValueProvider();
    }

    public static void ForEach<TKey, TValue>
        (this IDictionary<TKey, TValue> dictionary,
            Action<TKey, TValue> func)
    {
        foreach (var entry in dictionary)
        {
            func(entry.Key, entry.Value);
        }
    }

}
