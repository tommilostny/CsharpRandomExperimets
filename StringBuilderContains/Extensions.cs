using System.Text;

namespace StringBuilderContains;

public static class Extensions
{
    public static bool Contains(this StringBuilder stringBuilder, ReadOnlySpan<char> value)
    {
        int searchedLength = value.Length;
        int sbLength = stringBuilder.Length;
        Span<char> chunk = stackalloc char[searchedLength << 1];

        for (int i = 0; i < sbLength; i += searchedLength)
        {
            Span<char> higher = chunk[searchedLength..];
            higher.CopyTo(chunk);
            higher.Clear();

            var copyCount = searchedLength < sbLength - i ? searchedLength : sbLength - i;
            stringBuilder.CopyTo(i, higher, copyCount);

            if (chunk.Contains(value))
            {
                return true;
            }
        }
        return false;
    }

    public static bool ContainsRabinKarp(this StringBuilder stringBuilder, ReadOnlySpan<char> value)
    {
        int searchedLength = value.Length;
        int sbLength = stringBuilder.Length;
        Span<char> chunk = stackalloc char[searchedLength << 1];

        for (int i = 0; i < sbLength; i += searchedLength)
        {
            Span<char> higher = chunk[searchedLength..];
            higher.CopyTo(chunk);
            higher.Clear();

            var copyCount = searchedLength < sbLength - i ? searchedLength : sbLength - i;
            stringBuilder.CopyTo(i, higher, copyCount);

            if (chunk.ContainsRabinKarp(value))
            {
                return true;
            }
        }
        return false;
    }

    private static bool Contains<T>(this Span<T> span, ReadOnlySpan<T> value)
    {
        for (int i = 0, j = 0; i < span.Length; i++)
        {
            if (!EqualityComparer<T>.Default.Equals(span[i], value[j]))
            {
                j = 0;
                continue;
            }
            if (++j == value.Length)
            {
                return true;
            }
        }
        return false;
    }

    private static bool ContainsRabinKarp(this Span<char> span, ReadOnlySpan<char> value)
    {
        var valueHash = _DJB2(value);
        var lenght = value.Length;
        var end = span.Length - value.Length + 1;

        for (int i = 0; i < end; i++)
        {
            var window = span.Slice(i, lenght);
            if (_DJB2(window) == valueHash && window.SequenceEqual(value))
            {
                return true;
            }
        }
        return false;

        static ulong _SBDM(ReadOnlySpan<char> value)
        {
            ulong hash = 0;
            foreach (var item in value)
            {
                hash = 65599U * hash + item;
            }
            return hash;
        }

        static ulong _DJB2(ReadOnlySpan<char> value)
        {
            ulong hash = 5381;
            foreach (var item in value)
            {
                hash = ((hash << 5) + hash) + item;
            }
            return hash;
         }        
    }
}
