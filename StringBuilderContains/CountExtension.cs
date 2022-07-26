using System.Text;

namespace StringBuilderContains;

internal static class CountExtension
{
    internal static int Count(this StringBuilder source, char value)
    {
        int count = 0;
        for (int i = 0; i < source.Length; i++)
        {
            if (source[i] == value)
            {
                count++;
            }
        }
        return count;
    }
}
