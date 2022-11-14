namespace StackAutomata;

public abstract class StackAutomaton
{
    protected int _state = 'S';
    protected Stack<char> _stack = new();

    private readonly Queue<char> _input = new();

    protected StackAutomaton(string? input, ReadOnlySpan<char> alphabet)
    {
        if (input is null)
        {
            throw new ArgumentNullException(nameof(input));
        }
        for (int i = 0; i < input.Length; i++)
        {
            var inputChar = input[i];
            if (alphabet.Contains(inputChar))
            {
                _input.Enqueue(inputChar);
                continue;
            }
            throw new ArgumentException($"'{inputChar}' is not from given alphabet.");
        }
    }

    public abstract bool Run();

    protected char GetNextInputChar() => _input.Dequeue();

    protected void PrintStep(char inputChar)
    {
        Console.Write(inputChar);
        Console.Write('\t');
        Console.Write("Stack: ");
        foreach (var item in _stack)
        {
            Console.Write(item);
            Console.Write(' ');
        }
        Console.WriteLine();
    }
}
