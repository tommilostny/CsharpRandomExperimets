namespace TuringMachines;

public abstract class TuringMachine
{
    protected int _state = 1;
    protected int _tapeIndex = 0;
    protected readonly char[] _tape;

    public TuringMachine(string? input, char[] alphabet)
    {
        if (input is null)
            throw new ArgumentNullException(nameof(input));

        _tape = new char[input.Length + 2];

        for (int i = 0; i < input.Length; i++)
        {
            var inputChar = input[i];
            if (alphabet.Contains(inputChar))
            {
                _tape[i + 1] = inputChar;
                continue;
            }
            throw new ArgumentException($"'{inputChar}' is not from given alphabet.");
        }
    }

    /// <summary>
    /// Run the Turing machine with state logic directly from the diagram.
    /// Move right: i++;
    /// Move left:  i--;
    /// </summary>
    /// <returns> Accept, decline. </returns>
    public abstract bool Run();

    public void PrintTape()
    {
        foreach (var item in _tape)
        {
            Console.Write(item);
        }
        Console.WriteLine();
    }
}
