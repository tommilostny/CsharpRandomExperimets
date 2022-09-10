namespace SortingAlgorithms;

public static class QuickSort
{
    public static void Sort<T>(T[] array) where T : IComparable<T>
    {
        if (array.Length <= 1)
            return;

        var pivot = array[^1];
        var leCount = array.Count(x => x.CompareTo(pivot) <= 0) - 1;

        var lessEqual = new T[leCount];
        var greater = new T[array.Length - leCount - 1];
        int lei = 0, gi = 0;

        for (int i = 0; i < array.Length - 1; i++)
        {
            if (array[i].CompareTo(pivot) <= 0)
            {
                lessEqual[lei++] = array[i];
                continue;
            }
            greater[gi++] = array[i];
        }

        Sort(lessEqual);
        Sort(greater);

        for (int i = 0; i < array.Length; i++)
        {
            array[i] = i == leCount ? pivot : (i < leCount ? lessEqual[i] : greater[i - leCount - 1]);
        }
    }
}
