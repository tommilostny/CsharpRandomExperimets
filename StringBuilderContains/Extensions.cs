using System.Text;

namespace StringBuilderContains;

public static class Extensions
{
    //blbost, moc alokací :(
    public static bool ContainsParallel(this StringBuilder source, ReadOnlySpan<char> value, StringComparison cmpType = StringComparison.Ordinal)
    {
        var cores = Environment.ProcessorCount;
        var valueStr = value.ToString();

        Span<char> sourceSpan = stackalloc char[source.Length];
        source.CopyTo(0, sourceSpan, source.Length);

        var tasks = new Task<bool>[cores];
        var startIndex = 0;
        var partLength = source.Length / cores;
        var endIndex = partLength;

        for (int i = 0; i < cores; i++)
        {
            string window = sourceSpan[startIndex..endIndex].ToString();

            tasks[i] = Task.Run(() => window.Contains(valueStr, cmpType));
            
            startIndex = endIndex;
            if (i == cores - 2)
            {
                partLength += source.Length - cores * partLength;
            }
            endIndex += partLength;
        }
        return tasks.Any(t => t.Result);
    }

    public static bool Contains(this StringBuilder source, ReadOnlySpan<char> value, StringComparison cmpType = StringComparison.Ordinal)
    {
        Span<char> window = stackalloc char[value.Length << 1];

        for (var i = 0; i < source.Length; i += value.Length)
        {
            Span<char> higherHalf = window[value.Length..];
            higherHalf.CopyTo(window);
            higherHalf.Clear();

            var maxCopyCount = source.Length - i;
            source.CopyTo(i, higherHalf, Math.Min(value.Length, maxCopyCount));

            if (((ReadOnlySpan<char>)window).Contains(value, cmpType))
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
