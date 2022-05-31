static class Extensions
{
    public static int ToInt(this System.Text.StringBuilder sb)
    {
        if (int.TryParse(sb.ToString(), out var value))
        {
            return value;
        }
        return int.MaxValue;
    }

    public static TValue GetExistingOrNew<TKey, TValue>(this Dictionary<TKey, TValue> dict, TKey key)
        where TKey : notnull
        where TValue : new()
    {
        if (!dict.TryGetValue(key, out var storedValue))
        {
            storedValue = dict[key] = new TValue();
        }
        return storedValue;
    }

    public static int GetIntFromStart(this string str)
    {
        var stringBuilder = new System.Text.StringBuilder();
        foreach (var character in str)
        {
            if (!char.IsDigit(character))
            {
                break;
            }
            stringBuilder.Append(character);
        }
        return stringBuilder.ToInt();
    }
}