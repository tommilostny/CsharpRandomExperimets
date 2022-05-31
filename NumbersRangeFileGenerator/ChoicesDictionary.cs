class ChoicesDictionary<TKey, TChoiceValue> where TKey : notnull
{
    private double _total;

    private readonly Dictionary<TKey, Choice<TChoiceValue>> _choices = new();

    public TChoiceValue this[TKey key]
    {
        set
        {
            _ = _choices.GetExistingOrNew(key) + value;
            _total++;
        }
    }

    public void PrintAllChoices()
    {
        var totalPercent = 0.0;

        foreach (var choiceKV in _choices)
        {
            var percentage = choiceKV.Value.Count / _total * 100.0;
            totalPercent += percentage;

            Console.Write($"{choiceKV.Key}: {choiceKV.Value}\t({Math.Round(percentage, 2)} %)\t[");

            foreach ((var number, var isLast) in choiceKV.Value.GetAll())
            {
                Console.Write(number);
                if (!isLast)
                {
                    Console.Write(", ");
                }
            }
            Console.WriteLine(']');
        }

        Console.WriteLine($"\nTotal choices: {_total} ({totalPercent} %)");
    }
}
