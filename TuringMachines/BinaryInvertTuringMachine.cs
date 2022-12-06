namespace TuringMachines;

public sealed class BinaryInvertTuringMachine : TuringMachine<int, char>
{
    protected override IReadOnlySet<char> Alphabet { get; } = new HashSet<char> { '0', '1' };

    protected override int InitialState { get; } = 1;

    protected override IReadOnlyDictionary<int, bool> FinalStates { get; } = new Dictionary<int, bool>
    {
        { 7, true },
    };

    protected override IReadOnlyDictionary<(int, char), TMAction> TransitionFunctions { get; } = new Dictionary<(int, char), TMAction>
    {
        { (1, Blank), new MoveRight { ToState = 2 } },

        { (2, '0'), new Write { ToState = 3, Value = '1' } },
        { (2, '1'), new Write { ToState = 3, Value = '0' } },
        { (2, Blank), new MoveLeft { ToState = 4 } },

        { (3, '0'), new MoveRight { ToState = 2 } },
        { (3, '1'), new MoveRight { ToState = 2 } },

        { (4, '0'), new Write { ToState = 6, Value = '1' } },
        { (4, '1'), new Write { ToState = 5, Value = '0' } },
        { (4, Blank), new MoveRight { ToState = 7 } },

        { (5, '0'), new MoveLeft { ToState = 4 } },

        { (6, '0'), new MoveLeft { ToState = 6 } },
        { (6, '1'), new MoveLeft { ToState = 6 } },
        { (6, Blank), new MoveRight { ToState = 7 } },
    };
}
