namespace TuringMachines;

public sealed class BinaryInvertTuringMachine : TuringMachine
{
    protected override Lazy<char[]> Alphabet { get; } = new(new[] { '0', '1' });

    protected override Lazy<int[]> States { get; } = new(Enumerable.Range(1, 7).ToArray());

    protected override int InitialState => 1;

    protected override Lazy<(int state, bool result)[]> FinalStates { get; } = new(new[]
    {
        (7, true),
    });

    protected override Lazy<TMAction[]> TransitionFunctions { get; } = new(new TMAction[]
    {
        new(FromState: 1, ToState: 2, Input: Blank, TMActions.MoveRight),

        new(FromState: 2, ToState: 3, Input: '0', TMActions.Write, '1'),
        new(FromState: 2, ToState: 3, Input: '1', TMActions.Write, '0'),
        new(FromState: 2, ToState: 4, Input: Blank, TMActions.MoveLeft),
        
        new(FromState: 3, ToState: 2, Input: '0', TMActions.MoveRight),
        new(FromState: 3, ToState: 2, Input: '1', TMActions.MoveRight),
        
        new(FromState: 4, ToState: 6, Input: '0', TMActions.Write, '1'),
        new(FromState: 4, ToState: 5, Input: '1', TMActions.Write, '0'),
        new(FromState: 4, ToState: 7, Input: Blank, TMActions.MoveRight),
        
        new(FromState: 5, ToState: 4, Input: '0', TMActions.MoveLeft),
        
        new(FromState: 6, ToState: 6, Input: '0', TMActions.MoveLeft),
        new(FromState: 6, ToState: 6, Input: '1', TMActions.MoveLeft),
        new(FromState: 6, ToState: 7, Input: Blank, TMActions.MoveRight),
    });
}
