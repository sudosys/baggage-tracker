namespace BaggageTrackerApi.Extensions;

public static class EnumerableExtensions
{
    public static IEnumerable<T> EmptyIfNull<T>(this ICollection<T>? input) => input ?? Enumerable.Empty<T>();
}