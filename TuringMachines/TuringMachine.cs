using System.Text.Json;

namespace TuringMachines;

public abstract class TuringMachine
{
    protected const char Blank = (char)0;

    protected enum TMActions { Write, MoveLeft, MoveRight };

    protected record TMAction(int FromState, int ToState, char Input, TMActions Action, char? ValueToWrite = null);

    protected abstract Lazy<char[]> Alphabet { get; }

    protected abstract Lazy<int[]> States { get; }

    protected abstract int InitialState { get; }

    protected abstract Lazy<(int state, bool result)[]> FinalStates { get; }

    protected abstract Lazy<TMAction[]> TransitionFunctions { get; }

    private List<char>? _tape;

    public bool Run(string? input)
    {
        CheckInputInitTape(input);
        int state = InitialState, tapeIndex = 0;
        while (true)
        {
            var currentSymbol = ReadFromTape(tapeIndex);
            var functionToPerform = TransitionFunctions.Value.First(f =>
            {
                return f.FromState == state && f.Input == currentSymbol;
            });
            switch (functionToPerform.Action)
            {
                case TMActions.Write:
                    WriteToTape(tapeIndex, functionToPerform.ValueToWrite);
                    break;
                case TMActions.MoveLeft:
                    tapeIndex--;
                    break;
                case TMActions.MoveRight:
                    tapeIndex++;
                    break;
                default:
                    throw new InvalidOperationException();
            }
            state = functionToPerform.ToState;
            foreach (var final in FinalStates.Value)
            {
                if (final.state == state)
                    return final.result;
            }
        }
    }

    public void PrintTape()
    {
        if (_tape is not null)
        {
            Console.Write($"({_tape.Count(c => c != Blank)}) ");
            foreach (var item in _tape)
            {
                Console.Write(item);
            }
            Console.WriteLine();
        }
    }

    private void WriteToTape(int index, char? valueToWrite)
    {
        if (_tape is null || valueToWrite is null)
        {
            throw new InvalidOperationException();
        }
        _tape[index] = (char)valueToWrite;
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
            throw new ArgumentException($"'{inputChar}' is not from alphabet {JsonSerializer.Serialize(Alphabet)}.");
        }
    }
}
