using System.Numerics;
using System.Text.Json;

namespace TuringMachines;

/// <summary>
/// Base class for Turing machine simulations in C#.
/// </summary>
/// <typeparam name="TState">
/// Type of state names. (i.e. int with states in [1,2,3,...], string names, etc.)
/// </typeparam>
/// <typeparam name="TSymbol">
/// Type of symbol to be read and written to the machine tape
/// (<see cref="IMinMaxValue{TSymbol}.MinValue"/> will be used as <seealso cref="Blank"/>),
/// (checks for equality are also needed to compare against <seealso cref="Blank"/>).
/// </typeparam>
public abstract class TuringMachine<TState, TSymbol> where TSymbol : IMinMaxValue<TSymbol>, IEquatable<TSymbol>
{
    protected static readonly TSymbol Blank = TSymbol.MinValue;

    protected abstract record TMAction
    {
        public required TState ToState { get; init; }
    }

    protected record MoveRight : TMAction { }

    protected record MoveLeft : TMAction { }

    protected record Write : TMAction
    {
        public required TSymbol Value { get; init; }
    }

    /// <summary> Input alphabet Σ. </summary>
    protected abstract IReadOnlySet<TSymbol> Alphabet { get; }

    /// <summary> Initial state mark value of the machine. </summary>
    protected abstract TState InitialState { get; }

    /// <summary> States that indicate the end of the program and their boolean return value. </summary>
    protected abstract IReadOnlyDictionary<TState, bool> FinalStates { get; }

    /// <summary>
    /// (Current machine state, Symbol at the tape head)
    /// : Some state transition action that implements <see cref="TMAction"/>.
    /// </summary>
    protected abstract IReadOnlyDictionary<(TState, TSymbol), TMAction> TransitionFunctions { get; }

    private List<TSymbol>? _tape;
    private int _tapeIndex;

    public bool Run(TSymbol[]? input)
    {
        CheckInputInitTape(input);
        var state = InitialState;
        _tapeIndex = 0;
        while (true)
        {
            var currentSymbol = ReadFromTape(_tapeIndex);
            var functionToPerform = TransitionFunctions[(state, currentSymbol)];
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
            }
            if (FinalStates.TryGetValue(state = functionToPerform.ToState, out var retVal))
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
            var lenStr = $"({_tape.Count(c => !c.Equals(Blank))}) ";
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

    private void WriteToTape(int index, TSymbol valueToWrite)
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

    private TSymbol ReadFromTape(int index)
    {
        if (_tape is null || index < 0 || index >= _tape.Count)
        {
            return Blank;
        }
        return _tape[index];
    }

    private void CheckInputInitTape(TSymbol[]? input)
    {
        if (input is null)
            throw new ArgumentNullException(nameof(input));

        _tape = new List<TSymbol> { Blank };

        foreach (var inputChar in input)
        {
            if (Alphabet.Contains(inputChar))
            {
                _tape.Add(inputChar);
                continue;
            }
            throw new ArgumentException($"'{inputChar}' is not from alphabet {
                JsonSerializer.Serialize(Alphabet)
            }.");
        }
    }
}
