namespace TuringMachines;

/// <summary>
/// Turing machine that accepts words from language L = { a^(2^n) | n >= 0 }.
/// </summary>
public sealed class PowerOf2AsTuringMachine : TuringMachine
{
    protected override Lazy<IReadOnlyCollection<char>> Alphabet { get; } = new(new[] { 'a' });

    protected override int InitialState => 1;

    protected override Lazy<IReadOnlyDictionary<int, bool>> FinalStates { get; } = new(new Dictionary<int, bool>
    {
        { 7, true }, { 8, false },
    });

    protected override Lazy<IReadOnlyDictionary<(int, char), TMAction>> TransitionFunctions { get; } = new(new Dictionary<(int, char), TMAction>
    {
        { (1, Blank), new MoveRight { ToState = 2 } },

        { (2, 'a'), new MoveRight { ToState = 3 } },
        { (2, Blank), new MoveLeft { ToState = 8 } },

        { (3, '#'), new MoveRight { ToState = 3 } },
        { (3, 'a'), new Write { ToState = 4, Value = '#' } },
        { (3, Blank), new MoveLeft { ToState = 7 } },

        { (4, '#'), new MoveRight { ToState = 4 } },
        { (4, 'a'), new MoveRight { ToState = 5 } },
        { (4, Blank), new MoveLeft { ToState = 6 } },

        { (5, '#'), new MoveRight { ToState = 5 } },
        { (5, 'a'), new Write { ToState = 4, Value = '#' } },
        { (5, Blank), new MoveLeft { ToState = 8 } },

        { (6, '#'), new MoveLeft { ToState = 6 } },
        { (6, 'a'), new MoveLeft { ToState = 6 } },
        { (6, Blank), new MoveRight { ToState = 2 } },
    });
}
