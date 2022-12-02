namespace TuringMachines;

/// <summary>
/// Turing machine that accepts words from language L = { a^(2^n) | n >= 0 }.
/// </summary>
public sealed class PowerOf2AsTuringMachine : TuringMachine
{
    protected override Lazy<char[]> Alphabet { get; } = new(new[] { 'a' });

    protected override Lazy<int[]> States { get; } = new(Enumerable.Range(1, 8).ToArray());

    protected override int InitialState => 1;

    protected override Lazy<(int state, bool result)[]> FinalStates { get; } = new(new[]
    {
        (7, true), (8, false),
    });

    protected override Lazy<TMAction[]> TransitionFunctions { get; } = new(new TMAction[]
    {
        new(FromState: 1, ToState: 2, Input: Blank, TMActions.MoveRight),
        
        new(FromState: 2, ToState: 3, Input: 'a', TMActions.MoveRight),
        new(FromState: 2, ToState: 8, Input: Blank, TMActions.MoveRight),
        
        new(FromState: 3, ToState: 3, Input: '#', TMActions.MoveRight),
        new(FromState: 3, ToState: 4, Input: 'a', TMActions.Write, '#'),
        new(FromState: 3, ToState: 7, Input: Blank, TMActions.MoveRight),
        
        new(FromState: 4, ToState: 4, Input: '#', TMActions.MoveRight),
        new(FromState: 4, ToState: 5, Input: 'a', TMActions.MoveRight),
        new(FromState: 4, ToState: 6, Input: Blank, TMActions.MoveLeft),
        
        new(FromState: 5, ToState: 5, Input: '#', TMActions.MoveRight),
        new(FromState: 5, ToState: 4, Input: 'a', TMActions.Write, '#'),
        new(FromState: 5, ToState: 8, Input: Blank, TMActions.MoveRight),
        
        new(FromState: 6, ToState: 6, Input: '#', TMActions.MoveLeft),
        new(FromState: 6, ToState: 6, Input: 'a', TMActions.MoveLeft),
        new(FromState: 6, ToState: 2, Input: Blank, TMActions.MoveRight),
    });
}
