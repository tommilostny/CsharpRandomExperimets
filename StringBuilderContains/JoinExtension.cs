using System.Text;

namespace StringBuilderContains;

internal static class JoinExtension
{
    internal static void JoinInto<T>(this IEnumerable<T> collection, StringBuilder destination, char separator = '\n')
    {
        destination.Clear();
        destination.AppendJoin(separator, collection);
    }
}
