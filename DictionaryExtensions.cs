

public static class DictionaryExtensions
{
    public static Dictionary<TKey, List<T>> Group<T, TKey>(this IEnumerable<T> collection, Func<T, TKey> grouper) =>
        collection
            .GroupBy(grouper)
            .ToDictionary(g => g.Key, g => g.ToList());

    public static TV GetValue<TK, TV>(this IDictionary<TK, TV> dict, TK key) => key != null && dict.TryGetValue(key, out var value)
        ? value
        : default;
}
