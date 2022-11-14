namespace StackAutomata;

public class TINdu2StackAutomaton : StackAutomaton
{
    public TINdu2StackAutomaton(string? input) : base(input, "abA0B")
    {
    }

    private const char _x = '×';
    private const char _o = '•';

    /// <summary>
    /// 1. Uvažujte abecedu Σ = {a, b, A, 0, B}. Sestrojte deterministický zásobníkový automat
    ///    přijímající jazyk L ⊆ Σ∗ definovany jako:
    ///         L = { w.0 | w ∈ {a, b}∗ ∧ #a(w) = #b(w) } ∪
    ///             { w.A | w ∈ {a, b}∗ ∧ #a(w) > #b(w) } ∪
    ///             { w.B | w ∈ {a, b}∗ ∧ #a(w) < #b(w) },
    ///    kde #x(w) pro x ∈ {a, b} značí počet výskytů symbolu x v řetězci w.
    ///    Použijte abecedu zásobníkových symbolů Γ = {×, •}
    ///    a symbol × jako startovací symbol zásobníku. Automat zapište v grafické formě.
    ///    Demonstrujte přijetí slova abaabA.
    /// </summary>
    public override bool Run()
    {
        _stack.Push(_x);

        while (_state != 'F')
        {
            (char tape, char stack) input;
            try
            {
                input = (GetNextInputChar(), _stack.Pop());
            }
            catch
            {
                return false;
            }
            switch (_state)
            {
                case 'S':
                    switch (input)
                    {
                        case ('0', _x):
                            _state = 'F';
                            break;
                        case ('a', _x):
                            _state = 'A';
                            _stack.Push(_x);
                            _stack.Push(_o);
                            break;
                        case ('b', _x):
                            _state = 'B';
                            _stack.Push(_x);
                            _stack.Push(_o);
                            break;
                        default:
                            return false;
                    }
                    break;

                case 'A':
                    switch (input)
                    {
                        case ('a', _o):
                            _stack.Push(_o);
                            _stack.Push(_o);
                            break;
                        case ('a', _x):
                            _stack.Push(_x);
                            _stack.Push(_o);
                            break;
                        case ('b', _o):
                            break;
                        case ('b', _x):
                            _state = 'B';
                            _stack.Push(_x);
                            _stack.Push(_o);
                            break;
                        case ('A', _o) or ('0', _x):
                            _state = 'F';
                            break;
                        default:
                            return false;
                    }
                    break;

                case 'B':
                    switch (input)
                    {
                        case ('b', _o):
                            _stack.Push(_o);
                            _stack.Push(_o);
                            break;
                        case ('b', _x):
                            _stack.Push(_x);
                            _stack.Push(_o);
                            break;
                        case ('a', _o):
                            break;
                        case ('a', _x):
                            _state = 'A';
                            _stack.Push(_x);
                            _stack.Push(_o);
                            break;
                        case ('B', _o) or ('0', _x):
                            _state = 'F';
                            break;
                        default:
                            return false;
                    }
                    break;

                default: //unsupported state
                    throw new InvalidOperationException();
            }
            PrintStep(input.tape);
        }
        return true;
    }
}
