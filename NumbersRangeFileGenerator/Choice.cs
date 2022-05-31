class Choice<TValue>
{
    private readonly HashSet<TValue> _questionNumbers = new();

    public int Count => _questionNumbers.Count;

    public override string ToString() => Count.ToString();

    public TValue this[int index] => _questionNumbers.ElementAt(index);

    public static Choice<TValue> operator + (Choice<TValue> choice, TValue questionNumber)
    {
        choice._questionNumbers.Add(questionNumber);
        return choice;
    }

    public IEnumerable<(TValue, bool)> GetAll()
    {
        for (var i = 0; i < Count; i++)
        {
            yield return (this[i], i == Count - 1);
        }
    }
}
