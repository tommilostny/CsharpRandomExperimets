namespace TuringMachines;

public class BinaryInvertTuringMachine : TuringMachine
{
    public BinaryInvertTuringMachine(string? input) : base(input, alphabet: new[]{ '0', '1' })
    { }

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
                    switch (_tape[_tapeIndex])
                    {
                        case '0':
                            _tape[_tapeIndex] = '1';
                            _state = 3;
                            break;
                        case '1':
                            _tape[_tapeIndex] = '0';
                            _state = 3;
                            break;
                        default:
                            _tapeIndex--;
                            _state = 4;
                            break;
                    }
                    break;
                case 3:
                    if (_tape[_tapeIndex] is '0' or '1')
                    {
                        _tapeIndex++;
                        _state = 2;
                    }
                    break;
                case 4:
                    switch (_tape[_tapeIndex])
                    {
                        case '0':
                            _tape[_tapeIndex] = '1';
                            _state = 6;
                            break;
                        case '1':
                            _tape[_tapeIndex] = '0';
                            _state = 5;
                            break;
                        default:
                            _state = 7;
                            break;
                    }
                    break;
                case 5:
                    if (_tape[_tapeIndex] == '0')
                    {
                        _tapeIndex--;
                        _state = 4;
                    }
                    break;
                case 6:
                    if (_tape[_tapeIndex] is '0' or '1')
                    {
                        _tapeIndex--;
                        break;
                    }
                    _state = 7;
                    break;

                default: return false;
            }
        }
        return true;
    }
}
