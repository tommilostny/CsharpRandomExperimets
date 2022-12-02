namespace SortingAlgorithms;

public static partial class SortExtensions
{
    /// <summary>
    /// Async variant of the Quicksort algorithm. Memory inefficient with exponencial space complexity.
    /// </summary>
    /// <typeparam name="T">Comparable type (int, double, ...)</typeparam>
    /// <param name="array">Array to be sorted. Result is stored here.</param>
    public static async Task QuickSortAsync<T>(this T[] array) where T : IComparable<T>
    {
        if (array.Length <= 1)
            return;

        int pivotIndex = _random.Next() % array.Length;
        var pivot = array[pivotIndex];
        var leCount = array.Count(x => x.CompareTo(pivot) <= 0) - 1;

        var lessEqual = new T[leCount];
        var greater = new T[array.Length - leCount - 1];
        int lei = 0, gi = 0;
        object l1 = new(), l2 = new();

        Parallel.For(0, array.Length, i =>
        {
            if (i != pivotIndex)
            {
                if (array[i].CompareTo(pivot) <= 0)
                {
                    lock (l1)
                    {
                        lessEqual[lei++] = array[i];
                    }
                    return;
                }
                lock (l2)
                {
                    greater[gi++] = array[i];
                }
            }
        });

        var task1 = QuickSortAsync(lessEqual);
        var task2 = QuickSortAsync(greater);
        await Task.WhenAll(task1, task2);

        Parallel.For(0, array.Length, i =>
        {
            array[i] = i != leCount ? (i < leCount ? lessEqual[i] : greater[i - leCount - 1]) : pivot;
        });
    }
}
