namespace TuringMachines;

public abstract class TuringMachine
{
    protected const char Blank = (char)0;

    protected abstract record TMAction
    {
        public required int ToState { get; init; }
    }

    protected record MoveRight : TMAction { }

    protected record MoveLeft : TMAction { }

    protected record Write : TMAction
    {
        public required char Value { get; init; }
    }

    protected abstract Lazy<IReadOnlyCollection<char>> Alphabet { get; }

    protected abstract int InitialState { get; }

    protected abstract Lazy<IReadOnlyDictionary<int, bool>> FinalStates { get; }

    protected abstract Lazy<IReadOnlyDictionary<(int state, char input), TMAction>> TransitionFunctions { get; }

    private List<char>? _tape;
    private int _tapeIndex;

    public bool Run(string? input)
    {
        CheckInputInitTape(input);
        int state = InitialState;
        _tapeIndex = 0;
        while (true)
        {
            var currentSymbol = ReadFromTape(_tapeIndex);
            var functionToPerform = TransitionFunctions.Value[(state, currentSymbol)];
            switch (functionToPerform)
            {
                case Write w:
                    WriteToTape(_tapeIndex, w.Value);
                    break;
                case MoveLeft:
                    _tapeIndex--;
                    break;
                case MoveRight:
                    _tapeIndex++;
                    break;
                default:
                    throw new InvalidOperationException();
            }
            if (FinalStates.Value.TryGetValue(state = functionToPerform.ToState, out var retVal))
            {
                return retVal;
            }
        }
    }

    public void PrintTape()
    {
        if (_tape is not null)
        {
            //Print tape length.
            var lenStr = $"({_tape.Count(c => c != Blank)}) ";
            Console.Write(lenStr);
            
            //Print tape content.
            foreach (var item in _tape)
            {
                Console.Write(item);
            }
            Console.WriteLine();

            //Mark tape head position.
            int headMarkerPosition = _tapeIndex + lenStr.Length - 1;
            for (int i = 0; i <= headMarkerPosition; i++)
            {
                if (i == headMarkerPosition)
                {
                    Console.Write('^');
                    break;
                }
                Console.Write(' ');
            }
        }
    }

    private void WriteToTape(int index, char valueToWrite)
    {
        if (_tape is null)
        {
            throw new InvalidOperationException();
        }
        if (index >= _tape.Count)
        {
            _tape.Add(valueToWrite);
        }
        _tape[index] = valueToWrite;
    }

    private char ReadFromTape(int index)
    {
        if (_tape is null || index < 0 || index >= _tape.Count)
        {
            return Blank;
        }
        return _tape[index];
    }

    private void CheckInputInitTape(string? input)
    {
        if (input is null)
            throw new ArgumentNullException(nameof(input));

        _tape = new List<char> { Blank };

        foreach (var inputChar in input)
        {
            if (Alphabet.Value.Contains(inputChar))
            {
                _tape.Add(inputChar);
                continue;
            }
            throw new ArgumentException($"'{inputChar}' is not from alphabet {
                System.Text.Json.JsonSerializer.Serialize(Alphabet.Value)
            }.");
        }
    }
}
