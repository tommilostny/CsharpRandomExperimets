using TuringMachines;

int selection;
do
{
    Console.WriteLine("Select Turing machine you wish to try:");
    Console.WriteLine("1. Binary number invert");
    Console.WriteLine("2. Word is from L = { a^(2^n) | n >= 0 }?");
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
                var result = tm2.Run();
                Console.Write("Length of entered sequence ");
                if (result)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("is");
                    Console.ResetColor();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("is not");
                    Console.ResetColor();
                }
                Console.WriteLine(" a power of 2.");
                Console.WriteLine("Turing machine tape:");
                tm2.PrintTape();
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
            }
            break;
    }
    Console.WriteLine();
    Console.ReadKey();
}
while (selection != 0);
