namespace SortingAlgorithms;

public static partial class SortExtensions
{
    public static void LinearSort(this int[] array)
    {
        int min, max;
        try { min = array.Min(); max = array.Max(); }
        catch { return; }

        var buffer = new uint[max - min + 1];

        for (int i = 0; i < array.Length; i++)
        {
            buffer[array[i] - min]++;
        }
        for (int i = 0, j = 0; i < buffer.Length; i++)
        {
            for (uint k = 0; k < buffer[i]; k++)
            {
                array[j++] = i + min;
            }
        }
    }
}
