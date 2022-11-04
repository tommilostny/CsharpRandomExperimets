//Get binary number input from user.
Console.WriteLine("Binary number (string of 0s and 1s) to be inverted (42 -> -42): ");
var inputStr = Console.ReadLine();
if (inputStr == null)
    return 1;

//Initialize the tape.
Span<char> tape = stackalloc char[inputStr.Length + 2];
for (int j = 0; j < inputStr.Length; j++)
{
    var inputChar = inputStr[j];
    if (inputChar is '0' or '1')
    {
        tape[j + 1] = inputChar;
        continue;
    }
    return 1;
}
//Initialize the state and tape position variables.
byte state = 1;
byte i = 0;

//Run the Turing machine with state logic directly from the diagram.
//Move right: i++;
//Move left:  i--;
//Final state: 7

while (state != 7)
{
    switch (state)
    {
        case 1:
            if (tape[i] == 0)
            {
                i++;
                state = 2;
            }
            break;
        case 2:
            switch (tape[i])
            {
                case '0':
                    tape[i] = '1';
                    state = 3;
                    break;
                case '1':
                    tape[i] = '0';
                    state = 3;
                    break;
                default:
                    i--;
                    state = 4;
                    break;
            }
            break;
        case 3:
            if (tape[i] is '0' or '1')
            {
                i++;
                state = 2;
            }
            break;
        case 4:
            switch (tape[i])
            {
                case '0':
                    tape[i] = '1';
                    state = 6;
                    break;
                case '1':
                    tape[i] = '0';
                    state = 5;
                    break;
                default:
                    state = 7;
                    break;
            }
            break;
        case 5:
            if (tape[i] == '0')
            {
                i--;
                state = 4;
            }
            break;
        case 6:
            if (tape[i] is '0' or '1')
            {
                i--;
                break;
            }
            state = 7;
            break;

        default: return 1;
    }
}

foreach (var item in tape)
{
    Console.Write(item);
}
Console.WriteLine();
return 0;