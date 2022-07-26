using System.Text;

namespace StringBuilderContains;

internal static class SplitExtension
{
    internal static List<StringBuilder> Split(this StringBuilder stringBuilder, char delimiter)
    {
        var current = new StringBuilder();
        var result = new List<StringBuilder>();

        for (int i = 0; i < stringBuilder.Length; i++)
        {
            var character = stringBuilder[i];
            if (character == delimiter)
            {
                result.Add(current);
                current = new();
                continue;
            }
            current.Append(character);
        }
        if (current.Length > 0)
        {
            result.Add(current);
        }
        return result;
    }

    internal static StringBuilder[] Split2(this StringBuilder stringBuilder, char delimiter)
    {
        var current = new StringBuilder();
        var result = new StringBuilder[stringBuilder.Count(delimiter) + 1];
        var j = 0;
        for (var i = 0; i < stringBuilder.Length; i++)
        {
            var character = stringBuilder[i];
            if (character == delimiter)
            {
                result[j++] = current;
                current = new();
                continue;
            }
            current.Append(character);
        }
        if (current.Length > 0)
        {
            result[j] = current;
        }
        return result;
    }
}
