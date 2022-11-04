namespace TuringMachines;

/// <summary>
/// Turing machine that accepts words from language L = { a^(2^n) | n >= 0 }.
/// </summary>
public class PowerOf2AsTuringMachine : TuringMachine
{
    public PowerOf2AsTuringMachine(string? input) : base(input, alphabet: new[] { 'a' })
    {
    }

    public override bool Run()
    {
        while (_state != 7)
        {
            switch (_state)
            {
                case 1:
                    if (_tape[_tapeIndex] == 0)
                    {
                        _tapeIndex++;
                        _state = 2;
                    }
                    break;
                case 2:
                    if (_tape[_tapeIndex] == 'a')
                    {
                        _tapeIndex++;
                        _state = 3;
                        break;
                    }
                    _state = 8;
                    break;
                case 3:
                    switch (_tape[_tapeIndex])
                    {
                        case '#':
                            _tapeIndex++;
                            break;
                        case 'a':
                            _tape[_tapeIndex] = '#';
                            _state = 4;
                            break;
                        default:
                            _state = 7;
                            break;
                    }
                    break;
                case 4:
                    switch (_tape[_tapeIndex])
                    {
                        case '#':
                            _tapeIndex++;
                            break;
                        case 'a':
                            _tapeIndex++;
                            _state = 5;
                            break;
                        default:
                            _tapeIndex--;
                            _state = 6;
                            break;
                    }
                    break;
                case 5:
                    switch (_tape[_tapeIndex])
                    {
                        case '#':
                            _tapeIndex++;
                            break;
                        case 'a':
                            _tape[_tapeIndex] = '#';
                            _state = 4;
                            break;
                        default:
                            _state = 8;
                            break;
                    }
                    break;
                case 6:
                    switch (_tape[_tapeIndex])
                    {
                        case '#' or 'a':
                            _tapeIndex--;
                            break;
                        default:
                            _tapeIndex++;
                            _state = 2;
                            break;
                    }
                    break;
                default: return false;
            }
        }
        return true;
    }
}
