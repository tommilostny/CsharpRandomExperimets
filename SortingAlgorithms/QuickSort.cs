namespace SortingAlgorithms;

public static class QuickSort
{
    private static readonly Random _random = new();

    /// <summary>
    /// Async variant of the Quicksort algorithm. Memory inefficient with exponencial space complexity.
    /// </summary>
    /// <typeparam name="T">Comparable type (int, double, ...)</typeparam>
    /// <param name="array">Array to be sorted. Result is stored here.</param>
    public static async Task SortAsync<T>(T[] array) where T : IComparable<T>
    {
        if (array.Length <= 1)
            return;

        int pivotIndex = _random.Next() % array.Length;
        var pivot = array[pivotIndex];
        var leCount = array.Count(x => x.CompareTo(pivot) <= 0) - 1;

        var lessEqual = new T[leCount];
        var greater = new T[array.Length - leCount - 1];
        int lei = 0, gi = 0;

        for (int i = 0; i < array.Length; i++)
        {
            if (i == pivotIndex)
            {
                continue;
            }
            if (array[i].CompareTo(pivot) <= 0)
            {
                lessEqual[lei++] = array[i];
                continue;
            }
            greater[gi++] = array[i];
        }

        var task1 = SortAsync(lessEqual);
        var task2 = SortAsync(greater);
        await Task.WhenAll(task1, task2);

        for (int i = 0; i < array.Length; i++)
        {
            array[i] = i != leCount ? (i < leCount ? lessEqual[i] : greater[i - leCount - 1]) : pivot;
        }
    }

    /// <summary>
    /// In place variant using a Span.
    /// Memory efficient.
    /// </summary>
    /// <typeparam name="T">Comparable type (int, double, ...)</typeparam>
    /// <param name="span">Array span to be sorted. Result is stored in place here.</param>
    public static void Sort<T>(Span<T> span) where T : IComparable<T>
    {
        if (span.Length <= 1)
            return;

        var i = _random.Next() % span.Length;
        var pivot = span[i];
        (span[i], span[^1]) = (span[^1], pivot);
        i = -1;
        
        for (var j = 0; j < span.Length - 1; j++)
        {
            if (span[j].CompareTo(pivot) <= 0)
            {
                (span[++i], span[j]) = (span[j], span[i]);
            }
        }
        (span[++i], span[^1]) = (span[^1], span[i]);

        Sort(span[..i++]);
        Sort(span[i..]);
    }
}
