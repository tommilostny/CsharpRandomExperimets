using TuringMachines;

int selection;
do
{
    Console.WriteLine("Select Turing machine you wish to try:");
    Console.WriteLine("0. End");
    Console.WriteLine("1. Binary number invert");
    Console.WriteLine("2. Power of 2 'a's?");
    Console.WriteLine();
    try
    {
        selection = Convert.ToInt32(Console.ReadLine());
    }
    catch
    {
        selection = 0;
    }
    string? inputStr;
    switch (selection)
    {
        case 1:
            Console.WriteLine("Binary number (string of 0s and 1s) to be inverted (42 -> -42): ");
            inputStr = Console.ReadLine();
            try
            {
                var tm1 = new BinaryInvertTuringMachine(inputStr);
                tm1.Run();
                tm1.PrintTape();
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
            }
            break;

        case 2:
            Console.WriteLine("Sequence of 'a' symbols:");
            inputStr = Console.ReadLine();
            try
            {
                var tm2 = new PowerOf2AsTuringMachine(inputStr);
                if (tm2.Run())
                {
                    Console.WriteLine("Entered sequence is a power of 2.");
                }
                else
                {
                    Console.WriteLine("Entered sequence is not a power of 2.");
                }
                Console.WriteLine("TM tape state:");
                tm2.PrintTape();
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
            }
            break;
    }
    Console.WriteLine();
}
while (selection != 0);
