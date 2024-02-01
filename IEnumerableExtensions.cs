using System.Reflection;

public static class IEnumerableExtensions
{
    public static bool IsEmpty<T>(this IEnumerable<T> source) => !source?.Any() ?? true;

    public static List<T> FilterByDateRange<T>(this IEnumerable<T> list, Func<T, DateTime> GetDateFunc, DateTime start, DateTime end) =>
        list.ToList().FindAll(t => GetDateFunc.Invoke(t) >= start && GetDateFunc.Invoke(t) <= end);

    public static IEnumerable<(T Item, int Index)> WithIndex<T>(this IEnumerable<T> source) => source.Select((item, index) => (item, index));

    public static void ForEachWithIndex<T>(this IEnumerable<T> ie, Action<T, int> action)
    {
        var i = 0;
        foreach (var e in ie)
        {
            action(e, i++);
        }
    }

    public static void ForEach<T>(this IEnumerable<T> ie, Action<T> action)
    {
        foreach (var e in ie)
        {
            action(e);
        }
    }

    public static List<T> GetPropertiesOfType<T>(this Type type) =>
        type
            .GetProperties(BindingFlags.Public | BindingFlags.Static)
            .Where(propertyInfo => propertyInfo.PropertyType == typeof(T))
            .Select(propertyInfo => (T) propertyInfo.GetValue(null))
            .ToList();

    public static List<T> IntoList<T>(this T t) => new() { t };

    public static IEnumerable<T> Concat<T>(this T t, IEnumerable<T> list) => t.IntoList().Concat(list);

    public static T Transform<T>(this T t, Func<T, T> func) => func(t);

    public static IEnumerable<T> TakeUntilIncluding<T>(this IEnumerable<T> list, Func<T, bool> predicate)
    {
        foreach (T el in list)
        {
            yield return el;
            if (predicate(el))
                yield break;
        }
    }

    public static bool In<T>(this T item, params T[] list) => item != null && list.Contains(item);

    public static bool IsNullOrEmpty<T>(this IEnumerable<T> enumerable) => enumerable == null || !enumerable.Any();

    public static string Join(this IEnumerable<char> array, string separator) => string.Join(separator, array);

    public static string Join(this IEnumerable<string> array, string separator) => string.Join(separator, array);

    public static (List<T> TrueList, List<T> FalseList) Split<T>(this List<T> source, Func<T, bool> predicate)
    {
        var trueList = source.Where(predicate.Invoke).ToList();
        var falseList = source.Except(trueList).ToList();
        return (trueList, falseList);
    }

    public static IEnumerable<(T First, T Second)> Match<T>(this IEnumerable<T> firstList, IEnumerable<T> secondList, Func<T, T, bool> compare) =>
        firstList
            .Select(a => (first: a, second: secondList.FirstOrDefault(b => compare(a, b))))
            .Where(match => match.second != null);

    public static IEnumerable<T> MissingFrom<T>(this IEnumerable<T> firstList, IEnumerable<T> secondList, Func<T, T, bool> compare) =>
        firstList.Where(a => secondList.FirstOrDefault(b => compare(a, b)) == null);

    public static IComparable MostCommon<T>(this IEnumerable<T> list, Func<T, IComparable> func) =>
        func.Invoke(list.OrderByDescending(func).FirstOrDefault());
}
