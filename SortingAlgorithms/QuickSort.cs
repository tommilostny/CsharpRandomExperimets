namespace SortingAlgorithms;

public static class QuickSort
{
    private static readonly Random _random = new();

    public static async Task SortAsync<T>(T[] array) where T : IComparable<T>
    {
        if (array.Length <= 1)
            return;

        int pivotPos = _random.Next() % array.Length;
        var pivot = array[pivotPos];
        var leCount = array.Count(x => x.CompareTo(pivot) <= 0) - 1;

        var lessEqual = new T[leCount];
        var greater = new T[array.Length - leCount - 1];
        int lei = 0, gi = 0;

        for (int i = 0; i < array.Length; i++)
        {
            if (i == pivotPos)
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
}
