namespace SortingAlgorithms;

public static partial class SortExtensions
{
    public static void QuickSort<T>(this T[] array) where T : IComparable<T>
    {
        array.AsSpan().QuickSort();
    }

    /// <summary>
    /// In place variant using a Span.
    /// Memory efficient.
    /// </summary>
    /// <typeparam name="T">Comparable type (int, double, ...)</typeparam>
    /// <param name="span">Array span to be sorted. Result is stored in place here.</param>
    public static void QuickSort<T>(this Span<T> span) where T : IComparable<T>
    {
        if (span.Length <= 1)
            return;

        var i = _random.Next() % span.Length;
        var pivot = span[i];
        (span[i], span[^1]) = (span[^1], pivot);
        i = -1;

        for (var j = 0; j < span.Length - 1; j++)
        {
            if (span[j].CompareTo(pivot) < 0)
            {
                (span[++i], span[j]) = (span[j], span[i]);
            }
        }
        (span[++i], span[^1]) = (span[^1], span[i]);

        QuickSort(span[..i++]);
        QuickSort(span[i..]);
    }
}
